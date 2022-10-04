import { TokenContract } from "@/contracts/TokenContract";
import { UserContract, UserLoginContract } from "@/contracts/UserContract";
import { AuthService } from "@/services/AuthService";
import { defineStore } from "pinia";
import { ref, computed } from "vue";
import { useErrorCode } from "@/plugins/ErrorCode";
import Router from "@/router";
import { AxiosError } from "axios";

export const useAuthStore = defineStore(
    "authStore",
    () => {
        const authService = new AuthService();
        const errorCode = useErrorCode();
        const token = ref<TokenContract>(null);
        const user = ref<UserContract>(null);

        const authenticated = computed(() => user.value != null && token.value != null);

        async function login(userLogin: UserLoginContract): Promise<void> {
            try {
                const authResponse = await authService.login(userLogin);
                token.value = authResponse.token;
                user.value = authResponse.user;

                Router.push({ path: "/" });
            } catch (error) {
                errorCode.showErrorMessage(error as AxiosError, "login");
            }
        }

        async function logout(): Promise<void> {
            try {
                await authService.logout();
                resetAuhtentication();

                Router.push({ path: "/login" });
            } catch (error) {
                console.log(error);
            }
        }

        async function refreshToken(): Promise<boolean> {
            try {
                const newToken = await authService.refreshToken(token.value);
                token.value = newToken;
            } catch (error) {
                resetAuhtentication();
                Router.push({ path: "/login" });
                return false;
            }

            return true;
        }

        function resetAuhtentication() {
            token.value = null;
            user.value = null;
        }

        function hasRole(role: string): boolean {
            return user.value.role.includes(role);
        }

        return { token, user, authenticated, login, refreshToken, logout, resetAuhtentication, hasRole };
    },
    { persist: true }
);
