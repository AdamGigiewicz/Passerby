export type User = {
  id: number,
  login: string,
  password: string,
  passwordCriteria: boolean,
  isAdmin: boolean,
  isBlocked: boolean
}
