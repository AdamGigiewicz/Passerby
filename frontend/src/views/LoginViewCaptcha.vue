<template>
    <div>
        <h2>SIGN IN</h2>
        <Form @submit="onSubmit" :validation-schema="schema" v-slot="{ errors }">
            <div class="form-group">
                <label>login</label>
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
                <label>one-time password</label>
                <Field name="password" type="password" class="form-control" :class="{ 'is-invalid': errors.password }" />
                <div class="invalid-feedback">{{ errors.password }}</div>
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

    import { useAuthStore } from '@/stores/auth.store';

    const schema = Yup.object().shape({
        login: Yup.string().required('login is required'),
        password: Yup.string().required('password is required')
    });

    const generateRandomNumber = () => {
        return Math.floor(Math.random() * 100) + 1;
    };

    const randomNumbers = {
        randomNumber1: ref(generateRandomNumber()),
        randomNumber2: ref(generateRandomNumber())
    };

    const onSubmit: SubmissionHandler = (values: any, { setErrors: any }: any) => {
        const { login, password } = values;
        return useAuthStore().signin(login, password)
        //.catch(error => setErrors({ apiError: error }));
    }
</script>
