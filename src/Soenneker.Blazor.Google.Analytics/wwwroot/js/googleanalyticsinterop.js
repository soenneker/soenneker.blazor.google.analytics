function getDataLayer() {
    window.dataLayer = window.dataLayer || [];
    return window.dataLayer;
}

function gtag() {
    getDataLayer().push(arguments);
}

function hasCommand(command, argument) {
    return getDataLayer().some(item => item && item[0] === command && (argument === undefined || item[1] === argument));
}

function hasGoogleTagScript() {
    return Array.from(document.scripts).some(script => {
        if (!script.src) {
            return false;
        }

        const url = new URL(script.src, document.baseURI);
        return url.hostname === "www.googletagmanager.com" && url.pathname === "/gtag/js";
    });
}

function toConsentState(settings, includeWaitForUpdate) {
    const state = {
        ad_storage: settings.adStorage ? "granted" : "denied",
        analytics_storage: settings.analyticsStorage ? "granted" : "denied",
        ad_user_data: settings.adUserData ? "granted" : "denied",
        ad_personalization: settings.adPersonalization ? "granted" : "denied"
    };

    if (includeWaitForUpdate && Number.isInteger(settings.waitForUpdateMilliseconds) && settings.waitForUpdateMilliseconds > 0) {
        state.wait_for_update = settings.waitForUpdateMilliseconds;
    }

    return state;
}

export function init(tagId) {
    if (!hasCommand("js")) {
        gtag("js", new Date());
    }

    if (!hasCommand("config", tagId)) {
        gtag("config", tagId);
    }

    if (hasGoogleTagScript()) {
        return;
    }

    const script = document.createElement("script");
    script.async = true;
    script.src = "https://www.googletagmanager.com/gtag/js?id=" + encodeURIComponent(tagId);
    document.head.appendChild(script);
}

export function setDefaultConsent(settings) {
    gtag("consent", "default", toConsentState(settings, true));
}

export function updateConsent(settings) {
    gtag("consent", "update", toConsentState(settings, false));
}

export function config(tagId, parameters) {
    gtag("config", tagId, parameters || {});
}

export function event(name, parameters) {
    gtag("event", name, parameters || {});
}

export function pageView(pageLocation, pageTitle) {
    const parameters = {};

    if (pageLocation) {
        parameters.page_location = pageLocation;
    }

    if (pageTitle) {
        parameters.page_title = pageTitle;
    }

    event("page_view", parameters);
}
