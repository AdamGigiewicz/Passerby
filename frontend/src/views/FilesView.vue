<template>
  <div>
    <h1>Files list</h1>
    <div class="d-flex justify-content-end mt-3">
      <button class="btn btn-success" @click="addFile">New file</button>
    </div>
    <table class="table">
      <thead>
        <tr>
          <th>Name</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="file in files">
          <td>{{ file }}</td>
          <td>
            <button class="btn btn-primary" @click="editFile(file)">Edit</button>
            <button class="btn btn-danger" @click="removeFile(file)">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import { Ref, ref, computed } from 'vue';
import { useAuthStore } from '@/stores/auth.store';
import { storeToRefs } from 'pinia';
import { router } from '@/helpers/router';
const files: Ref<string[]> = ref(await useAuthStore().getFiles())
let name = 0;

function addFile() {
  files.value.push("new_file_" + (name++).toString() + ".txt");
  save();
}

function editFile(file: string) {
  let newName = prompt("edit file name", file)
  if (newName && files.value.filter(x => x == newName).length == 0) {
    files.value[files.value.indexOf(file)] = newName;
    save();
  }
}

function removeFile(file: string) {
  files.value = files.value.filter(x => x != file);
  save();
}
function save() {
  useAuthStore().setFiles(files.value);
}
</script>
