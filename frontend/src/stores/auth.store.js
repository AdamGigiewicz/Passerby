import { defineStore } from 'pinia';
import { fetchWrapper, router } from '@/helpers';
import { ref } from 'vue';

const baseUrl = `${import.meta.env.VITE_API_URL}/user`;

export const useAuthStore = defineStore('auth', () => {
  const token = ref();
  async function signin(login, password) {
    token.value = await fetchWrapper.post(baseUrl, { login, password });
    router.push(this.returnUrl || '/');
  }
  async function editPassword(oldPassword, newPassword) {
    const userToken = await fetchWrapper.post(baseUrl, { oldPassword, newPassword });
    token.value = userToken;
    router.push(this.returnUrl || '/');
  }
  function signout() {
    token.value = "";
    router.push('/login');
  }
  return { token, signin, editPassword,test, signout }
});
