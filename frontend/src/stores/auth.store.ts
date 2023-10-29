import { defineStore } from 'pinia';
import { fetchWrapper } from '@/helpers/fetch-wrapper';
import { router } from '@/helpers/router';
import { ref } from 'vue';

const baseUrl = `${import.meta.env.VITE_API_URL}/user`;

export const useAuthStore = defineStore('auth', () => {
  const token = ref("");
  const returnUrl = ref();
  loadToken();

  function loadToken() {
    const storedToken = localStorage.getItem('token');
    if (storedToken != null) {
      token.value = storedToken
    }
  }

  function saveToken(tokenToSave: string) {
    localStorage.setItem('token', tokenToSave);
  }

  async function signin(login: string, password: string) {
    const receivedToken = (await fetchWrapper.post(baseUrl, { login, password })).token;
    saveToken(receivedToken)
    loadToken();
    router.push(returnUrl.value || '/');
  }

  async function editPassword(oldPassword: string, newPassword: string) {
    await fetchWrapper.put(baseUrl, { oldPassword, newPassword });
  }

  function signout() {
    token.value = "";
    localStorage.removeItem('token');
    router.push('/login');
  }

  return { token, signin, editPassword, signout, returnUrl }
});
