import { createI18n, LocaleMessages, LocaleMessageValue, VueMessageType } from "vue-i18n";

export class I18n {
    private static loadLocaleMessages(): LocaleMessages<Record<string, LocaleMessageValue<VueMessageType>>> {
        const locales = require.context("./translations", true, /[A-Za-z0-9-_,\s]+\.json$/i);
        const messages: LocaleMessages<Record<string, LocaleMessageValue<VueMessageType>>> = {};
        locales.keys().forEach((key) => {
            const matched = key.match(/([A-Za-z0-9-_]+)\./i);
            if (matched && matched.length > 1) {
                const locale = matched[1];
                messages[locale] = locales(key);
            }
        });

        return messages;
    }

    private static getBrowserLanguage(): string {
        return window.navigator.language ? window.navigator.language.substring(0, 2) : null;
    }

    public static getLocale(): string {
        return this.getBrowserLanguage() || process.env.VUE_APP_I18N_LOCALE;
    }

    public static getFallbackLocale(): string {
        return process.env.VUE_APP_I18N_FALLBACK_LOCALE;
    }

    public static getVueI18n() {
        return createI18n({
            locale: this.getLocale(),
            fallbackLocale: this.getFallbackLocale(),
            messages: this.loadLocaleMessages()
        });
    }
}

export default I18n.getVueI18n();
