import { AuthResponseContract } from "@/contracts/AuthResponseContract";
import { TokenContract } from "@/contracts/TokenContract";
import { UserLoginContract } from "@/contracts/UserContract";
import Axios from "@/plugins/Axios";

export class AuthService {
    public async login(userLogin: UserLoginContract): Promise<AuthResponseContract> {
        const result = await Axios.api.post<AuthResponseContract>("Auth/login", userLogin);
        return result.data;
    }

    public async refreshToken(oldToken: TokenContract): Promise<TokenContract> {
        const result = await Axios.api.post<TokenContract>("Auth/refresh", oldToken);
        return result.data;
    }

    public async test(): Promise<void> {
        const result = await Axios.api.post("Test");
        return result.data;
    }

    public async logout(): Promise<void> {
        const result = await Axios.api.delete("Auth/logout");
        return result.data;
    }
}
