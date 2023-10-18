import { defineStore } from 'pinia';

import { fetchWrapper } from '@/helpers';

const baseUrl = `${import.meta.env.VITE_API_URL}/users`;

export const useUsersStore = defineStore({
  id: 'users',
  state: () => ({
    users: {}
  }),
  actions: {
    async getAll() {
      this.users = { loading: true };
      fetchWrapper.get(baseUrl)
        .then(users => this.users = users)
        .catch(error => this.users = { error })
    },
    async delete(id) {
      const user = await fetchWrapper.delete(`${baseUrl}/remove/${id}`);

    },
    async getById(id) {
      const user = await fetchWrapper.get(`${baseUrl}/${id}`);
      return user;
    },
    async edit(login, password  ) {
      //const { id, login, role, resetDate, blocked, criteria, password } = user;
      return await fetchWrapper.put(`${baseUrl}/edit`, { login, password });
    },
  }
});
