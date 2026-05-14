# 跨平台工作站重构方案（完善版 / 一次性重写 / Win+Linux / Go）

## 1. Summary

目标：将当前 WinForms（.NET Framework 4.8）单体色谱工作站，重构为**Win+Linux 同步可运行**的“边缘节点工作站”，按 **采集 / 分析 / UI** 三层解耦，并满足云端溯源与运维反控等要求。

本方案基于仓库现状与现有协议实现落点，重点把“昨天的规划”补齐为：**可直接开工的工程拆分、接口契约、兼容策略、验证路径与交付形态**。

新增约束（本次确认）：
- 重构版本需要严格参考现有 Windows 版本的分析逻辑；交付过程中必须持续用基线数据集做对照回归，确保关键输出一致（或在约定误差阈值内一致）。

关键决策（已确认）：
- 迁移策略：一次性重写（旧 WinForms 仅用于对照与抓包/回归）
- 目标平台：Windows + Linux 同步
- 分析内核：Go（已确认）

## 2. Current State Analysis（基于仓库现状）

### 2.1 现有系统形态

- 单体 WinForms 主程序：解决方案 [IBrainChrom.sln](file:///d:/GIT/VS2022/Chromatography-workstation/IBrainChrom.sln)，主项目 [IBrainChrom.csproj](file:///d:/GIT/VS2022/Chromatography-workstation/IBrainChrom.csproj)
- 入口： [Program.Main](file:///d:/GIT/VS2022/Chromatography-workstation/IBrainChrom2018/Program.cs#L241-L276)
- 主窗体： [FormMain](file:///d:/GIT/VS2022/Chromatography-workstation/IBrainChrom2018/FormMain.cs)

### 2.2 现有通信与端口

主站（仪器→工作站，工作站作为 TCP Server）：
- 端口：`25001`（主）+ `8000`（同协议的额外监听）（见 [AsyncTcpServer.Start](file:///d:/GIT/VS2022/Chromatography-workstation/IBrainChrom2018/AsyncTcpServer.cs#L281-L299) 与 `tcpListener_8000` 初始化 [AsyncTcpServer.InitAsyncTcpServer](file:///d:/GIT/VS2022/Chromatography-workstation/IBrainChrom2018/AsyncTcpServer.cs#L220-L239)）
- 协议：帧头 `GCKC`，结构 `GCKC + Len(2) + Body + CRC(1)`（详见 [docs/MASTER_STATION.md](file:///d:/GIT/VS2022/Chromatography-workstation/docs/MASTER_STATION.md)）
- 收包与分发核心： [TcpServerSocket.OneDataReceive](file:///d:/GIT/VS2022/Chromatography-workstation/IBrainChrom2018/TcpServerSocket.cs#L478-L545) → [TcpServerSocket.AnalyseReceivedData](file:///d:/GIT/VS2022/Chromatography-workstation/IBrainChrom2018/TcpServerSocket.cs#L547-L1058)

从站（对外 Modbus-like，外部系统→工作站）：
- 端口：`502/503`（工作站作为 server）
- 当前实现为“非标准 Modbus/TCP”，按 12 字节帧解析，且存在 `Addr=hi*255+lo` 的真实行为（详见 [docs/SLAVE_STATION.md](file:///d:/GIT/VS2022/Chromatography-workstation/docs/SLAVE_STATION.md)）
- 设备标识（StationId）要求：Holding Register `801~812`（12 寄存器，24 ASCII）见 [docs/DEVICE_ID_STATIONID.md](file:///d:/GIT/VS2022/Chromatography-workstation/docs/DEVICE_ID_STATIONID.md)

云端上报（当前 C# MQTT 遥测）：
- 启动： [Program.Main](file:///d:/GIT/VS2022/Chromatography-workstation/IBrainChrom2018/Program.cs#L255-L275)
- 实现： [MqttTelemetryService](file:///d:/GIT/VS2022/Chromatography-workstation/IBrainChrom2018/MqttTelemetryService.cs#L15-L145)

### 2.3 现有数据/方法/分析（用于对照，不作为新架构依赖）

- 当前谱图落盘主要为 `.sda` 自定义二进制（保存链路： [TcpServerSocket.Save](file:///d:/GIT/VS2022/Chromatography-workstation/IBrainChrom2018/TcpServerSocket.cs#L3696-L3913)）
- 当前方法 `.mtd` 通过二进制/可回退 XML 序列化（[MtdSetup.LoadFromFile](file:///d:/GIT/VS2022/Chromatography-workstation/IBrainChrom2018/MtdSetup.cs#L163-L188)）
- 当前积分引擎在 C#： [Chromatogram.Process](file:///d:/GIT/VS2022/Chromatography-workstation/IBrainChrom2018/Chromatogram.cs#L782-L789) → `Signal.ApplyIntegs(...)` → [ApplyIntegs](file:///d:/GIT/VS2022/Chromatography-workstation/IBrainChrom2018/ApplyIntegs.cs#L10-L131)

结论：现有系统已具备“采集→实时显示→保存→分析→结果对外（Modbus/MQTT）”闭环，但与跨平台目标冲突点在于：
- UI 强依赖 WinForms/控件生态
- 数据与方法格式（BinaryFormatter/GZip/自定义二进制）不利于跨语言/云端重算与长期治理
- 协议实现与业务逻辑深度耦合在 FormMain/TcpServerSocket

## 3. Proposed Architecture（一次性重写后的目标形态）

### 3.1 总体分层（边缘节点）

1) **Collector（采集服务）**
- 与仪器对接：实现 `GCKC` 主站协议，监听 `25001/8000`，维护会话与心跳/重连策略
- 将数据流切片为“周期 trace”（一个分析周期的点列 + 元信息）
- 通过 WebSocket 向 UI 推送实时点列与周期事件
- 落盘 trace（XML+JSON），并写入本地索引（SQLite）便于查询

2) **Analyzer（分析服务 / Go）**
- 输入：trace + method
- 输出：result（JSON；必要时可输出 XML）
- 先实现 targeted integration（套峰+峰宽/漂移补偿的最小闭环），不追求与旧 C# 数值完全一致，但要求确定性与可复现
- 对外接口：HTTP（JSON）为主（gRPC 预留）

3) **UI（Web UI，跨平台）**
- 浏览器 UI（Windows/Linux 都可用），由 Edge API 服务静态托管
- 模块：
  - Realtime Monitor：实时曲线、选择方法、周期结束自动分析、叠加标注、开始/停止与基础告警
  - Method Editor：打开历史 trace，框选污染物窗口，配置 padding/align/baseline，保存方法版本

4) **Edge API（统一对外 API + 网关）**
- 统一 HTTP API：管理仪器、方法、trace、result、配置
- 统一鉴权：本地默认无鉴权（或 token），云端/运维反控单独鉴权策略
- 承担 MQTT 上报（或单独 Telemetry 服务）

### 3.2 技术选型与交付形态（Win+Linux 同步）

- 后端：Go（collector/analyzer/api/telemetry），单仓库多模块
- UI：Web SPA（TypeScript）
- Windows：提供一个一键启动包（或 Windows Service），默认打开本机浏览器访问 UI
- Linux：systemd 服务（或 Docker Compose），UI 通过 `http://<host>:<port>` 访问

### 3.3 兼容性策略（关键）

- 主站协议（25001/8000）：**必须 100% 协议兼容**（否则仪器无法接入）
- MODBUS：对外只提供**标准 Modbus/TCP**（MBAP 头、标准功能码），按寄存器地址实现映射（参考 [docs/DEVICE_ID_STATIONID.md](file:///d:/GIT/VS2022/Chromatography-workstation/docs/DEVICE_ID_STATIONID.md) 与现有映射习惯）
- 数据/方法：新系统以 JSON/XML 标准为准；旧 `.sda/.mtd` 通过**离线转换工具**（可先 Windows-only）迁移历史数据

## 4. Data Contracts（落地可执行的契约）

### 4.1 Trace（voc-trace.v1）

沿用 [docs/CROSS_PLATFORM_WORKSTATION_PLAN.md](file:///d:/GIT/VS2022/Chromatography-workstation/docs/CROSS_PLATFORM_WORKSTATION_PLAN.md) 的 XML/JSON 定义，补充最少字段集：
- `schema`: 固定 `"voc-trace.v1"`
- `traceId`: UUID（边缘生成）
- `deviceId`: 仪器 DeviceID（对应主站协议的 16 字节 ID）
- `stationId`: 24 ASCII（用于云端溯源与 Modbus 映射）
- `dataTime`: 周期起始时间（ISO8601 或 yyyymmddhhmmss，二选一固定）
- `timeSpanS`: 周期时长（秒）
- `sampleRateHz` 或 `dtS`（二选一固定；建议 `dtS`）
- `values`: 点列（int/float，单位在 `unit` 字段）

落盘目录约定（建议）：
- `data/traces/YYYY/MM/DD/<traceId>.json`
- `data/traces/YYYY/MM/DD/<traceId>.xml`

### 4.2 Method（voc-method.v1）

最小字段集：
- `schema`: `"voc-method.v1"`
- `methodId`: UUID
- `version`: int（递增）
- `pollutants[]`: `{ code, name, startS, endS, paddingS, alignMode, baselineMode, threshold }`
- `quant[]`（可选）：定量参数（k/b、单位、换算等）

方法版本化要求：
- result 必须引用 `methodId/version`
- method 改动后必须生成新 version，不允许原地覆盖

### 4.3 Result（voc-result.v1）

最小字段集：
- `schema`: `"voc-result.v1"`
- `traceId`, `deviceId`, `stationId`, `methodId`, `methodVersion`
- `pollutants[]`: `{ code, name, status, rtS, area, height }`
- `engine`: `{ name, version, gitSha }`（用于可追溯）
- `createdAt`

## 5. APIs（边缘本地 API，HTTP/JSON）

以 Edge API 作为统一入口，内部再调用 Analyzer：

- `GET /api/v1/devices`：列出当前在线仪器（含状态、最近一次 trace）
- `POST /api/v1/devices/{id}/commands`：对仪器下发控制（封装主站 SendCmd 语义）
- `GET /api/v1/traces?from=&to=&deviceId=`：查询 trace（从 SQLite 索引）
- `GET /api/v1/traces/{traceId}`：获取 trace JSON
- `POST /api/v1/methods`：创建 method（version=1）
- `POST /api/v1/methods/{methodId}/versions`：创建新版本
- `GET /api/v1/methods/{methodId}`：获取 method 最新版本
- `POST /api/v1/analyze`：入参 `{ traceId, methodId, methodVersion }` 或直接传 trace/method，返回 result
- `GET /api/v1/results?traceId=`：查询结果
- `GET /ws/v1/realtime?deviceId=`：WebSocket 实时推送（点列增量、周期结束事件、告警）

Analyzer 服务接口（内部或独立部署）：
- `POST /analyzer/v1/analyze`：`{ trace, method } -> result`

## 6. Protocol Work Items（把旧协议“拆出来”并落到 Go）

### 6.1 主站协议（25001/8000，GCKC）

工作项：
1. 提炼协议层：在 Go 中实现
   - 帧扫描（`GCKC`）
   - 长度解析（大端 2 字节）
   - CRC/校验：复刻 [IBrainConvert.BitByBitNo](file:///d:/GIT/VS2022/Chromatography-workstation/IBrainChrom2018/IBrainConvert.cs#L251-L260) 的行为
2. 复刻会话层语义：
   - DeviceID 16 字节解析、特殊占位 ID 处理（参考 [docs/MASTER_STATION.md](file:///d:/GIT/VS2022/Chromatography-workstation/docs/MASTER_STATION.md#L75-L82)）
   - 去重策略：按 IP/DeviceID 的复用与踢重（参考 [AsyncTcpServer.CheckHasDoubleConnect](file:///d:/GIT/VS2022/Chromatography-workstation/IBrainChrom2018/AsyncTcpServer.cs#L635-L666)）
3. 命令分发最小集（支撑 MVP）：
   - 上行采集数据：cmd `143`（解析出点列增量、状态、温度等）
   - 分析开始/停止应答：`146/147/150/151`（用于周期控制）
   - 报警：`251`（用于告警与从站状态位）
   - 其余命令先按“透传/记录”保留，逐步补齐
4. 下行命令（MVP）：
   - `16/17` 控温开关
   - `18/19` 全分析开关
   - `22/23` 通道分析开始/停止（含显式通道）
   - CRC 与帧拼装必须严格一致（参考 [TcpServerSocket.SendCmd](file:///d:/GIT/VS2022/Chromatography-workstation/IBrainChrom2018/TcpServerSocket.cs#L3254-L3321)）

验证策略：
- 从旧系统提取“关键帧样本”（hex/pcap/日志），建立 golden tests：
  - 解包后字段一致
  - 构帧后与旧系统字节级一致（至少对关键命令）

### 6.2 Modbus/TCP（标准协议）

落地规则（标准 Modbus/TCP）：
- 端口：建议默认 `1502`（Linux 免特权端口），支持配置为 `502`
- 多仪器：使用 MBAP 的 `Unit Identifier` 作为“仪器选择”（单仪器固定 `UnitId=1`）
- 功能码：读写走标准 `01/03/05/15/06/16`
- StationId：Holding Register `801~812`（24 ASCII，12 寄存器），见 [docs/DEVICE_ID_STATIONID.md](file:///d:/GIT/VS2022/Chromatography-workstation/docs/DEVICE_ID_STATIONID.md)

映射规划（MVP）：
- Coils `10000..10008`：状态位（控温/分析/各通道采集态/点火/事件位）
- Coils `10000..10007`：控制位（写入后映射为主站下行命令：16/17、18/19、22/23、20 等）
- Holding Registers：按 `base = channelIndex*10000` 分段，提供“元信息 + StationId + 峰表”快照（峰表从 `base+1000` 起，每峰固定宽度，预留扩展）

## 7. Cloud Telemetry（云端溯源/留存）

约束（已确认）：
- 自建 EMQX 接收 → 入 Elasticsearch 留存≥3年
- 轻量/增量上报；规模按 1000 台、2 分钟/次、24h
- 组份不固定：按工作站实际峰结果上传
- 反控仅允许运维类（工艺类预留）

边缘侧实现建议：
- Telemetry 服务（Go）订阅本地事件总线：
  - 周期结束：trace 元信息 + result 摘要
  - 心跳：设备在线/状态位
  - 告警：来自 cmd 251 等
- MQTT Topic 约定（示例，最终需与你的云端规范对齐）：
  - `station/{stationId}/device/{deviceId}/telemetry`
  - `station/{stationId}/device/{deviceId}/result`
  - `station/{stationId}/device/{deviceId}/alarm`
- 上报策略：
  - result 走增量（仅上传本周期污染物结果）
  - trace 默认不上传（可配置上传/抽样上传）
- 反控：
  - 单独 topic `.../ops/cmd`，仅允许运维命令映射到本地 `POST /devices/{id}/commands`

## 8. Repo Layout（建议在本仓库内新增）

一次性重写但保留旧工程对照，建议新增：
- `src/edge/`：Go workspace
  - `cmd/collector/`
  - `cmd/analyzer/`
  - `cmd/edge-api/`
  - `cmd/telemetry/`
  - `internal/protocol/gckc/`：主站协议编解码
  - `internal/modbus/`：标准 modbus/tcp（寄存器映射 + 单元测试）
  - `internal/storage/`：trace/method/result 落盘 + SQLite 索引
  - `internal/bus/`：进程内事件总线（或 NATS/Redis，MVP 先用内存）
- `src/ui/`：Web UI
  - `apps/realtime-monitor/`
  - `apps/method-editor/`（可同一 SPA 用路由拆分）
- `docs/schemas/`：trace/method/result 的 JSON Schema + 示例
- `tools/legacy-converter/`：旧 `.sda/.mtd` 转换工具（可先 Windows-only）

## 9. Milestones（可验收的交付拆分）

M0（准备）：
- 抽取并固化协议样本：从旧系统日志/抓包形成 `testdata/`（hex fixtures）
- 固化 schema：trace/method/result v1 的 JSON Schema + 示例文件

M1（Collector 最小闭环）：
- Go collector 能监听 25001/8000，识别设备上线，接收采集数据 cmd 143
- WebSocket 推送实时点列（可在简单页面画出曲线）
- 周期结束能落盘 trace（JSON+XML）

M2（Analyzer v1）：
- 实现 targeted integration v1（padding + peakmax align + baseline_endpoints + threshold）
- 对同一输入保证确定性（浮点误差阈值内稳定）
- `POST /analyzer/v1/analyze` 跑通

M3（UI MVP）：
- Realtime Monitor：实时曲线、选择 method、周期结束自动调用 analyze 并叠加窗口/结果
- Method Editor：打开 trace、框选污染物窗口、保存 method v1（版本化）

M4（从站与对外）：
- 标准 Modbus/TCP Server（03/01/05/15 等）
- StationId 寄存器（801~812）

M5（云端上报）：
- MQTT 上报 result/telemetry/alarm
- 运维反控链路（云→边缘→仪器命令）跑通（权限与审计到位）

## 10. Verification（必须能自证“可用且可迁移”）

### 10.1 单元测试
- 主站协议：帧扫描/长度/CRC/编解码（golden bytes）
- Analyzer：给定 trace+method 的结果稳定性（golden JSON）
- Modbus：标准 modbus/tcp 的寄存器读写、异常码、端序策略

### 10.2 集成测试（推荐做一个“仪器模拟器”）
- Go 写一个 simulator：按 `GCKC` 协议周期性发送 cmd 143 点列 + 发送开始/停止应答
- Collector + UI 能完整跑通一个周期：实时显示 → 周期结束 → trace 落盘 → analyze → result 落盘 → UI 标注

### 10.3 基线对照（分析严格参考 Win 版本）
- 建立“基线导出器”：用现有 C# 引擎对固定 trace+method 导出基线结果（JSON）
- 建立“对照器”：新 Analyzer 输出与基线 JSON 自动对比，输出差异报告（字段差异、误差统计、是否通过）
- 验收口径（建议先约定并写死在测试里）：
  - RT：误差 <= 0.01 min（或等价秒数）
  - 峰面积/峰高：相对误差 <= 0.5%（小峰可加绝对误差兜底）
  - 峰数、定性匹配状态、定量结果：按业务规则逐项判等

### 10.4 UI 冒烟验收（连接主板 + 实时曲线）
- 连接：打开界面后可看到“设备在线”（来自主站连接与心跳）
- 控制：点击开始/停止可下发命令并收到应答，状态机与 Win 版本一致
- 曲线：实时点列持续增长、绘图连续刷新、Y 轴自动缩放可用且不抖动
- 周期：周期结束自动落盘 trace，自动触发 analyze，结果叠加显示

### 10.5 回归对照（与旧系统对照）
- 协议层：旧/新对同一输入帧的解析字段一致
- 控制链路：新系统下发命令，仪器/模拟器应答与状态机一致
- Modbus：外部系统按寄存器地址读取/写入，状态位与结果字段语义一致

## 11. Assumptions & Decisions（已锁定/待锁定）

已锁定：
- 一次性重写；Win+Linux 同步；Go 作为分析内核
- UI 采用 Web 形态以保证跨平台一致体验

待锁定（若你不提出变更，本方案默认按下列决定执行）：
- 标准 Modbus/TCP 端口默认 `1502`（避免占用系统 502 权限与冲突）；需要兼容 502 时通过配置 + Linux 能力授权实现
- trace 时间格式：统一用 ISO8601（含时区）并同时保留 `yyyymmddhhmmss` 字段用于兼容显示（两者都写，避免歧义）

---

## 附：与现有“昨天规划文档”的关系

- 本方案延续并落地 [docs/CROSS_PLATFORM_WORKSTATION_PLAN.md](file:///d:/GIT/VS2022/Chromatography-workstation/docs/CROSS_PLATFORM_WORKSTATION_PLAN.md) 的核心：三层解耦、XML+JSON 双格式、targeted integration 最小闭环、方法版本化。
- 本方案新增并细化：协议兼容与拆分、标准 Modbus/TCP 寄存器映射、具体 API、落盘与索引、云端上报与运维反控边界、以及可执行的里程碑与验证方案。
