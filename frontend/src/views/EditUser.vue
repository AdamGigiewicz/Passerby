<script setup>
import { Form, Field } from 'vee-validate';
import * as Yup from 'yup';

import { useUsersStore} from '@/stores';
import { ref } from 'vue';
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'

const authStore = useUsersStore();
const user = ref(await authStore.getById(1));
const date = ref(new Date());

async function onSubmit() {
  await authStore.edit(user.value.login, user.value.password) 
        .catch(error => alert(error));
}
</script>

<template>
  <div>
    <h2>Edit</h2>
    <form @submit="onSubmit">
      <div class="form-group">
        <label>Login</label>
        <field name="login" type="text" class="form-control" v-model="user.login"  />
      </div>
      <div class="form-group">
        <label>Password</label>
        <field name="password" type="password" class="form-control" v-model="user.password" />
      </div>
      <div class="form-group">
        <label>Role</label>
        <input name="role" type="checkbox" value="" v-model="user.role" />
      </div>
      <div class="form-group">
        <label>Blocked</label>
        <input name="blocked" type="checkbox" value="" v-model="user.blocked" />
      </div>
      <div class="form-group">
        <label>Criteria</label>
        <input name="criteria" type="checkbox" value="" v-model="user.criteria" />
      </div>
      <div class="form-group">
        <label>Criteria</label>
      <VueDatePicker name="resetDate" v-model="date"></VueDatePicker>
      </div>
      <div class="form-group">
        <button  class="btn btn-primary">
          <span class="spinner-border spinner-border-sm mr-1"></span>
          sign in
        </button>
      </div>
    </form>
  </div>
</template>
