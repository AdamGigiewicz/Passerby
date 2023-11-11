import "bootstrap/dist/css/bootstrap.css"
import { createApp } from 'vue';
import { createPinia } from 'pinia';

import App from './App.vue';
import { router } from './helpers/router';

import {VueReCaptcha} from "vue-recaptcha-v3";

const app = createApp(App);

app.use(createPinia());
app.use(router);
app.use(VueReCaptcha, { siteKey: "6LcG9AspAAAAAAvZLcFM5r-csluUH1eCAbTzxKBR"})
app.mount('#app')
import 'bootstrap/dist/js/bootstrap.js';

