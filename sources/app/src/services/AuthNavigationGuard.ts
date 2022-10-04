import { NavigationGuardNext, RouteLocation } from "vue-router";
import { useAuthStore } from "@/stores/authStore";

export default class AuthNavigationGuard {
    public async requireLogin(to: RouteLocation, from: RouteLocation, next: NavigationGuardNext): Promise<void> {
        const authStore = useAuthStore();

        if (!authStore.authenticated) {
            return next({ path: "/login" });
        }

        return next();
    }

    public async handleAlreadyLoggedIn(to: RouteLocation, from: RouteLocation, next: NavigationGuardNext): Promise<void> {
        const authStore = useAuthStore();

        if (authStore.authenticated) {
            return next({ path: "/" });
        }

        return next();
    }
}
