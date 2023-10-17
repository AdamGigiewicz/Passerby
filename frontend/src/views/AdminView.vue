<template>
  <div>
    <h1>Admin Dashboard</h1>
    <h3>List of Logged In Users:</h3>
    <ul>
      <li v-for="user in users" :key="user.id">
        {{ user.firstName }} {{ user.lastName }}
      </li>
    </ul>
    <div v-if="users.loading" class="spinner-border spinner-border-sm"></div>
    <div v-if="users.error" class="text-danger">Error loading users: {{ users.error }}</div>
  </div>
</template>

<script setup>
  import { ref, computed } from 'vue';
  import { useUsersStore } from '@/stores';

  const usersStore = useUsersStore();
  const users = computed(() => usersStore.users);

  // Pobieranie u¿ytkowników (mo¿esz zmieniæ to zale¿nie od swojej implementacji)
  usersStore.getAll();
</script>
