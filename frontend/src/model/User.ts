export type User = {
  id: number,
  login: string,
  password: string,
  passwordCriteria: boolean,
  hasToResetPassword:boolean,
  isAdmin: boolean,
  isBlocked: boolean
}
