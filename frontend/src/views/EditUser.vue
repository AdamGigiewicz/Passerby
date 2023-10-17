<script setup>
    import { Form, Field } from 'vee-validate';
    import * as Yup from 'yup';

    import { useAuthStore } from '@/stores';
    import { ref } from 'vue';
    import VueDatePicker from '@vuepic/vue-datepicker';
    import '@vuepic/vue-datepicker/dist/main.css'

    const date = ref(new Date());

    function onSubmit(values) {
        const authStore = useAuthStore();
        const { login, password } = values;

        return authStore.create(login, password)
    }
</script>

<template>
    <div>
        <h2>Edit</h2>
        <form @submit="onSubmit">
            <div class="form-group">
                <label>Login</label>
                <field name="login" type="text" class="form-control" />
            </div>
            <div class="form-group">
                <label>Password</label>
                <field name="password" type="password" class="form-control" />
            </div>
            <div class="form-group">
                <label>Role</label>
                <input type="checkbox" value="" />
            </div>
            <div class="form-group">
                <label>Blocked</label>
                <input type="checkbox" value="" />
            </div>
            <div class="form-group">
                <label>Criteria</label>
                <input type="checkbox" value="" />
            </div>
            <div class="form-group">
                <label>Criteria</label>
                <input type="checkbox" value="" />
            </div>
            <VueDatePicker v-model="date"></VueDatePicker>
            <div class="form-group">
                <button class="btn btn-primary">
                    <span class="spinner-border spinner-border-sm mr-1"></span>
                    sign in
                </button>
            </div>
        </form>
    </div>
</template>
