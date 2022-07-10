export class UserResponse {
    constructor(
        public id: string,
        public userName: string,
        public firstName: string,
        public lastName: string,
        public email: string,
    ) { }
}