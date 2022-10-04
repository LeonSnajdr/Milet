import { TokenContract } from "./TokenContract";
import { UserContract } from "./UserContract";

export interface AuthResponseContract {
    token: TokenContract;
    user: UserContract;
}
