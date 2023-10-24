<template>
  <div>
    <h2>Edit User</h2>
    <Form @submit="onSubmit" class="user-form" :validation-schema="schema">
      <template v-slot="{ errors, isSubmitting }">
        <div class="form-group">
          <label for="login">Login</label>
          <Field name="login" type="text" class="form-control" v-model="user.login"
            :class="{ 'is-invalid': errors.login }" />
          <div class="invalid-feedback">{{ errors.login }}</div>
        </div>
        <div class="form-group">
          <label for="password">Password</label>
          <Field name="password" type="password" class="form-control" v-model="user.password"
            :class="{ 'is-invalid': errors.password }" />
          <div class="invalid-feedback">{{ errors.password }}</div>
        </div>
        <div class="form-group form-check">
          <input type="checkbox" id="isAdmin" class="form-check-input" v-model="user.isAdmin" />
          <label for="isAdmin" class="form-check-label">Admin</label>
        </div>
        <div class="form-group form-check">
          <input type="checkbox" id="isBlocked" class="form-check-input" v-model="user.blocked" />
          <label for="isBlocked" class="form-check-label">Blocked</label>
        </div>
        <div class="form-group form-check">
          <input type="checkbox" id="passwordCriteria" class="form-check-input" v-model="user.passwordCriteria" />
          <label for="passwordCriteria" class="form-check-label">Criteria</label>
        </div>
        <!--<VueDatePicker v-model="date" class="date-picker"></VueDatePicker>-->
        <div class="form-group">
          <button class="btn btn-primary" :disabled="isSubmitting">
            <span v-show="isSubmitting" class="spinner-border spinner-border-sm mr-1"></span>
            Edit
          </button>
        </div>
        <div v-if="errors.apiError" class="alert alert-danger mt-3 mb-0">{{ errors.apiError }}</div>
      </template>
    </Form>
  </div>
</template>

<script setup>

import { Form, Field } from 'vee-validate';
import * as Yup from 'yup';
import { ref } from 'vue';
import { useAdminStore } from '@/stores';
import { storeToRefs } from 'pinia';
import { router } from '@/helpers';

const schema = Yup.object().shape({
  login: Yup.string().required('login is required'),
  password: Yup.string().required('password is required')
});

const adminStore = useAdminStore();
const user = ref(await adminStore.getUserToEdit())

function onSubmit({ setErrors }) {
  adminStore.edit(user.value)
  //   .catch(error => setErrors({ apiError: error }));
  router.push('/admin')
}

</script>
<style scoped>
.user-form {
  max-width: 300px;
  margin: 0 auto;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
  background-color: #fff;
}

.form-group {
  margin-bottom: 15px;
}

.form-check-label {
  padding-left: 5px;
}

.date-picker {
  width: 100%;
}
</style>
