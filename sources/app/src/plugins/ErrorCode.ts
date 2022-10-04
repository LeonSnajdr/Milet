import { AxiosError } from "axios";
import { first, get, keys } from "lodash";
import i18n from "../i18n";
import { useToast } from "./Toast";

export function useErrorCode() {
    const { t } = i18n.global;
    const toast = useToast();

    function showErrorMessage(error: AxiosError, prefix: string) {
        const validationDetails: ValidationProblemDetails = error.response.data as ValidationProblemDetails;

        const errorKey: string = getValidationProblem(validationDetails);
        const errorTranslation = getErrorTranslation(`${prefix}.${errorKey}`, validationDetails.detail, "error.default");
        toast.error(errorTranslation);
    }

    function getValidationProblem(validationDetails: ValidationProblemDetails): string {
        const firstError: string = first(keys(validationDetails.errors));
        const firstProblem: string = first(get(validationDetails.errors, firstError));

        if (firstError && firstProblem) {
            return (firstError + "." + firstProblem).toLowerCase();
        }

        return "";
    }

    function getErrorTranslation(errorKey: string, details: string, fallbackKey: string): string {
        const translation = t(errorKey, { details: details }) as string;
        if (translation != errorKey) {
            return translation;
        } else {
            console.warn("translation missing:", errorKey);
            return t(fallbackKey) as string;
        }
    }

    return { showErrorMessage };
}

export interface ValidationProblemDetails {
    status?: number;
    title: string;
    detail: string;
    errors: ValidationError;
}

export interface ValidationError {
    [key: string]: string[];
}
