# 快速操作手册

本手册记录日常开发、部署、调试所需的命令和操作方法，方便在任意窗口快速查阅。

---

## 环境配置

### 本地开发环境（Windows）

- **工作目录**：`I:\GIT\VS2022\Chromatography-workstation`
- **Go编译器**：`D:\GOPATH\go1.26.2\bin\go.exe`
- **Go模块缓存**：`I:\GIT\VS2022\go_cache\pkg\mod`
- **远程测试机**：`10.8.5.23`（办公室23测试机，Windows）
- **远程用户**：`trae`
- **远程密码**：见 `.env` 文件 `TEST_WIN_PASSWORD`

### 远程部署目录

- **服务目录**：`C:\Users\trae\Desktop\edge`
- **错误日志**：`C:\Users\trae\Desktop\edge\collector-service.err.log`
- **审计数据**：`C:\Users\trae\Desktop\edge\audit_history.json`
- **审计配置**：`C:\Users\trae\Desktop\edge\audit_config.json`

---

## 编译命令

### 本地编译（Windows AMD64）

```powershell
cd src\edge\cmd\collector
D:\GOPATH\go1.26.2\bin\go.exe build -o collector.exe .
```

### 编译并测试（不带锁定文件）

```powershell
cd src\edge\cmd\collector
D:\GOPATH\go1.26.2\bin\go.exe build -o collector_test.exe .
```

### 交叉编译（Linux ARM64）

```powershell
cd src\edge\cmd\collector
$env:GOOS="linux"
$env:GOARCH="arm64"
D:\GOPATH\go1.26.2\bin\go.exe build -trimpath -ldflags "-s -w" -o collector-linux-arm64 .
```

---

## 部署命令

### 全自动部署（推荐）

```powershell
cd i:\GIT\VS2022\Chromatography-workstation
powershell -ExecutionPolicy Bypass -File .\deploy_and_verify.ps1
```

脚本会自动：
1. 编译Go二进制
2. 打包部署文件
3. 停止远程服务
4. 上传压缩包
5. 解压并启动服务
6. 验证进程运行状态

### 手动部署步骤

#### 1. 编译

```powershell
cd src\edge\cmd\collector
D:\GOPATH\go1.26.2\bin\go.exe build -o collector.exe .
```

#### 2. 打包

```powershell
cd i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector
Compress-Archive -Path collector.exe, static, .env -DestinationPath deploy_new.zip -Force
```

#### 3. 上传到远程

```powershell
$env:SSH_PASSWORD = "a1234567A"
sshx "-h=10.8.5.23" "-u=trae" --password-only --upload=deploy_new.zip --to=C:\Users\trae\Desktop\edge\deploy_new.zip
```

#### 4. 远程解压并重启服务

```bash
sshx -h 10.8.5.23 -u trae --password-only "cd C:\Users\trae\Desktop\edge; tar -xf deploy_new.zip"
sshx -h 10.8.5.23 -u trae --password-only "cd C:\Users\trae\Desktop\edge; .\collector-service.exe restart"
```

#### 5. 验证

```powershell
curl.exe -s http://10.8.5.23:8080/
```

---

## 远程服务管理命令

### 使用WinSW服务管理

```bash
# 停止服务
sshx -h 10.8.5.23 -u trae --password-only "cd C:\Users\trae\Desktop\edge; .\collector-service.exe stop"

# 启动服务
sshx -h 10.8.5.23 -u trae --password-only "cd C:\Users\trae\Desktop\edge; .\collector-service.exe start"

# 安装服务（首次部署）
sshx -h 10.8.5.23 -u trae --password-only "cd C:\Users\trae\Desktop\edge; .\collector-service.exe install"

# 卸载服务
sshx -h 10.8.5.23 -u trae --password-only "cd C:\Users\trae\Desktop\edge; .\collector-service.exe uninstall"
```

### 直接进程管理（应急）

```bash
# 查看进程
sshx -h 10.8.5.23 -u trae --password-only "tasklist | findstr collector"

# 强制杀进程
sshx -h 10.8.5.23 -u trae --password-only "taskkill /F /IM collector.exe"

# 使用SYSTEM权限杀进程（防止普通账户无权Kill）
sshx -h 10.8.5.23 -u trae --password-only "schtasks /Run /TN KillCollector"
```

---

## 日志查看命令

### 查看服务日志

```bash
# 错误日志（主要调试用）
sshx -h 10.8.5.23 -u trae --password-only "type C:\Users\trae\Desktop\edge\collector-service.err.log"

# 查看最新50行
sshx -h 10.8.5.23 -u trae --password-only "Get-Content C:\Users\trae\Desktop\edge\collector-service.err.log -Tail 50"

# 实时跟踪日志
sshx -h 10.8.5.23 -u trae --password-only "Get-Content C:\Users\trae\Desktop\edge\collector-service.err.log -Wait"
```

### 本地编译日志

```powershell
cd src\edge\cmd\collector
D:\GOPATH\go1.26.2\bin\go.exe build -v . 2>&1 | Select-String "error"
```

---

## API 验证命令

### 健康检查

```powershell
curl.exe -s http://10.8.5.23:8080/api/v1/health
```

### 版本查询（HTML页面）

