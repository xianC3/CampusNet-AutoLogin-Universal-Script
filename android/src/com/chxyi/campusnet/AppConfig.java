package com.chxyi.campusnet;

final class AppConfig {
    static final String PACKAGE = "com.chxyi.campusnet.universal";
    static final String DEFAULT_PORTAL_URL =
            "http://172.26.3.60/a79.htm";
    static final int DEFAULT_PORTAL_API_PORT = 801;
    static final String DEFAULT_SELF_SERVICE_LOGIN_URL =
            "http://10.16.11.11:60001/Self/login/?302=LI";
    static final String DEFAULT_SELF_SERVICE_DASHBOARD_URL =
            "http://10.16.11.11:60001/Self/dashboard";
    static final String ACCOUNT = "";
    static final String PASSWORD = "";
    static final String DEFAULT_PROVIDER_SUFFIX = "";
    static final String DEFAULT_SSID = "";
    static final boolean PERSONAL_MODE = false;
    static final String DEFAULT_TRUSTED_MACS = "";

    static final String PREFS = "campusnet_prefs";
    static final String KEY_AUTO_ENABLED = "auto_enabled";
    static final String KEY_SSIDS = "campus_ssids";
    static final String KEY_LAST_STATUS = "last_status";
    static final String KEY_LAST_TIME = "last_time";
    static final String KEY_BATTERY_PROMPTED = "battery_prompted";
    static final String KEY_ACCOUNT = "account";
    static final String KEY_PASSWORD = "password";
    static final String KEY_PROVIDER_SUFFIX = "provider_suffix";
    static final String KEY_DEVICE_MAC = "device_mac";
    static final String KEY_TRUSTED_MACS = "trusted_macs";
    static final String KEY_PORTAL_URL = "portal_url";
    static final String KEY_PORTAL_API_PORT = "portal_api_port";
    static final String KEY_CLEANUP_ENABLED = "cleanup_enabled";
    static final String KEY_SELF_SERVICE_LOGIN_URL =
            "self_service_login_url";
    static final String KEY_SELF_SERVICE_DASHBOARD_URL =
            "self_service_dashboard_url";

    static final String ACTION_STATUS =
            "com.chxyi.campusnet.universal.STATUS_UPDATED";

    private AppConfig() {
    }
}
