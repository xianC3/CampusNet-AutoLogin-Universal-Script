# 校园网自动登录通用脚本

面向常见 Dr.COM 校园网门户的 Windows 与 Android 自动登录工具。

项目包含两个独立实现：

- `windows/`：原生 Windows WinForms 程序，支持系统托盘、登录后自动运行和 Wi-Fi 变化检测。
- `android/`：原生 Android 应用，支持开机检查、前台服务、定时任务和多校园 Wi-Fi 名单。

## 下载安装

普通用户无需编译源码，请从 [GitHub Releases](https://github.com/xianC3/CampusNet-AutoLogin-Universal-Script/releases/latest) 下载最新的开放版安装包：

- Android 1.1.0：[CampusNet-AutoLogin-Universal-Android-1.1.0.apk](https://github.com/xianC3/CampusNet-AutoLogin-Universal-Script/releases/latest/download/CampusNet-AutoLogin-Universal-Android-1.1.0.apk)
- Windows 1.1.0：[CampusNet-AutoLogin-Universal-Windows-Setup-1.1.0.exe](https://github.com/xianC3/CampusNet-AutoLogin-Universal-Script/releases/latest/download/CampusNet-AutoLogin-Universal-Windows-Setup-1.1.0.exe)
- 文件校验值：[SHA256SUMS.txt](https://github.com/xianC3/CampusNet-AutoLogin-Universal-Script/releases/latest/download/SHA256SUMS.txt)

## 开放配置

通用版不要求校园网接口固定：

- 认证门户 URL 可修改并保存。
- Dr.COM 登录 API 端口可修改。
- 设备上限自动注销可独立开启或关闭。
- 自助服务登录 URL 可修改。
- 在线设备仪表盘 URL 可修改。
- 在线列表和注销请求地址会根据仪表盘 URL 自动派生。

程序仍保留原有 Dr.COM 认证流程、校园 Wi-Fi 名单、外网连通性检查和安全注销逻辑。

## 主要功能

- 支持“校园网、联通、移动、电信”四种接入方式。
- 只处理用户加入名单的校园 Wi-Fi，家庭网络、手机热点和普通 Wi-Fi 会跳过。
- 同时检查校园门户认证状态和外部网络连通性。
- 支持添加、查看和删除多个校园 Wi-Fi 名称。
- 自动注销关闭后，设备数达到上限时不会访问自助服务。
- 自动注销开启后，设备数达到上限时只清理最旧会话；找不到本机 MAC（读取不到或在线列表中不存在）时同样只注销最旧的一条。
- Windows 自动切换校园网默认关闭；开启后也仅在当前网络无法上网且附近存在名单中的校园网时切换。
- 门户“系统忙”等临时错误会自动重试；校园门户接口异常但外网可用时不会误报断网。
- Windows 账号密码通过 DPAPI 加密后保存在当前用户目录。
- Android 账号密码只保存在应用私有存储中。

## 默认配置

仓库中的默认 URL 仅用于保持现有部署兼容，也是填写示例。其他学校或校区必须在软件中修改后保存，不能假定默认接口一定适用。

Windows：点击“连接设置”修改门户、API 端口和自助服务接口。

Android：点击“连接接口设置”修改保存。

## 仓库不包含的内容

- 不包含任何个人账号、密码、MAC 地址或个人校园 Wi-Fi 名称。
- 源码提交中不包含已签名的 APK、EXE 或安装包；正式构建通过 GitHub Releases 单独发布。
- 不包含 Android 签名密钥、Visual Studio 缓存或本机运行数据。

## 构建

Windows 构建说明见 [windows/README.md](windows/README.md)。

Android 构建说明见 [android/README.md](android/README.md)。

## 使用限制

本项目不是任何学校的官方软件，不能绕过账号欠费、停用、密码错误或门户自身的限制。

设备上限清理只会操作学校自助服务返回的在线会话。自动注销开启且找不到本机 MAC（读取不到或列表中不存在）时，程序会注销登录时间最早的一条会话；账号密码不正确、验证码持续失败、在线列表为空或接口格式异常时会停止注销。关闭自动注销后，程序不会访问自助服务。

## 安全说明

请只在自己的账号和已授权设备上使用。不要提交账号、密码、验证码、登录 Cookie、MAC 地址、运行日志或个人网络配置。
