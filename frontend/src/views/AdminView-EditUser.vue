<template>
    <div class="edit-user-form">
        <h3>Edit User</h3>
        <form @submit.prevent="updateUser">
            <div class="form-group">
                <label for="username">Username</label>
                <input type="text" id="username" v-model="editedUser.username" required>
            </div>
            <div class="form-group">
                <label for="password">Password</label>
                <input type="password" id="password" v-model="editedUser.password" required>
            </div>
            <button type="submit" class="btn btn-primary">Save Changes</button>
        </form>
    </div>
</template>

<script>
    export default {
        props: {
            user: Object // Przekazuje obiekt u¿ytkownika do edycji
        },
        data() {
            return {
                editedUser: { ...this.user } // U¿yj kopii obiektu u¿ytkownika do edycji
            };
        },
        methods: {
            updateUser() {
                // Obs³u¿ logikê aktualizacji u¿ytkownika
                // Mo¿esz wys³aæ zmienione dane na serwer lub zaktualizowaæ lokalny stan
            }
        }
    };
</script>

<style scoped>
    /* Dodaj style CSS dla formularza edycji u¿ytkownika tu */
</style>
