<script setup>
    import { Form, Field } from 'vee-validate';
    import * as Yup from 'yup';

    import { useAdminStore } from '@/stores';

    const schema = Yup.object().shape({
        login: Yup.string().required('login is required'),
        password: Yup.string().required('password is required')
    });

    function onSubmit(values, { setErrors }) {
        const adminstore = useAdminStore();
        const { login, password } = values;

        return adminstore.add(login, password)
            .catch(error => setErrors({ apiError: error }));
    }
</script>

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

