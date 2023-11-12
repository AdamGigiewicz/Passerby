<template>
  <div class="password-change">
    <h1>Hi!</h1>
    <button class="btn btn-primary" @click="openFiles()">Edit files</button>
    <br>
    <button class="btn btn-secondary" @click="changePassword()">Change password</button>
  </div>
</template>

<script setup lang="ts">
import { router } from '@/helpers/router';
import { useAuthStore } from '@/stores/auth.store';

function changePassword() {
  router.push('/password')
}
function openFiles() {
  if (useAuthStore().executions < 4) {
    useAuthStore().addExecution();
    router.push('/files')
  }
  else {
    alert("execution limit exceeded")
  }
}
</script>
