import { useAuthStore } from "@/stores/authStore";
import axios, { AxiosInstance, AxiosRequestConfig, AxiosError } from "axios";

export default class Axios {
    private static instance: Axios = null;

    private api: AxiosInstance;

    private constructor() {
        this.api = this.createApi();
    }

    private createApi(): AxiosInstance {
        const client = axios.create();
        client.defaults.baseURL = process.env.VUE_APP_API_URL;

        const authStore = useAuthStore();

        const onRequest: (config: AxiosRequestConfig) => Promise<AxiosRequestConfig> = async (requestConfig: AxiosRequestConfig) => {
            if (authStore.token) {
                const headers = requestConfig.headers;
                if (headers) {
                    headers["Authorization"] = `Bearer ${authStore.token.accessToken}`;
                }
            }

            return requestConfig;
        };

        const onError: (error: AxiosError) => void = async (error: AxiosError) => {
            if (error.response?.status === 401) {
                const refreshSuccessfull = await authStore.refreshToken();
                if (refreshSuccessfull) {
                    error.config.headers["Authorization"] = `Bearer ${authStore.token.accessToken}`;

                    // Send the original response again
                    return axios.request(error.config);
                }
            } else if (error.response?.status === 403) {
                console.warn("axios api request requires permission", error.config?.url, error.message);
            }

            throw error;
        };

        client.interceptors.request.use(onRequest, undefined);
        client.interceptors.response.use(undefined, onError);

        return client;
    }

    public static get api(): AxiosInstance {
        if (!this.instance) {
            this.instance = new Axios();
        }

        return this.instance.api;
    }
}
