<template>
    <MglMap mapStyle="https://api.maptiler.com/maps/streets/style.json?key=ktE68yuu0bp5J9ZIpzvv" :zoom="14" :center="[12.336790534844734, 49.62984211929829]">
        <MglNavigationControl :position="Position.TOP_LEFT" />
        <MglMarker :coordinates="[12.336790534844734, 49.62984211929829]"></MglMarker>
    </MglMap>
</template>

<script setup lang="ts">
import { AuthService } from "@/services/AuthService";
import { useAuthStore } from "@/stores/authStore";
import { MglMap, MglNavigationControl, Position, MglMarker } from "vue-maplibre-gl";

const authStore = useAuthStore();

const authService = new AuthService();

async function test() {
    try {
        const test = await authService.test();
        console.log(test);
    } catch (error) {
        console.log(error);
    }
}

async function logout() {
    await authStore.logout();
}
</script>

<style lang="scss">
@import "~vue-maplibre-gl/src/css/maplibre";
</style>
