<template>
    <div>
        <h1>Admin Dashboard</h1>
        <div class="d-flex justify-content-end mt-3">
            <button class="btn btn-success" @click="addUser">Add User</button>
        </div>
        <h3>List of Logged In Users:</h3>
        <table class="table">
            <thead>
                <tr>
                    <th>Login</th>
                    <th>Actions</th> <!-- Trzecia kolumna z przyciskami -->
                </tr>
            </thead>
            <tbody>
                <tr v-for="user in users" :key="user.id">
                    <td>{{ user.login }}</td>
                    <td>
                        <!-- Przyciski do edycji i usuniêcia -->
                        <button class="btn btn-primary" @click="editUser(user)">Edit</button>
                        <button class="btn btn-danger" @click="deleteUser(user)">Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
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
