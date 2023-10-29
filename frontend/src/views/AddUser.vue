<template>
  <div class="user-form">
    <h2>Add User</h2>
    <Form @submit="onSubmit" :validation-schema="schema">
      <template v-slot="{ errors, isSubmitting }">
        <div class="form-group">
          <label for="login">Login</label>
          <Field name="login" type="text" class="form-control" :class="{ 'is-invalid': errors.login }" />
          <div class="invalid-feedback">{{ errors.login }}</div>
        </div>
        <div class="form-group">
          <label for="password">Password</label>
          <Field name="password" type="password" class="form-control" :class="{ 'is-invalid': errors.password }" />
          <div class="invalid-feedback">{{ errors.password }}</div>
        </div>
        <div class="form-group">
          <button class="btn btn-primary" :disabled="isSubmitting">
            <span v-show="isSubmitting" class="spinner-border spinner-border-sm mr-1"></span>
            Add User
          </button>
        </div>
        <div v-if="errors.apiError" class="alert alert-danger mt-3 mb-0">{{ errors.apiError }}</div>
      </template>
    </Form>
  </div>
</template>

<script setup lang="ts">
import { Form, Field, SubmissionHandler } from 'vee-validate';
import { useAdminStore } from '@/stores/admin.store';
import { router } from '@/helpers/router';
import * as Yup from 'yup';

const schema = Yup.object().shape({
  login: Yup.string().required('login is required'),
  password: Yup.string().required('password is required')
});

const onSubmit: SubmissionHandler = (values: any, { setErrors: any }: any) => {
  const adminstore = useAdminStore();
  adminstore.add(values.login, values.password)
  //  .catch(error => setErrors({ apiError: error }));
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
