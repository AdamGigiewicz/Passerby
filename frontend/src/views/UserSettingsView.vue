<template>
  <h1>Hi!</h1>
  <div class="password-change-form">
    <h2>Change Password</h2>
    <Form @submit="onSubmit" :validation-schema="schema">
      <template v-slot="{ errors, isSubmitting }">
        <div class="form-group">
          <div class="form-item">
            <label for="oldPassword">Current Password </label>
            <Field type="password" name="oldPassword" class="form-control"
              :class="{ 'is-invalid': errors.currentPassword }" />
            <div class="invalid-feedback">{{ errors.oldPassword }}</div>
          </div>
        </div>
        <br>
        <div class="form-group">
          <label for="newPassword">New Password</label>
          <Field type="password" name="newPassword" class="form-control" :class="{ 'is-invalid': errors.newPassword }" />
          <div class="invalid-feedback">{{ errors.newPassword }}</div>
        </div>
        <br>
        <div class="form-group">
          <label for="confirmPassword">Confirm New Password</label>
          <Field type="password" name="confirmPassword" class="form-control"
            :class="{ 'is-invalid': errors.confirmPassword }" />
          <div class="invalid-feedback">{{ errors.confirmPassword }}</div>
        </div>
        <div class="form-group">
          <button class="btn btn-primary" :disabled="isSubmitting">
            <span v-show="isSubmitting" class="spinner-border spinner-border-sm mr-1"></span>
            Change Password
          </button>
        </div>
        <div v-if="errors.apiError" class="alert alert-danger mt-3 mb-0">{{ errors.apiError }}</div>
      </template>
    </Form>
    <button @click="recaptcha">Execute recaptcha</button>
  </div>
</template>

<script setup lang="ts">
import { useAuthStore } from '@/stores/auth.store';
import { Form, Field, SubmissionHandler } from 'vee-validate';
import * as Yup from 'yup';
import { router } from '@/helpers/router';
import { VueReCaptcha, useReCaptcha } from 'vue-recaptcha-v3'
const schema = Yup.object().shape({
  oldPassword: Yup.string().required('current password is required'),
  newPassword: Yup.string().required('new password is required'),
  confirmPassword: Yup.string().required('password confirmation is required').oneOf([Yup.ref('newPassword'), null], 'passwords must match')
});

const { executeRecaptcha, recaptchaLoaded} = useReCaptcha();

const recaptcha = async () => {
  // (optional) Wait until recaptcha has been loaded.
  await recaptchaLoaded()

  // Execute reCAPTCHA with action "login".
  return await executeRecaptcha('login') ;

}
const onSubmit: SubmissionHandler = async (values: any, { setErrors: any }: any) => {
  const { oldPassword, newPassword } = values;
  const token = await recaptcha();
  await useAuthStore().editPassword(oldPassword, newPassword, token)
  //  .catch(error => setErrors({ apiError: error }));
  router.push('/')
}
</script>
