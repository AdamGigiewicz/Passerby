<template>
  <div class="password-change">
    <h1>Hi!</h1>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useAuthStore } from '@/stores';

const authStore = useAuthStore();
const currentPassword = ref('');
const newPassword = ref('');
const confirmPassword = ref('');

const changePassword = async () => {
  if (newPassword.value !== confirmPassword.value) {
    alert("New password and confirm password do not match.");
    return;
  }

  try {
    await authStore.changePassword(currentPassword.value, newPassword.value);
    alert("Password changed successfully.");
    currentPassword.value = '';
    newPassword.value = '';
    confirmPassword.value = '';
  } catch (error) {
    alert("Password change failed. Error: " + error.message);
  }
};
</script>
