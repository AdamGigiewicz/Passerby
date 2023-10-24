import { defineStore } from 'pinia';
import { fetchWrapper, router } from '@/helpers';
import { ref } from 'vue';

const baseUrl = `${import.meta.env.VITE_API_URL}/user`;

export const useAuthStore = defineStore('auth', () => {
  const token = ref(null);
  const returnUrl = ref();
  loadToken();

  function loadToken() {
    const storedToken = localStorage.getItem('token');
    if (storedToken != null) {
      token.value = storedToken
    }
  }

  function saveToken(tokenToSave) {
    localStorage.setItem('token', tokenToSave);
  }

  async function signin(login, password) {
    const receivedToken = (await fetchWrapper.post(baseUrl, { login, password })).token;
    saveToken(receivedToken)
    loadToken();
    router.push(returnUrl.value || '/');
  }

  async function editPassword(oldPassword, newPassword) {
    await fetchWrapper.post(baseUrl, { oldPassword, newPassword });
  }

  function signout() {
    token.value = null;
    localStorage.removeItem('token');
    router.push('/login');
  }

  return { token, signin, editPassword, signout }
});
