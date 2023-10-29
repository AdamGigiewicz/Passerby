<template>
  <div class="password-change">
    <h1>Hi!</h1>
    <button class="btn btn-primary" @click="changePassword()">Change password</button>
  </div>
</template>

<script setup lang="ts">
import { router } from '@/helpers/router';
function changePassword() {
  router.push('/password')
}
</script>
