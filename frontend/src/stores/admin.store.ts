import { defineStore } from 'pinia';
import { fetchWrapper } from '@/helpers/fetch-wrapper';
import { ref } from 'vue';
import { User } from '@/model/User';

const baseUrl = `${import.meta.env.VITE_API_URL}/admin`;

export const useAdminStore = defineStore('admin', () => {
  const users = ref()
  const userToEdit = ref();

  async function add(login: string, password: string) {
    await fetchWrapper.post(baseUrl, { login, password });
    getAll();
  }

  async function getAll() {
    users.value = { loading: true };
    fetchWrapper.get(baseUrl)
      .then(fetchedUsers => users.value = fetchedUsers)
  }

  async function setUserToEdit(id: number) {
    userToEdit.value = id;
  }

  async function getUserToEdit() {
    return getById(userToEdit.value)
  }

  async function getById(id: number) {
    return await fetchWrapper.get(`${baseUrl}/${id}`);
  }

  async function edit(user: User) {
    await fetchWrapper.put(baseUrl, user);
    getAll();
  }

  async function remove(id: number) {
    await fetchWrapper.delete(`${baseUrl}/${id}`, { id });
    getAll();
  }

  return { users, add, getAll, getById, setUserToEdit, getUserToEdit, edit, remove }
});
