import { defineStore } from 'pinia';
import { fetchWrapper } from '@/helpers';
import { ref } from 'vue';

const baseUrl = `${import.meta.env.VITE_API_URL}/admin`;

export const useAdminStore = defineStore('admin', () => {
  const users = ref()

  async function add(login, password) {
    await fetchWrapper.post(baseUrl);
  }

  async function getAll() {
    users.value = { loading: true };
    fetchWrapper.get(baseUrl)
      .then(fetchedUsers => users.value = fetchedUsers)
  }

  async function getById(id) {
    await fetchWrapper.get(`${baseUrl}/${id}`);
  }
  
  async function edit(user) {
    const {id, login, password, passwordCriteria, isAdmin, isBlocked} = user;
    await fetchWrapper.put(baseUrl, {id, login, password, passwordCriteria, isAdmin, isBlocked});
  }

  async function remove(id) {
    await fetchWrapper.delete(`${baseUrl}/${id}`);
  }

  return {users, add, getAll, getById, edit, remove}
});
