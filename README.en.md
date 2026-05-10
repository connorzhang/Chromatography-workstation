# Chromatography Workstation (IBrainChrom)

[中文说明](./README.md)

This repository contains a WinForms desktop application targeting `.NET Framework 4.8` (`net48`).

## Project

- Solution: `IBrainChrom.sln`
- Project: `IBrainChrom.csproj`
- UI: WinForms
- Target framework: `.NET Framework 4.8`

## Docs

- VS2022 dev environment: `docs/DEV_SETUP.md`
- Reference machine VS2022 info: `docs/vs2022.txt`

## Dependencies

- DevExpress: the project references DevExpress `22.2` from the default installation path `C:\Program Files\DevExpress 22.2\...`
- Third-party DLLs: `SF-G/` (see `docs/DEV_SETUP.md`)

## Troubleshooting

- “Failed to load project file / Data at the root level is invalid”: usually caused by invalid hidden characters at the beginning of `IBrainChrom.csproj` (e.g., duplicated BOM). Make sure the first visible character is `<Project ...>`.
- Resource-related errors when using `dotnet msbuild`: this is a `net48` project; building with Visual Studio / .NET Framework MSBuild is recommended.
