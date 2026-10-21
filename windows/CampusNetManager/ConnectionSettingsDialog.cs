namespace CampusNetManager;

public sealed class ConnectionSettingsDialog : Form
{
    private readonly TextBox _portalUrl;
    private readonly NumericUpDown _apiPort;
    private readonly CheckBox _cleanupEnabled;
    private readonly TextBox _loginUrl;
    private readonly TextBox _dashboardUrl;

    public string PortalUrl { get; private set; }
    public int PortalApiPort { get; private set; }
    public bool AutoCleanupEnabled { get; private set; }
    public string SelfServiceLoginUrl { get; private set; }
    public string SelfServiceDashboardUrl { get; private set; }

    public ConnectionSettingsDialog(AppSettings settings)
    {
        PortalUrl = settings.PortalUrl;
        PortalApiPort = settings.PortalApiPort;
        AutoCleanupEnabled = settings.AutoCleanupEnabled;
        SelfServiceLoginUrl = settings.SelfServiceLoginUrl;
        SelfServiceDashboardUrl = settings.SelfServiceDashboardUrl;

        Text = "连接接口设置";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(650, 535);
        Font = new Font("Microsoft YaHei UI", 10f);
        BackColor = Color.FromArgb(245, 247, 251);

        var title = new Label
        {
            Text = "自定义校园网接口",
            Font = new Font("Microsoft YaHei UI", 17f, FontStyle.Bold),
            ForeColor = Color.FromArgb(24, 33, 47),
            AutoSize = true,
            Location = new Point(28, 22)
        };
        var hint = new Label
        {
            Text = "修改前请确认新地址能打开学校认证页。默认值兼容当前南昌校区。",
            ForeColor = Color.FromArgb(96, 112, 137),
            AutoSize = true,
            Location = new Point(30, 64)
        };

        var portalLabel = CreateLabel("认证门户 URL", 92);
        _portalUrl = CreateUrlBox(settings.PortalUrl, 116);

        var portLabel = CreateLabel("Dr.COM 登录 API 端口", 164);
        _apiPort = new NumericUpDown
        {
            Minimum = 1,
            Maximum = 65535,
            Value = Math.Clamp(settings.PortalApiPort, 1, 65535),
            Location = new Point(30, 188),
            Width = 150,
            Font = new Font("Microsoft YaHei UI", 11f)
        };

        _cleanupEnabled = new CheckBox
        {
            Text = "设备数达到上限时，允许自动进入自助服务注销旧会话",
            Checked = settings.AutoCleanupEnabled,
            AutoSize = true,
            Location = new Point(30, 234),
            ForeColor = Color.FromArgb(24, 33, 47)
        };

        var loginLabel = CreateLabel("自助服务登录 URL", 274);
        _loginUrl = CreateUrlBox(settings.SelfServiceLoginUrl, 298);

        var dashboardLabel = CreateLabel("在线设备仪表盘 URL", 346);
        _dashboardUrl = CreateUrlBox(settings.SelfServiceDashboardUrl, 370);

        var note = new Label
        {
            Text = "关闭自动注销后，设备数达到上限时不会访问自助服务。\n" +
                   "列表和注销接口会从仪表盘 URL 自动派生。",
            ForeColor = Color.FromArgb(96, 112, 137),
            AutoSize = true,
            Location = new Point(30, 430)
        };

        var save = new Button
        {
            Text = "保存",
            Location = new Point(430, 480),
            Width = 90,
            Height = 34
        };
        var cancel = new Button
        {
            Text = "取消",
            DialogResult = DialogResult.Cancel,
            Location = new Point(530, 480),
            Width = 90,
            Height = 34
        };
        AcceptButton = save;
        CancelButton = cancel;
        save.Click += (_, _) => SaveSettings();
        _cleanupEnabled.CheckedChanged += (_, _) =>
        {
            _loginUrl.Enabled = _cleanupEnabled.Checked;
            _dashboardUrl.Enabled = _cleanupEnabled.Checked;
        };
        _loginUrl.Enabled = _cleanupEnabled.Checked;
        _dashboardUrl.Enabled = _cleanupEnabled.Checked;

        Controls.AddRange(new Control[]
        {
            title, hint, portalLabel, _portalUrl, portLabel, _apiPort,
            _cleanupEnabled, loginLabel, _loginUrl, dashboardLabel,
            _dashboardUrl, note, save, cancel
        });
    }

    private Label CreateLabel(string text, int top)
    {
        return new Label
        {
            Text = text,
            ForeColor = Color.FromArgb(96, 112, 137),
            AutoSize = true,
            Location = new Point(30, top)
        };
    }

    private TextBox CreateUrlBox(string text, int top)
    {
        return new TextBox
        {
            Text = text,
            Location = new Point(30, top),
            Width = 590,
            Font = new Font("Microsoft YaHei UI", 10.5f)
        };
    }

    private void SaveSettings()
    {
        var portalUrl = _portalUrl.Text.Trim();
        if (!SettingsStore.IsHttpUrl(portalUrl))
        {
            ShowError("认证门户 URL 必须是完整的 http:// 或 https:// 地址。");
            return;
        }
        if (_apiPort.Value <= 0 || _apiPort.Value > 65535)
        {
            ShowError("登录 API 端口必须在 1 到 65535 之间。");
            return;
        }

        var loginUrl = _loginUrl.Text.Trim();
        var dashboardUrl = _dashboardUrl.Text.Trim();
        if (_cleanupEnabled.Checked &&
            !SettingsStore.IsHttpUrl(loginUrl))
        {
            ShowError("启用自动注销时，自助服务登录 URL 必须是有效地址。");
            return;
        }
        if (_cleanupEnabled.Checked &&
            !SettingsStore.IsHttpUrl(dashboardUrl))
        {
            ShowError("启用自动注销时，在线设备仪表盘 URL 必须是有效地址。");
            return;
        }

        PortalUrl = portalUrl;
        PortalApiPort = (int)_apiPort.Value;
        AutoCleanupEnabled = _cleanupEnabled.Checked;
        SelfServiceLoginUrl = SettingsStore.IsHttpUrl(loginUrl)
            ? loginUrl
            : AppSettings.DefaultSelfServiceLoginUrl;
        SelfServiceDashboardUrl = SettingsStore.IsHttpUrl(dashboardUrl)
            ? dashboardUrl.TrimEnd('/')
            : AppSettings.DefaultSelfServiceDashboardUrl;
        DialogResult = DialogResult.OK;
    }

    private void ShowError(string message)
    {
        MessageBox.Show(
            this,
            message,
            "连接接口设置",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        );
    }
}
