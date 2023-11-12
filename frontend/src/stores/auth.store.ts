import { defineStore } from 'pinia';
import { fetchWrapper } from '@/helpers/fetch-wrapper';
import { router } from '@/helpers/router';
import { ref } from 'vue';
import {encrypt, decrypt} from '@/composables/cipher'; 

const baseUrl = `${import.meta.env.VITE_API_URL}/user`;

export const useAuthStore = defineStore('auth', () => {
  const token = ref("");
  const returnUrl = ref();
  const executions = ref(0);
  loadToken();
  loadExecutions();

  function unlock(key: string){
    if(decrypt(key, getShift()) == getSimpleToken()){ 
      executions.value = 0;
      saveExecutions;
      alert("success: provided key was correct")
    }
    else{alert("failure: provided key was incorrect")}
  }
  function getKey(){
    return encrypt(getSimpleToken(), getShift());
  }
  function getShift(){
    return token.value.length % 26;
  }
  function getSimpleToken(){
    return token.value.toLowerCase().replace(/[^a-zA-Z]/g, '');
  }
  function loadExecutions() {
    const storedExecutions = localStorage.getItem('executions');
    if (storedExecutions != null) {
      executions.value = parseInt(storedExecutions)
    }
  }

  function addExecution() {
    executions.value++;
    saveExecutions();
  }

  function saveExecutions(){

    localStorage.setItem('executions', executions.value.toString())
  }

  function loadToken() {
    const storedToken = localStorage.getItem('token');
    if (storedToken != null) {
      token.value = storedToken
    }
  }

  function saveToken(tokenToSave: string) {
    localStorage.setItem('token', tokenToSave);
  }

  async function signin(login: string, password: string) {
    const receivedToken = (await fetchWrapper.post(baseUrl, { login, password })).token;
    saveToken(receivedToken)
    loadToken();
    router.push(returnUrl.value || '/');
  }

  async function resetPassword(login: string, password: string) {
    await fetchWrapper.patch(baseUrl, { login, password });
  }
  async function editPassword(oldPassword: string, newPassword: string, token: string) {
    await fetchWrapper.put(baseUrl, { oldPassword, newPassword, token });
  }

  async function getFiles() {
    const files = await fetchWrapper.get(baseUrl + "/files") as unknown as {files: string};
    const filesArray: string[] = files.files.split(',');
    return filesArray;
  }

  async function setFiles(filesArray: string[]) {
    const files: string = filesArray.toString();
    await fetchWrapper.patch(baseUrl + "/files", { files });
  }

  function signout() {
    token.value = "";
    localStorage.removeItem('token');
    router.push('/login');
  }

  return {unlock, getKey, addExecution, executions, token, signin, getFiles, setFiles, editPassword, signout, returnUrl, resetPassword }
});
