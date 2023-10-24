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
          <th>Id</th>
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
import { storeToRefs } from 'pinia';
import {router} from '@/helpers';
const adminStore = useAdminStore();
const { users } = storeToRefs(adminStore);

adminStore.getAll();

function addUser(){
 router.push('/add')
}

function editUser(user) {
  adminStore.setUserToEdit(user.id);
  router.push('/edit')
}

function removeUser(user) {
  adminStore.remove(user.id);
}
</script>
