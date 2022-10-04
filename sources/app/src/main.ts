import { createApp } from "vue";
import { createPinia } from "pinia";
import VueMaplibreGl from "vue-maplibre-gl";
import App from "./App.vue";
import router from "./router";
import piniaPluginPersistedstate from "pinia-plugin-persistedstate";
import "./index.css";
import "mosha-vue-toastify/dist/style.css";
import i18n from "./i18n";

const app = createApp(App);

const pinia = createPinia();
pinia.use(piniaPluginPersistedstate);

app.use(pinia);
app.use(router);
app.use(VueMaplibreGl);
app.use(i18n);

app.mount("#app");
