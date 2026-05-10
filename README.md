# Chromatography Workstation（IBrainChrom）

[English README](./README.en.md)

本仓库为色谱工作站 WinForms 桌面程序源码（`.NET Framework 4.8` / `net48`）。

## 项目信息

- 解决方案：`IBrainChrom.sln`
- 项目：`IBrainChrom.csproj`
- UI：WinForms
- 目标框架：`.NET Framework 4.8`

## 文档

- 开发环境配置（VS2022）：`docs/DEV_SETUP.md`
- 参考机 VS2022 信息：`docs/vs2022.txt`

## 依赖说明

- DevExpress：项目引用了 DevExpress `22.2`（默认安装路径 `C:\Program Files\DevExpress 22.2\...`）
- 第三方 DLL：仓库内 `SF-G/`（详见 `docs/DEV_SETUP.md` 的“项目依赖 DLL（SF-G 目录）”）

## 常见问题

- 打开项目提示“未能加载项目文件 / 根级别上的数据无效（第 1 行，位置 1）”：通常是 `IBrainChrom.csproj` 文件开头存在不可见字符（如重复 BOM）。确保文件第 1 个可见字符就是 `<Project ...>`。
- 使用 `dotnet msbuild` 构建报资源相关错误：该项目为 `net48`，建议优先使用 VS2022 / MSBuild（.NET Framework）进行构建与调试。
