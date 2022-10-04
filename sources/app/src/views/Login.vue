<template>
    <div class="min-h-screen flex flex-col justify-center">
        <div class="mx-auto w-80">
            <input
                v-model="username"
                :placeholder="$t('login.username')"
                type="text"
                class="bg-accent p-3 mb-2 w-full rounded-md text-white placeholder-secondary outline-none block"
            />
            <div class="w-full relative overflow-hidden">
                <input
                    v-model="password"
                    @keydown.enter="login"
                    :placeholder="$t('login.password')"
                    type="password"
                    :class="password.length > 0 ? 'w-2/3' : 'w-full'"
                    class="bg-accent p-3 mb-2 rounded-md text-white outline-none transition-all duration-300"
                />
                <button @click="login" class="text-white mt-3 w-1/3 absolute">
                    <BaseSpinner v-if="loading" class="mx-auto" />
                    <p v-else>{{ $t("login.button") }}</p>
                </button>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { useAuthStore } from "@/stores/authStore";
import { ref } from "vue";
import BaseSpinner from "@/components/BaseSpinner.vue";

const authStore = useAuthStore();

const loading = ref<boolean>(false);

const username = ref<string>("");
const password = ref<string>("");

async function login() {
    loading.value = true;

    await authStore.login({
        username: username.value,
        password: password.value
    });

    username.value = "";
    password.value = "";
    loading.value = false;
}
</script>
