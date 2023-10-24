import { defineStore } from 'pinia';
import { fetchWrapper } from '@/helpers';
import { ref } from 'vue';

const baseUrl = `${import.meta.env.VITE_API_URL}/admin`;

export const useAdminStore = defineStore('admin', () => {
  const users = ref()
  const userToEdit = ref();

  async function add(login, password) {
    await fetchWrapper.post(baseUrl, { login, password });
    getAll();
  }

  async function getAll() {
    users.value = { loading: true };
    fetchWrapper.get(baseUrl)
      .then(fetchedUsers => users.value = fetchedUsers)
  }

  async function setUserToEdit(id) {
    userToEdit.value = id;
  }

  async function getUserToEdit() {
    return getById(userToEdit.value)
  }

  async function getById(id) {
    return await fetchWrapper.get(`${baseUrl}/${id}`);
  }

  async function edit(user) {
    const { id, login, password, passwordCriteria, isAdmin, isBlocked } = user;
    await fetchWrapper.put(baseUrl, { id, login, password, passwordCriteria, isAdmin, isBlocked });
    getAll();
  }

  async function remove(id) {
    await fetchWrapper.delete(`${baseUrl}/${id}`, { id });
    getAll();
  }

  return { users, add, getAll, getById, setUserToEdit, getUserToEdit, edit, remove }
});
