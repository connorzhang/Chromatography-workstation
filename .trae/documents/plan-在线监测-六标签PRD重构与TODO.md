# 在线监测六标签（PRD）重构与完善计划

## Summary
把当前内嵌 HTML 的在线监测页面重构为 PRD 规定的 6 个顶栏标签：**概览 / 曲线 / 结果 / 事件 / 日志 / 设置**，并在不改端口（8080/25001/8000）、只连真实 GC 主板的前提下，逐步补齐旧版工作站中对应的“设备遥测（温度/EPC气路）/出数/历史报表/调试链路”。

本计划先产出“可用的六标签在线监测 MVP”（可以联调、可出数、可看遥测、可看事件/日志、可查结果历史），再把旧版工作站的“方法/谱图处理/报表”逐步下沉到二级入口（避免顶栏超过 6 个标签）。

## Current State Analysis（基于仓库现状）
### 现有实现落点
- 后端与页面同文件：`src/edge/cmd/collector/main.go`
  - TCP 监听：25001/8000（GCKC 帧）
  - HTTP：8080，SSE `/events`
  - 已有事件类型：`device`、`samples`、`result`、`telemetry`
  - 已有本地出数接口：`localResult`（按“采集时间”触发，仅出数不停止采集）
- 前端页面当前 6 标签为：主界面/谱图/仪器方法/谱图处理/记录报表/系统设置（与 PRD 不一致）
- “实测”与“实测℃”区域已接入遥测，但 EPC 映射在不同机型可能存在气路索引差异，需要配置化

### 旧版工作站功能对照（用于重构参考）
- 温度实测：来自主板 `Cmd=143` payload 头部 BCD 温度字段，旧版落到 `InsDeviceCtrl.ReadTempratureTable(...)`
- EPC 气路实测：来自主板 `Cmd=159`，旧版 `TcpServerSocket.Answer159(...)` 解析后更新 `InsDeviceCtrl.UpdateEpcInfo(...)`
  - 旧版同时显示实测 `psi` 与实测 `sccm`，并将 `655.35` 视为无效值（显示 `--`）
- 结果历史/报表：
  - NMHC（总烃/甲烷/非甲烷总烃）对应表 `RNNMHC`（SQLite `ngmpol.dll`）
  - BTEX 对应表 `RNBTEX`（后续再扩）
- 方法/积分/峰表：
  - `.mtd`：`MtdSetup`，应用入口 `Instrument.ApplyMethod`
  - 峰表字段：`Peak.area/height/pkRT/startT/endT` 等（后续在“谱图处理”二级入口实现）

## Decisions（已确认）
- 顶部 6 标签按 PRD：**概览 / 曲线 / 结果 / 事件 / 日志 / 设置**
- “实测(载气/氢气/空气)”显示：**psi + sccm 同时显示**
- 报表第一版：**先 NMHC（总烃/甲烷/非甲烷总烃），后续扩 BTEX**

## Proposed Changes（分阶段 TODO，可批量执行）
> 说明：每个阶段都保持端口不变；只做真实主板联调；每个阶段完成后通过脚本重启服务并人工验证页面。

### Phase A：六标签结构重排（PRD 对齐）
**目标**：顶栏与视图结构变为 PRD 六标签，切换标签不重建 SSE 连接。

- 文件：`src/edge/cmd/collector/main.go`
  - 前端 HTML：重命名 tabs 与 views
    - `home` → `overview`（概览）
    - `chrom` → `curve`（曲线）
    - 新增：`result`（结果）、`events`（事件）、`logs`（日志）
    - `settings` 保留为设置
  - 前端 JS：把当前“主界面 KPI/时间/进样”迁移到 `overview`；把曲线页逻辑迁移到 `curve`
  - 兼容策略：旧的“仪器方法/谱图处理/记录报表”先移出顶栏，作为后续二级入口（放到“设置”或“结果”页的按钮/卡片里）

**验收**
- 顶栏出现 6 标签（概览/曲线/结果/事件/日志/设置），切换不掉线
- `curve` 页继续实时画图、自动出数、显示遥测

### Phase B：概览页（设备状态 + 核心 KPI）
**目标**：对标 PRD“概览视图（设备状态）”，并复用现有设备列表 API。

- 前端（`overview`）新增卡片/列表：
  - 设备列表：仅显示 `GC...`，字段包含 `connected/lastSeen/lastCmd/cmdCounts[143]/last143`
  - 当前设备选择（与曲线页联动）
  - KPI 卡：总烃/甲烷/非甲烷总烃（展示“最新出数”的值）
  - 运行次数：先按“结果条数”计数（后续再对齐旧版 CountAnalyse / cntSeq）

