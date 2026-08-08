# 变更记录 (CHANGELOG)

记录每次版本升级的详细内容，方便追溯问题。

---

## [v0.3.146] - 2026-08-05（当前最新版本）

### 审计快照功能修复（重大Bug修复）

**问题**：审计表格始终为空，没有真正的审计记录。

**根因分析**：
- 在HAL架构（散件模式）下，`auto_connect.go` 通过Modbus读取温度和EPC数据后，只调用 `hub.Publish()` 推送到WebSocket，**完全没有更新 `st.LastTelemetry`**
- 审计快照 `takeAuditSnapshot()` 读取的 `st.LastTelemetry` 始终为 `nil`，无法获取任何遥测数据
- 之前错误地尝试修改 `main.go` 中处理 Cmd 128/143/159 私有报文的逻辑，但这些报文在HAL架构下根本不会触发（TCD传感器使用Modbus RTU，不走私有协议）

**修复内容**：
- 在 [auto_connect.go](file:///i:/GIT/VS2022/Chromatography-workstation/src/edge/cmd/collector/auto_connect.go) 中：
  - 温度数据读取后（约第127行），加锁更新 `st.LastTelemetry` 的 TempCol/TempInj1/TempDet1 和 SetTemp 字段
  - EPC数据读取后（约第211行），加锁更新 `st.LastTelemetry` 的 CarrierPsi/CarrierSccm 字段
- 在 [audit_snapshot.go](file:///i:/GIT/VS2022/Chromatography-workstation/src/edge/cmd/collector/audit_snapshot.go) 中：
  - 当 `te == nil` 时返回而非强制记录空数据，避免脏数据污染审计文件
- 版本号从 v0.3.143 升级到 v0.3.146

**验证结果**：
- 审计快照每5分钟正常记录，包含真实温度、压力、流量数据
- 远程日志显示：`[Audit] Evaluated 1 devices, 1 had non-nil LastTelemetry`

---

## [v0.3.145] - 2026-08-05

### EPC下发延迟修复（性能优化）

**问题**：EPC配置下发需要等待十几秒才有反馈成功。

**根因分析**：
- `handleEPCConfig`（在 [hal_modbus_epc.go](file:///i:/GIT/VS2022/Chromatography-workstation/src/edge/cmd/collector/hal_modbus_epc.go) 第265行）需要串行写入5个Modbus寄存器（mode、pressure、flow、gasType、units）
- 每个写操作（`WriteSingleRegister`、`WriteFloat32`）都独立调用 `m.mu.Lock()` + `m.lockPort()`
- 后台 `startEpcHighFreqPoll` 每500ms调用 `ReadStateOnce()`，同样需要获取 `m.mu` 和 `portMu` 锁
- 导致每次写入都要等待后台轮询完成，5次写入 × (轮询耗时 + 50ms总线间隔) = 十几秒延迟

**修复内容**：
- 新增 `WriteAllConfig` 批量写入方法（[hal_modbus_epc.go](file:///i:/GIT/VS2022/Chromatography-workstation/src/edge/cmd/collector/hal_modbus_epc.go) 第228行）
- 一次性加锁、一次性锁端口完成全部5个寄存器写入，后台轮询无法在中间插入
- `handleEPCConfig` 改为调用 `WriteAllConfig` 替代5次独立写入

**验证结果**：
- EPC配置下发响应时间从十几秒降到秒级

---

## [v0.3.144] - 2026-08-04

### 审计快照功能初始实现

**新增功能**：
- 新增 [audit_snapshot.go](file:///i:/GIT/VS2022/Chromatography-workstation/src/edge/cmd/collector/audit_snapshot.go)：定时快照模块
- 新增 [api_audit.go](file:///i:/GIT/VS2022/Chromatography-workstation/src/edge/cmd/collector/api_audit.go)：审计API接口
- 新增前端审计页面 [audit.js](file:///i:/GIT/VS2022/Chromatography-workstation/src/edge/cmd/collector/static/js/views/audit.js)
- 支持配置快照间隔时间（默认5分钟）
- 记录温度、压力、流量、桥流等参数
- 数据持久化到 `audit_history.json`（最多10000条）

**问题**：
- 初始实现中存在 `st.LastTelemetry` 为 `nil` 的问题（见 v0.3.146 修复）

---

## [v0.3.143] - 2026-08-03

### 前端白屏问题修复

**问题**：谱图无法显示，出现白屏。

**根因**：PowerShell脚本注入JS代码时，反引号（\`）被吞噬导致 `audit.js` 语法错误。

**修复**：改用Python Base64解码直接写入文件，规避操作系统转义陷阱。

---

## [v0.3.141] - 2026-07-10

### 版本固化

- 版本号稳定在 v0.3.141
- 核心功能：HAL架构、Modbus轮询、TCD数据采集

---

## 历史版本（简要）

| 版本 | 日期 | 主要变更 |
|------|------|----------|
| v0.3.136 | 2026-07-10 | 修复99999自动循环问题、JS crash修复 |
| v0.3.135 | 2026-07-10 | 审计备注输入、TCD/EPC同步 |
| v0.3.124 | 2026-06-16 | HAL重构为SiLA 2 gRPC、SPA内存泄漏修复 |
| v0.3.98 | 2026-05-22 | TCD基线漂移计算 |
| v0.3.41 | 2026-05-15 | 后端自动连接、TCD/Modbus支持 |
| v0.2.1 | 2026-04 | 早期迭代 |
| v0.1.0 | 2026-03 | 初始版本 |
