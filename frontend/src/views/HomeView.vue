<script setup>
    import { ref } from 'vue';
    import { useAuthStore } from '@/stores';

    const authStore = useAuthStore();
    const currentPassword = ref('');
    const newPassword = ref('');
    const confirmPassword = ref('');

    const changePassword = async () => {
        // Tutaj mo¿esz dodaæ logikê zmiany has³a
        if (newPassword.value !== confirmPassword.value) {
            // SprawdŸ, czy nowe has³o i potwierdzenie has³a s¹ zgodne
            alert("New password and confirm password do not match.");
            return;
        }

        try {
            // Wywo³aj API lub inne metody do zmiany has³a
            await authStore.changePassword(currentPassword.value, newPassword.value);
            alert("Password changed successfully.");
            currentPassword.value = '';
            newPassword.value = '';
            confirmPassword.value = '';
        } catch (error) {
            alert("Password change failed. Error: " + error.message);
        }
    };
</script>


<template>
    <div class="password-change">
        <h1>Hi {{authUser?.firstName}}!</h1>

        <div class="password-change-form">
            <h2>Change Password</h2>
            <form @submit.prevent="changePassword">
                <div class="form-group">
                    <div class="form-item">
                        <label for="currentPassword">Current Password </label>
                        <input type="password" id="currentPassword" v-model="currentPassword" required>
                    </div>
                </div>
                <br>
                <div class="form-group">
                    <label for="newPassword">New Password</label>
                    <input type="password" id="newPassword" v-model="newPassword" required>
                </div>
                <br>
                <div class="form-group">
                    <label for="confirmPassword">Confirm New Password</label>
                    <input type="password" id="confirmPassword" v-model="confirmPassword" required>
                </div>
                <button type="submit" class="btn btn-primary">Change Password</button>
            </form>
        </div>
    </div>
</template>