**验收**
- 不进入曲线页也能看到设备在线与最近数据
- KPI 与曲线页结果一致

### Phase C：结果页（NMHC 历史 + 导出/删除）
**目标**：落地旧版 `RNNMHC` 的核心能力：历史列表、时间范围筛选、导出、删除。

实现路径（推荐按顺序）：
1) **前端内存版（快速可用）**
   - 前端维护一个 per-device 的结果环形缓冲（来自 SSE `result`），渲染表格
   - 支持时间范围过滤（按 `result.createdAt`）
2) **服务端持久化版（可选，但建议尽快补齐）**
   - 服务端维护结果历史（内存 + `.run/results_nmch.jsonl` 追加写）
   - 新增 API：
     - `GET /api/v1/results/nmhc?deviceId=&from=&to=&limit=`
     - `DELETE /api/v1/results/nmhc?deviceId=&from=&to=`
     - `GET /api/v1/results/nmhc/export.csv?...`

字段（第一版）：
- 时间、总烃、甲烷、非甲烷总烃、deviceId、traceId（可隐藏）

**验收**
- 每次出数都会进入“结果历史”
- 可按时间段删除（删除后列表减少）
- 可导出 CSV

### Phase D：事件页（最小事件流）
**目标**：对标 PRD“事件视图（最小事件流）”，用于联调排障。

- 前端维护事件列表（环形缓冲）
  - 收到 `device/samples/result/telemetry` 都追加一行摘要
  - 每条显示：时间、deviceId、type、（samples: ch/values长度/dt/t0）、（result: pollutant数量）、（telemetry: 有哪些字段）
  - 提供过滤：仅当前设备 / 全部设备

**验收**
- 能直观看到是否持续收到 143/159、是否持续出 result

### Phase E：日志页（调试统计与健康状态）
**目标**：汇总当前已有 dbg 信息与 server 信息，形成可复制的联调诊断文本。

- 前端显示：
  - server：pid、startedAt、tcpPorts、httpPort（来自 `/api/v1/server`）
  - 当前设备统计：lastCmd、143 次数、lastSeen/last143 ago、elapsed/fullWindow、control on/off
  - （可选）最近一次出数时间/是否出错（result.error）

**验收**
- 设备在线但无曲线/无结果时，日志页能给出明确线索（143=0、last143 很久等）

### Phase F：设置页（本地显示参数 + EPC 映射）
**目标**：把“显示相关配置”都集中到设置页并持久化到 localStorage，解决不同机型 EPC 索引不同导致的“载气/氢气/空气不对”问题。

- 设置项（localStorage）：
  - 默认满屏时间、默认 ylow/yhigh、默认峰高自适应、默认采集时间
  - EPC 映射：
    - 载气对应 EPC idx（默认 0）
    - 氢气对应 EPC idx（默认 1）
    - 空气对应 EPC idx（默认 2）
  - 气体显示格式固定为 `psi + sccm`

**验收**
- 通过调整 EPC idx 映射，能把三路气体对到实际值
- 刷新后设置仍保留

### Phase G：旧版“方法/谱图处理/BTEX/高级报表”二级入口（后续迭代）
> 不占用顶栏标签，作为后续批量执行的 TODO。

- 方法（Method Editor）：
  - 从 `.run/method.json` 读写；提供 UI 编辑 pollutant 窗口、阈值等
  - 服务端提供 `GET/POST /api/v1/method`（更新后即时生效，不必重启）
- 谱图处理（Process）：
  - 展示峰表（area/height/rtS/start/end）与积分可视化（至少只读）
  - 后续再考虑“交互式积分事件”（对齐旧版 `IntegRow/IntegOprtStyle`）
- BTEX：
  - 扩展 method 与 analyzer 输出 pollutants（苯/甲苯/二甲苯等）
  - 结果页与导出扩列（对齐旧版 RNBTEX/vocTable）

## Assumptions & Constraints
- 不更改端口（8080/25001/8000）；不引入模拟器；联调以 `GC...` 设备为准
- “采集时间=出数时间”逻辑继续保持：到点只触发出数（localResult），不提前重启周期
- EPC 实测在不同机型可能不是固定 idx0/1/2；必须支持配置化映射

## Verification（每阶段都执行）
- 运行单测：`go test ./...`（目录 `src/edge`）
- 用重启脚本拉起：`src/edge/scripts/restart-collector.*`
- 浏览器验证：
  - 六标签切换不断线
  - 曲线持续绘制（143 连续）
  - 温度（143）与气体（159）持续刷新
  - 采集时间到点出数进入结果页历史
  - 事件页能看到 samples/result/telemetry 事件滚动
  - 设置页改 EPC idx 后气体行显示发生对应变化

