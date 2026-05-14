# Edge（Go）开发与本地联调

本目录是跨平台重构（边缘节点）的 Go 代码。

## 运行测试

在 `src/edge` 目录执行：

```bash
go test ./...
```

如果你的环境里 `go` 未加入 PATH，可直接使用安装路径：

```bash
"C:\Program Files\Go\bin\go.exe" test ./...
```

## Step 2：Collector（直连主板联调）

1) 启动采集服务（Collector）：

```bash
go run ./cmd/collector
```

也可以直接双击一键重启脚本（会打开一个单独的控制台窗口，方便你随时关闭/重启）：

- `src/edge/scripts/restart-collector.cmd`

默认：
- TCP：`25001` 与 `8000`
- HTTP：`127.0.0.1:8080`

注意：
- 本项目联调默认固定端口：TCP `25001/8000`、HTTP `8080`。

2) 打开界面查看实时曲线：

- `http://127.0.0.1:8080/`

说明：
- 页面只显示 `GC...` 的真实主板设备。
- 若能看到 `GC...` 在线但曲线为空，表示主板尚未发送 `Cmd=143` 实时数据流（通常需要先触发开始分析/采集流程）。

## 环境变量

Collector：
- `EDGE_ALLOW_CONTROL`：是否开启“开始/停止”等命令下发（默认关闭；联调时按需开启）
