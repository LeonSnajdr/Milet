export interface UserContract {
    id: string;
    username: string;
    email: string;
    givenName: string;
    surName: string;
    role: string;
}

export interface UserLoginContract {
    username: string;
    password: string;
}