```powershell
curl.exe -s http://10.8.5.23:8080/ | Select-String "v0.3"
```

### 审计数据查询

```powershell
curl.exe -s http://10.8.5.23:8080/api/v1/audit
```

### 审计配置更新

```powershell
# 启用审计，间隔3分钟
curl.exe -X POST http://10.8.5.23:8080/api/v1/audit -H "Content-Type: application/json" -d "{\"enabled\":true,\"intervalMins\":3}"
```

### EPC状态查询

```powershell
curl.exe -s http://10.8.5.23:8080/api/v1/epc/state
```

### EPC配置下发

```powershell
curl.exe -X POST http://10.8.5.23:8080/api/v1/epc/config -H "Content-Type: application/json" -d "{\"mode\":1,\"pressure\":76.5,\"flow\":10.0,\"gasType\":1,\"units\":1}"
```

---

## Git 操作规范

### 提交规范

```
[default][clusterhmi][IChanged]-[] 中文描述
[default][clusterhmi][IAdd]-[] 中文描述
```

### 常用命令

```powershell
# 查看状态
git status

# 查看提交历史
git log --oneline -10

# 添加文件并提交
git add .
git commit -m "[default][clusterhmi][IChanged]-[] 中文描述"

# 推送到远程
git push origin tcd
```

### 版本号管理

每次提交前必须递增版本号，修改位置：

```go
// src/edge/cmd/collector/main.go 第69行
const AppVersion = "v0.3.146"
```

**注意**：`main.go` 文件混有GBK编码的中文字节，使用Python脚本更新版本号时**必须用二进制模式**（`rb`/`wb`）：

```python
with open(filepath, 'rb') as f:
    data = f.read()
data = data.replace(b'const AppVersion = "v0.3.145"', b'const AppVersion = "v0.3.146"')
with open(filepath, 'wb') as f:
    f.write(data)
```

---

## 调试技巧

### 审计快照调试

1. **查看远程日志**：
   ```bash
   sshx -h 10.8.5.23 -u trae --password-only "type C:\Users\trae\Desktop\edge\collector-service.err.log"
   ```
   查找 `[Audit]` 开头的日志行

2. **检查LastTelemetry状态**：
   - 正常：`LastTelemetry is nil? false`
   - 异常：`LastTelemetry is nil? true`

3. **验证API数据**：
   ```powershell
   curl.exe -s http://10.8.5.23:8080/api/v1/audit | python -m json.tool
   ```

### EPC延迟调试

1. **查看Modbus轮询频率**：
   ```bash
   # 在 auto_connect.go 中，startEpcHighFreqPoll 使用500ms定时器
   ```

2. **测试批量写入效果**：
   ```powershell
   # 使用Timer测量响应时间
   $sw = System.Diagnostics.Stopwatch::StartNew()
   Invoke-RestMethod -Uri 'http://10.8.5.23:8080/api/v1/epc/config' -Method POST -Body '{"mode":1,"pressure":76.5}' -ContentType 'application/json'
   $sw.Stop()
   Write-Host "响应时间: $($sw.ElapsedMilliseconds) ms"
   ```

---

## 常见问题排查

### 问题1：审计记录为空

**现象**：审计表格只有表头，没有数据行。

**排查步骤**：
1. 查看远程日志确认 `LastTelemetry` 状态
2. 检查 `auto_connect.go` 是否正确更新 `LastTelemetry`
3. 确认设备已连接且Modbus轮询正常

**修复**：在 [auto_connect.go](file:///i:/GIT/VS2022/Chromatography-workstation/src/edge/cmd/collector/auto_connect.go) 的温度和EPC轮询处添加 `LastTelemetry` 更新逻辑。

### 问题2：EPC下发延迟

**现象**：点击"下发配置"后等待十几秒才返回成功。

**排查步骤**：
1. 检查 `handleEPCConfig` 是否串行调用多次写操作
2. 确认后台500ms轮询是否打断写入流程
3. 查看远程日志中的Modbus错误

**修复**：使用 `WriteAllConfig` 批量写入替代多次独立写入。

### 问题3：版本号未更新

**现象**：部署后页面仍显示旧版本号。

**原因**：版本号更新脚本使用UTF-8编码读写GBK文件，导致替换失败。

**修复**：使用二进制模式读写文件（见上方"版本号管理"章节）。

---

## 快速参考表

| 操作 | 命令 |
|------|------|
| 编译本地 | `cd src\edge\cmd\collector; D:\GOPATH\go1.26.2\bin\go.exe build -o collector.exe .` |
| 自动部署 | `powershell -ExecutionPolicy Bypass -File .\deploy_and_verify.ps1` |
| 查看日志 | `sshx -h 10.8.5.23 -u trae --password-only "type C:\Users\trae\Desktop\edge\collector-service.err.log"` |
| 重启服务 | `sshx -h 10.8.5.23 -u trae --password-only "cd C:\Users\trae\Desktop\edge; .\collector-service.exe restart"` |
| 验证版本 | `curl.exe -s http://10.8.5.23:8080/ \| Select-String "v0.3"` |
| 查询审计 | `curl.exe -s http://10.8.5.23:8080/api/v1/audit` |
| 查询EPC | `curl.exe -s http://10.8.5.23:8080/api/v1/epc/state` |
