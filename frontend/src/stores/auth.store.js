import { defineStore } from 'pinia';
import { fetchWrapper, router } from '@/helpers';
import { ref } from 'vue';

const baseUrl = `${import.meta.env.VITE_API_URL}/user`;

export const useAuthStore = defineStore('auth', () => {
  const token = ref("");
  const returnUrl = ref();
  init();

  function init() {
    const storedToken = localStorage.getItem('token');
    if (storedToken != null) {
      token.value = JSON.parse(storedToken).token
    }
  }

  async function signin(login, password) {
    token.value = await fetchWrapper.post(baseUrl, { login, password });
    localStorage.setItem('token', JSON.stringify(token.value));
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
