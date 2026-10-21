using System.Text.Json.Serialization;

namespace CampusNetManager;

public sealed class AppSettings
{
    public const string DefaultPortalUrl = "http://172.26.3.60/a79.htm";
    public const string DefaultSelfServiceLoginUrl =
        "http://10.16.11.11:60001/Self/login/?302=LI";
    public const string DefaultSelfServiceDashboardUrl =
        "http://10.16.11.11:60001/Self/dashboard";

    public string PortalUrl { get; set; } = DefaultPortalUrl;
    public int PortalApiPort { get; set; } = 801;
    public string Account { get; set; } = "";
    public string ProviderSuffix { get; set; } = "";
    public bool AutoCleanupEnabled { get; set; } = true;
    public string SelfServiceLoginUrl { get; set; } =
        DefaultSelfServiceLoginUrl;
    public string SelfServiceDashboardUrl { get; set; } =
        DefaultSelfServiceDashboardUrl;
    public List<string> CampusSsids { get; set; } = new();
    public bool AutoLoginEnabled { get; set; } = true;
    public string InterfaceName { get; set; } = "WLAN";
    public int RequestTimeoutSeconds { get; set; } = 8;
    public int WifiWaitSeconds { get; set; } = 45;
}
