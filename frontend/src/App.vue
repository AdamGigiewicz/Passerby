<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router';
import { useAuthStore } from '@/stores/auth.store';
import {storeToRefs} from 'pinia';

const authStore = useAuthStore();
const {token}= storeToRefs(authStore);

</script>

<template>
  <suspense>
    <div class="app-container bg-light">
      <nav v-show="token != null && token.length > 0" class="navbar navbar-expand navbar-dark bg-dark">
            <div class="navbar-nav">
                <RouterLink to="/" class="nav-item nav-link">Home</RouterLink>
                <a @click="authStore.signout()" class="nav-item nav-link">Logout</a>
            </div>
        </nav>
        <div class="container pt-4 pb-4">
            <RouterView />
        </div>
    </div>
  </suspense>
</template>

<style>
@import '@/assets/base.css';
</style>
