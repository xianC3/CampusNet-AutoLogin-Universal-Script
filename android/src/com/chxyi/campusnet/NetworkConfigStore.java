package com.chxyi.campusnet;

import android.content.Context;
import android.content.SharedPreferences;

import java.net.URI;

final class NetworkConfigStore {
    static final class Config {
        final String portalUrl;
        final int portalApiPort;
        final boolean cleanupEnabled;
        final String selfServiceLoginUrl;
        final String selfServiceDashboardUrl;

        Config(
                String portalUrl,
                int portalApiPort,
                boolean cleanupEnabled,
                String selfServiceLoginUrl,
                String selfServiceDashboardUrl
        ) {
            this.portalUrl = portalUrl;
            this.portalApiPort = portalApiPort;
            this.cleanupEnabled = cleanupEnabled;
            this.selfServiceLoginUrl = selfServiceLoginUrl;
            this.selfServiceDashboardUrl = selfServiceDashboardUrl;
        }
    }

    private NetworkConfigStore() {
    }

    static Config load(Context context) {
        SharedPreferences prefs = context.getSharedPreferences(
                AppConfig.PREFS,
                Context.MODE_PRIVATE
        );
        String portalUrl = normalizeHttpUrl(
                prefs.getString(
                        AppConfig.KEY_PORTAL_URL,
                        AppConfig.DEFAULT_PORTAL_URL
                ),
                AppConfig.DEFAULT_PORTAL_URL
        );
        int apiPort = prefs.getInt(
                AppConfig.KEY_PORTAL_API_PORT,
                AppConfig.DEFAULT_PORTAL_API_PORT
        );
        if (apiPort < 1 || apiPort > 65535) {
            apiPort = AppConfig.DEFAULT_PORTAL_API_PORT;
        }
        String loginUrl = normalizeHttpUrl(
                prefs.getString(
                        AppConfig.KEY_SELF_SERVICE_LOGIN_URL,
                        AppConfig.DEFAULT_SELF_SERVICE_LOGIN_URL
                ),
                AppConfig.DEFAULT_SELF_SERVICE_LOGIN_URL
        );
        String dashboardUrl = normalizeHttpUrl(
                prefs.getString(
                        AppConfig.KEY_SELF_SERVICE_DASHBOARD_URL,
                        AppConfig.DEFAULT_SELF_SERVICE_DASHBOARD_URL
                ),
                AppConfig.DEFAULT_SELF_SERVICE_DASHBOARD_URL
        );
        if (dashboardUrl.endsWith("/")) {
            dashboardUrl = dashboardUrl.substring(
                    0,
                    dashboardUrl.length() - 1
            );
        }
        return new Config(
                portalUrl,
                apiPort,
                prefs.getBoolean(AppConfig.KEY_CLEANUP_ENABLED, true),
                loginUrl,
                dashboardUrl
        );
    }

    static void ensureDefaults(
            SharedPreferences prefs,
            SharedPreferences.Editor editor
    ) {
        if (!prefs.contains(AppConfig.KEY_PORTAL_URL)) {
            editor.putString(
                    AppConfig.KEY_PORTAL_URL,
                    AppConfig.DEFAULT_PORTAL_URL
            );
        }
        if (!prefs.contains(AppConfig.KEY_PORTAL_API_PORT)) {
            editor.putInt(
                    AppConfig.KEY_PORTAL_API_PORT,
                    AppConfig.DEFAULT_PORTAL_API_PORT
            );
        }
        if (!prefs.contains(AppConfig.KEY_CLEANUP_ENABLED)) {
            editor.putBoolean(AppConfig.KEY_CLEANUP_ENABLED, true);
        }
        if (!prefs.contains(AppConfig.KEY_SELF_SERVICE_LOGIN_URL)) {
            editor.putString(
                    AppConfig.KEY_SELF_SERVICE_LOGIN_URL,
                    AppConfig.DEFAULT_SELF_SERVICE_LOGIN_URL
            );
        }
        if (!prefs.contains(AppConfig.KEY_SELF_SERVICE_DASHBOARD_URL)) {
            editor.putString(
                    AppConfig.KEY_SELF_SERVICE_DASHBOARD_URL,
                    AppConfig.DEFAULT_SELF_SERVICE_DASHBOARD_URL
            );
        }
    }

    static boolean isHttpUrl(String value) {
        try {
            URI uri = new URI(value == null ? "" : value.trim());
            return uri.isAbsolute() &&
                    ("http".equalsIgnoreCase(uri.getScheme()) ||
                            "https".equalsIgnoreCase(uri.getScheme())) &&
                    uri.getHost() != null;
        } catch (Exception e) {
            return false;
        }
    }

    private static String normalizeHttpUrl(String value, String fallback) {
        String candidate = value == null ? "" : value.trim();
        return isHttpUrl(candidate) ? candidate : fallback;
    }
}
