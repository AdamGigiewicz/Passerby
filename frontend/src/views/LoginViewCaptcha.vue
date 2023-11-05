<template>
  <div>
    <h2>SIGN IN</h2>
    <Form @submit="onSubmit" :validation-schema="schema" v-slot="{ errors }">
      <div class="form-group">
        <label>Login</label>
        <Field name="login" type="text" class="form-control" :class="{ 'is-invalid': errors.login }" />
        <div class="invalid-feedback">{{ errors.login }}</div>
      </div>
      <div class="form-group">
        <label>Function a/x</label>
        <div>
          <label>Random Numbers:</label>
        </div>
        <div>{{ randomNumbers.randomNumber1 }} / {{ randomNumbers.randomNumber2 }}</div>
      </div>
      <div class="form-group">
        <label>One-time password</label>
        <Field name="password" type="password" class="form-control" :class="{ 'is-invalid': errors.password }" />
        <div class="invalid-feedback">{{ errors.password }}</div>
      </div>
      <div class="form-group">
        <label for="newPassword">New Password</label>
        <Field type="password" name="newPassword" class="form-control" :class="{ 'is-invalid': errors.newPassword }" />
        <div class="invalid-feedback">{{ errors.newPassword }}</div>
      </div>
      <div class="form-group">
        <label for="confirmPassword">Confirm New Password</label>
        <Field type="password" name="confirmPassword" class="form-control"
          :class="{ 'is-invalid': errors.confirmPassword }" />
        <div class="invalid-feedback">{{ errors.confirmPassword }}</div>
      </div>
      <div class="form-group">
        <button class="btn btn-primary" :disabled="false">
          <span v-show="false" class="spinner-border spinner-border-sm mr-1"></span>
          Sign In
        </button>
      </div>
      <div v-if="errors.apiError" class="alert alert-danger mt-3 mb-0">{{ errors.apiError }}</div>
    </Form>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { Form, Field, SubmissionHandler } from 'vee-validate';
import { storeToRefs } from 'pinia';
import * as Yup from 'yup';

import { router } from '@/helpers/router';
import { useAuthStore } from '@/stores/auth.store';

const schema = Yup.object().shape({
  login: Yup.string().required('login is required'),
  password: Yup.string().required('password is required'),
  newPassword: Yup.string().required('new password is required'),
  confirmPassword: Yup.string().required('password confirmation is required').oneOf([Yup.ref('newPassword'), null], 'passwords must match')
});

const generateRandomNumber = () => {
  return Math.floor(Math.random() * 100) + 1;
};

const randomNumbers = {
  randomNumber1: ref(generateRandomNumber()),
  randomNumber2: ref(generateRandomNumber())
};

const result = (randomNumbers.randomNumber1.value / randomNumbers.randomNumber2.value).toFixed(1).toString();

console.log(result);


const onSubmit: SubmissionHandler = (values: any, { setErrors: any }: any) => {
  const { login, password, newPassword, confirmPassword } = values;
  if (values.password == result) {
    useAuthStore().resetPassword(login, newPassword)
  router.push('/login')
  } else {
    alert("Wrong one-time password")
  }
  //.catch(error => setErrors({ apiError: error }));
}
</script>
