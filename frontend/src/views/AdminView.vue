<!--<template>
    <div>
        <h1>Admin Dashboard</h1>
        <div class="d-flex justify-content-end mt-3">
            <button class="btn btn-success" @click="showAddUserForm">Add User</button>
        </div>
        <h3>List of Logged In Users:</h3>
        <table class="table">
            <thead>
                <tr>
                    <th>Login</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="user in users" :key="user.id">
                    <td>{{ user.login }}</td>
                    <td>
                        <button class="btn btn-primary" @click="editUser(user)">Edit</button>
                        <button class="btn btn-danger" @click="deleteUser(user)">Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
        <div v-if="users.loading" class="spinner-border spinner-border-sm"></div>
        <div v-if="users.error" class="text-danger">Error loading users: {{ users.error }}</div>
    </div>-->
<!-- Wyświetlanie formularza dodawania użytkownika -->
<!--<AddUserForm v-if="showAddUserForm" @closeForm="showAddUserForm = false" />-->
<!-- Wyświetlanie formularza edycji użytkownika -->
<!--<EditUserForm v-if="showEditUserForm" :user="selectedUser" @closeForm="showEditUserForm = false" />
</template>

<script>
    import AddUserForm from '@/components/AddUserForm.vue';
    import EditUserForm from '@/components/EditUserForm.vue';

    export default {
        components: {
            AddUserForm,
            EditUserForm
        },
        data() {
            return {
                showAddUserForm: false,
                showEditUserForm: false,
                selectedUser: null
            };
        },
        methods: {
            addUser() {
                this.showAddUserForm = true;
            },
            editUser(user) {
                this.selectedUser = user;
                this.showEditUserForm = true;
            },
            deleteUser(user) {
                // Obsłuż usunięcie użytkownika (np. wysłanie żądania do serwera)
            }
        }
    };
</script>-->
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
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="user in users" :key="user.id">
                <td>{{ user.id }}</td>
                <td>{{ user.login }}</td>
                <td>
                    <button class="btn btn-primary" @click="editUser(user)">Edit</button>
                    <button class="btn btn-danger" @click="removeUser(user)">Delete</button>
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
    import { useAdminStore } from '@/stores';

    const adminStore = useAdminStore();
    const users = computed(() => adminStore.users);

    adminStore.getAll();
    function removeUser(user){
      adminStore.remove(user.id);
    }
</script>
