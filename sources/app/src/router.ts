import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Home from "@/views/Home.vue";
import Login from "@/views/Login.vue";
import Multiguard from "vue-router-multiguard";
import AuthNavigationGuard from "./services/AuthNavigationGuard";

const guard = new AuthNavigationGuard();

const routes: Array<RouteRecordRaw> = [
    {
        path: "/login",
        name: "login",
        component: Login,
        beforeEnter: Multiguard([guard.handleAlreadyLoggedIn])
    },
    {
        path: "/",
        name: "home",
        component: Home,
        beforeEnter: Multiguard([guard.requireLogin])
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;
