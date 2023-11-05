<template>
  <div>
    <div class="alert alert-info">
      login: test<br />
      password: test
    </div>
    <h2>SIGN IN</h2>
    <Form @submit="onSubmit" :validation-schema="schema" v-slot="{ errors }">
      <div class="form-group">
        <label>login</label>
        <Field name="login" type="text" class="form-control" :class="{ 'is-invalid': errors.login }" />
        <div class="invalid-feedback">{{ errors.login }}</div>
      </div>
      <div class="form-group">
        <label>password</label>
        <Field name="password" type="password" class="form-control" :class="{ 'is-invalid': errors.password }" />
        <div class="invalid-feedback">{{ errors.password }}</div>
      </div>
      <div class="form-group">
        <button class="btn btn-primary" :disabled="counting">
          <vue-countdown v-if="counting" :time="60000 * 15"
            @end="onCountdownEnd" v-slot="{ minutes, seconds}">user blocked: {{minutes}}:{{seconds}}</vue-countdown>
          <div v-else>
            sign in
          </div>
          <span v-show="false" class="spinner-border spinner-border-sm mr-1"></span>
        </button>
      </div>
      <div v-if="errors.apiError" class="alert alert-danger mt-3 mb-0">{{ errors.apiError }}</div>
    </Form>
  </div>
</template>

<script setup lang="ts">
import { Form, Field, SubmissionHandler } from 'vee-validate';
import { storeToRefs } from 'pinia';
import * as Yup from 'yup';
import { ref } from 'vue';
import { useAuthStore } from '@/stores/auth.store';
import VueCountdown from '@chenfengyuan/vue-countdown'
const schema = Yup.object().shape({
  login: Yup.string().required('login is required'),
  password: Yup.string().required('password is required')
});

const onSubmit: SubmissionHandler = (values: any, { setErrors: any }: any) => {
  const { login, password } = values;
  useAuthStore().signin(login, password).then(increaseLoginAttempt)
  increaseLoginAttempt();
  //.catch(error => setErrors({ apiError: error }));
}
let loginAttempts = 0;
const counting = ref(false)
function increaseLoginAttempt() {
  loginAttempts++;
  if(loginAttempts > 4){
    counting.value = true;
  }
}
function onCountdownEnd() {
  loginAttempts = 0
  counting.value = false;
}
</script>
