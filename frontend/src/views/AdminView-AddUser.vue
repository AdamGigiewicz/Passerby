<template>
    <div class="add-user-form">
        <h3>Add User</h3>
        <form @submit.prevent="saveUser">
            <div class="form-group">
                <label for="username">Username</label>
                <input type="text" id="username" v-model="newUser.username" required>
            </div>
            <div class="form-group">
                <label for="password">Password</label>
                <input type="password" id="password" v-model="newUser.password" required>
            </div>
            <button type="submit" class="btn btn-success">Save</button>
        </form>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                newUser: {
                    username: '',
                    password: ''
                }
            };
        },
        methods: {
            saveUser() {
                // Obs³u¿ logikê dodawania u¿ytkownika
                // Mo¿esz wys³aæ dane na serwer lub dodaæ u¿ytkownika do lokalnej listy
            }
        }
    };
</script>

<style scoped>
    /* Dodaj style CSS dla formularza tu */
</style>
