<template>
    <div>
        <h2>Edit User</h2>
        <form @submit="onSubmit" class="user-form">
            <div class="form-group">
                <label for="login">Login</label>
                <Field name="login" type="text" class="form-control" />
            </div>
            <div class="form-group">
                <label for="password">Password</label>
                <Field name="password" type="password" class="form-control" />
            </div>
            <div class="form-group form-check">
                <input type="checkbox" id="role" class="form-check-input" value="" />
                <label for="role" class="form-check-label">Role</label>
            </div>
            <div class="form-group form-check">
                <input type="checkbox" id="blocked" class="form-check-input" value="" />
                <label for="blocked" class="form-check-label">Blocked</label>
            </div>
            <div class="form-group form-check">
                <input type="checkbox" id="criteria" class="form-check-input" value="" />
                <label for="criteria" class="form-check-label">Criteria</label>
            </div>
            <VueDatePicker v-model="date" class="date-picker"></VueDatePicker>
            <div class="form-group">
                <button class="btn btn-primary">
                    <span class="spinner-border spinner-border-sm mr-1"></span>
                    Edit
                </button>
            </div>
        </form>
    </div>
</template>

<style scoped>
    .user-form {
        max-width: 300px;
        margin: 0 auto;
        padding: 20px;
        border: 1px solid #ccc;
        border-radius: 5px;
        background-color: #fff;
    }

    .form-group {
        margin-bottom: 15px;
    }

    .form-check-label {
        padding-left: 5px;
    }

    .date-picker {
        width: 100%;
    }
</style>
