# 跨平台 VOCs 色谱工作站重构开发文档（边缘节点 + 云平台）

## 目标与约束

本次重构目标是将现有 Windows 工作站升级为跨平台（Linux/macOS/Windows）可运行的“边缘节点工作站”，并为后续云平台化做准备。

约束与已确认前提：

- 采集层：与色谱仪的连接使用网口协议（TCP），无需依赖 Windows 专有驱动。
- 报表层：不需要复刻原系统的 Word/Excel 互操作与复杂报表，允许使用新语言重新实现简单报表。
- 算法一致性：不要求与现有算法数值完全一致；要求同一输入在同一方法参数下输出稳定、可复现。
- UI：不复刻原 WinForms/DevExpress 界面；只需实现谱图展示与标定/套峰交互达到同等效果。
- 技术偏好：使用 Rust/Go 实现分析内核；未来要支持云平台化（方法下发、结果上报、云端重算）。

## 现有版本流程复盘（用于迁移对照）

现有版本已经实现“TCP 采集 → 实时显示 → 自动保存 → 停止后分析并打开谱图”的闭环链路。关键路径如下：

- 启动 TCP Server：`FormMain.StartTcpServer()`，创建采集端口 `25001` 与相关端口 `502/503`。
- 采集数据接收与解析：`TcpServerSocket.OneDataReceive()` → `AnalyseReceivedData()`。
- 开始/停止采集：UI 发送 cmd 22/23；应答 `Answer150/146` 将 `Signal.simple=true` 进入采集态；`Answer147` 触发 `StoptAllGather()` 保存。
- 实时曲线：采集包处理过程中调用 `Signal.AddDots(...)` 增长点列；`ChromAcqCtrl.timer_0_Tick` 定时 `Refresh()` 触发绘制。
- 落盘与分析：`TcpServerSocket.Save(...)` 构建 `Chromatogram`，调用 `Chromatogram.Process(...)` 完成积分/结果计算，然后落盘 `.sda`；若启用 `StopAutoAlalyse` 则自动打开谱图窗体。

迁移策略：保留“采集→显示→周期结束保存→分析→标注结果”的用户体验，但在新架构中拆分为独立模块与统一数据契约。

## 总体架构（边缘节点 + 可云化）

边缘节点建议按“采集 / 分析 / UI”三层解耦，可部署为多进程或同进程多模块：

1) 采集服务（TCP Ingest）
- 连接色谱仪（TCP），协议解析、重连、心跳。
- 将原始数据流切片为“一个测量周期”的谱图记录。
- 本地落盘：同步生成 XML + JSON。
- 对外发布：向本地 UI 推送实时点列/周期结束事件（WebSocket/IPC）。

2) 分析内核（Analyzer Core，Rust/Go）
- 输入：谱图记录（trace）+ 方法（method）。
- 输出：分析结果（result）。
- 对外形式：
  - 库模式（in-process）：低延迟。
  - 服务模式（out-of-process）：HTTP/gRPC，利于云化与多 UI。

3) UI
- Realtime Monitor：实时展示、选择方法、周期结束自动分析并标注结果。
- Method Editor：离线打开谱图、标定套峰、保存方法版本。

## 数据标准与文件格式（XML + JSON）

### 谱图 XML（已存在行业标准）

示例结构：

```xml
<?xml version="1.0" encoding="UTF-8"?>
<Voc>
  <DataTime>yyyymmddhhmmss</DataTime>
  <TimeSpan>120</TimeSpan>
  <Datas Count="600" Unit="pA">
    <Data Seq="0" Value="5" />
    <Data Seq="1" Value="10" />
    ...
  </Datas>
  <Pollutants>
    <Data PollCode="a05002" StartTime="34" EndTime="40" />
    <Data PollCode="a25003" StartTime="50" EndTime="56" />
  </Pollutants>
</Voc>
```

约定：

- X 轴生成规则（统一）：
  - `dt = TimeSpan / Datas.Count`（单位秒）
  - 第 `Seq=i` 点时间：`t[i] = i * dt`
- `Pollutants` 允许为空，表示未标定。

### JSON（推荐的通用格式）

JSON 与 XML 同源，建议将点序列展开为数组以提升性能：

