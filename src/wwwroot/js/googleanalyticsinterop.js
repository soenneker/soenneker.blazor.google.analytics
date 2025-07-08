export class GoogleAnalyticsInterop {
    init(tagId) {
        // Load the Google Analytics script
        const script = document.createElement('script');
        script.src = `https://www.googletagmanager.com/gtag/js?id=${tagId}`;
        script.async = true;
        document.head.appendChild(script);

        // Initialize gtag once the script is loaded
        script.onload = () => {
            window.dataLayer = window.dataLayer || [];
            function gtag() { window.dataLayer.push(arguments); }
            gtag('js', new Date());
            gtag('config', tagId);
        };
    }
}

window.GoogleAnalyticsInterop = new GoogleAnalyticsInterop();