import { createToast } from "mosha-vue-toastify";

export function useToast() {
    function error(error: string) {
        createToast(error, {
            hideProgressBar: true,
            position: "bottom-right",
            toastBackgroundColor: "#FD3D30"
        });
    }

    return { error };
}
