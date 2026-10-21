# 校园网自动登录通用脚本（Windows）

原生 Windows WinForms 自动登录程序，目标框架为 `net8.0-windows`。

## 环境

- Windows 10 或更高版本
- Visual Studio 2022/2026，安装“.NET 桌面开发”工作负载
- .NET 8 SDK

## 编译

用 Visual Studio 打开 `CampusNetManager.slnx`，或在本目录执行：

```powershell
dotnet build .\CampusNetManager.slnx -c Release
```

生成自包含单文件：

```powershell
dotnet publish .\CampusNetManager\CampusNetManager.csproj `
  -c Release -r win-x64 --self-contained true `
  -p:PublishSingleFile=true `
  -p:IncludeNativeLibrariesForSelfExtract=true `
  -o .\publish
```

如已安装 Inno Setup 6，可在 `publish` 生成后编译 `CampusNetManager-Setup.iss` 制作安装包。

## 首次配置

1. 打开程序并点击“账号设置”。
2. 账号栏只填写自己的学号或校园账号。
3. 选择校园网、联通、移动或电信，程序会自动拼接对应后缀。
4. 输入密码并保存。
5. 连接校园 Wi-Fi，点击“添加当前 Wi-Fi”。

家庭网络、手机热点和其他非名单 Wi-Fi 会被跳过。

## 连接接口设置

点击“连接设置”可以修改并保存：

- 认证门户 URL。
- Dr.COM 登录 API 端口。
- 设备达到上限时是否自动注销旧会话。
- 自助服务登录 URL。
- 在线设备仪表盘 URL。

关闭自动注销后不会访问自助服务。开启后，在线列表和注销接口会根据仪表盘 URL 自动派生。

## 自动运行

开启“登录 Windows 后自动在后台运行”后，程序会创建当前用户级别的计划任务。关闭主窗口后程序仍在托盘运行。

## 隐私

密码使用 Windows DPAPI 加密，仅保存在当前 Windows 用户的数据目录。仓库不包含账号、密码、受信 MAC 或任何个人网络配置。

## 适用范围

默认参数仅作为 Dr.COM 配置示例。使用前应在“连接设置”中核对或修改门户地址、登录 API 端口、自助服务登录地址和仪表盘地址。
