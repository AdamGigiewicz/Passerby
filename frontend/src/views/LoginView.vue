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
            <div class="form-group">
                <label>Select pictures with stairs</label>
                <div class="captcha">
                    <img src="../../public/image1.jpg" alt="Image 1" @click="toggleCaptchaSelection(0)" :class="{ 'selected': captchaSelection[0] }" />
                    <img src="../../public/image2.jpg" alt="Image 2" @click="toggleCaptchaSelection(1)" :class="{ 'selected': captchaSelection[1] }" />
                    <img src="../../public/image3.jpg" alt="Image 3" @click="toggleCaptchaSelection(2)" :class="{ 'selected': captchaSelection[2] }" />
                    <img src="../../public/image4.jpg" alt="Image 4" @click="toggleCaptchaSelection(3)" :class="{ 'selected': captchaSelection[3] }" />
                </div>
                <div v-if="wrongCaptcha" class="alert alert-danger mt-3 mb-0">Wrong Captcha</div>
            </div>
        </Form>
    </div>
</template>

<script setup lang="ts">
    import { Form, Field, SubmissionHandler } from 'vee-validate';
    import { ref } from 'vue';
    import * as Yup from 'yup';
    import { useAuthStore } from '@/stores/auth.store';
    import VueCountdown from '@chenfengyuan/vue-countdown';

    const schema = Yup.object().shape({
        login: Yup.string().required('login is required'),
        password: Yup.string().required('password is required'),
        captcha: Yup.array().min(2, 'Select at least 2 images with stairs').of(Yup.boolean()),
    });

    const captchaSelection = ref([false, false, false, false]);

    const correctCaptchaSelection = [true, true, false, false];

    const wrongCaptcha = ref(false);

    const onSubmit: SubmissionHandler = (values: any): void => {
        wrongCaptcha.value = false
        const isCaptchaCorrect = captchaSelection.value.every((selected: boolean, index: number) => {
            return selected === correctCaptchaSelection[index];
        });

        if (isCaptchaCorrect) {
            const { login, password } = values;
            useAuthStore().signin(login, password).then(increaseLoginAttempt);
            increaseLoginAttempt();
        }
        else {
            wrongCaptcha.value = true
        }
    }
    
    function toggleCaptchaSelection(index: number): void {
        captchaSelection.value[index] = !captchaSelection.value[index];
    }

    let loginAttempts = 0;
    const counting = ref(false);

    function increaseLoginAttempt(): void {
        loginAttempts++;
        if (loginAttempts > 4) {
            counting.value = true;
        }
    }

    function onCountdownEnd(): void {
        loginAttempts = 0;
        counting.value = false;
    }
</script>

<style scoped>
    .captcha img {
        width: 200px; /* Dostosuj rozmiar obrazków captcha do swoich potrzeb */
        height: 200px;
        margin: 5px;
        cursor: pointer;
    }

        .captcha img.selected {
            border: 2px solid blue; /* Zaznaczenie obrazka z schodami */
        }
</style>