```json
{
  "schema": "voc-trace.v1",
  "dataTime": "yyyymmddhhmmss",
  "timeSpanS": 120,
  "datas": {
    "count": 600,
    "unit": "pA",
    "values": [5, 10, 12]
  },
  "pollutants": [
    { "code": "a05002", "name": "总烃", "startS": 34, "endS": 40 },
    { "code": "a25003", "name": "甲烷", "startS": 50, "endS": 56 }
  ]
}
```

说明：

- `code` 是环保因子编码（如 `a05002`），`name` 是因子名称（如“总烃/甲烷”）。
- 结果换算系数（如 k/b）不与“因子编码/名称”混用，单独放在 method 的 quant 区。

## 方法（标定）与套峰分析

### 业务流程

1) 标定（Method Editor）：
- 打开历史谱图（XML/JSON）。
- 对每个污染物（因子）框选套峰窗口：`StartTime/EndTime`。
- 配置漂移鲁棒参数（padding、对齐策略）与基线策略。
- 保存为方法文件（JSON + XML），并做版本化（method_id/version）。

2) 实时分析（Realtime Monitor）：
- 采集服务持续输出实时点列；UI 实时绘制。
- 每个周期结束后自动：
  - 写入 trace（XML/JSON）。
  - 使用已选择的方法调用分析内核生成 result。
  - UI 将污染物窗口与结果（RT/面积/高度/状态）叠加标注。

### 漂移补偿（“套峰加峰宽”）

目的：周期性测量时 RT 小漂移导致窗口切峰不准。

机制建议同时具备：

- 窗口扩展：左右各加 `paddingS`，扩大积分范围。
- 窗口对齐：在扩展窗口内找峰顶（peakmax），并可选将窗口中心向峰顶对齐（限制最大平移量）。

默认推荐：`alignMode=peakmax` + 适度 `paddingS`。

### 目标积分（Targeted Integration）算法最小闭环

对每个污染物（code）执行：

- 取窗口：`[startS-paddingS, endS+paddingS]`（裁剪到 `[0, timeSpan]`）。
- 峰顶：窗口内取最大值点（正峰）或按配置支持负峰。
- 基线：默认用窗口两端点连线（linear_endpoints）；可选 robust_endpoints。
- 面积：对 `max(0, y-baseline)` 做梯形积分；高度：峰顶相对基线高度；RT：峰顶时间。
- 缺峰判定：`max_height < threshold` → `not_detected`。

该模式与“全谱自动检峰”解耦，适合云端批处理与现场稳定运行。

## 模块拆分与职责

### 边缘节点

**采集服务**
- 输入：TCP 数据流（仪器协议）。
- 输出：实时点列推送；周期 trace（XML/JSON）落盘；周期结束事件；可选上报云端。

**分析服务/库（Rust/Go）**
- 输入：trace + method。
- 输出：result（JSON，必要时可输出 XML）。

**Realtime Monitor UI**
- 实时曲线；选择 method；周期结束自动分析；叠加标注；本地基本操作（开始/停止、导出、上传、告警）。

### 方法编辑器（可与边缘 UI 同壳不同模块，也可独立应用）

**Method Editor UI**
- 打开历史谱图；框选污染物套峰；配置 padding/align/baseline；保存方法版本。

## API（本地与云端统一）

建议将分析内核以 HTTP/gRPC 暴露，便于边缘与云统一：

- `POST /trace`：上传 trace（JSON）。
- `POST /method`：上传/发布 method（JSON）。
- `POST /analyze`：入参 trace + method_id/version 或 method 内容，返回 result。
- `GET /result?...`：查询结果。

边缘可选择：

- 本地分析后只上报 result（节省云算力）。
- 或上传 trace 由云端统一分析（便于集中升级算法）。

## 版本治理与审计

- 方法文件必须版本化：`method_id + version`。
- 结果必须引用方法版本：result 内写明 `method_id/version`。
- trace + method 可重放得到同结果（强调确定性与可复现）。

## 里程碑（最小可用闭环）

M1：实现 voc-trace v1 的 XML/JSON 解析与序列化（采集服务与工具）。

M2：实现 method v1（pollutants + padding + align + baseline），Method Editor 能保存/加载。

M3：实现 targeted integration（area/height/rt/status）并提供 HTTP/gRPC 或库接口。

M4：Realtime Monitor：订阅采集点列实时绘图；周期结束自动分析并标注。

M5：边缘落盘（XML/JSON/result JSON）+ 可选云端上报接口。
