<script>
import '@/assets/details.css'
import '@/assets/buttons.css'
import Delete from "@/components/Delete.vue";
import ParameterUptForm from "@/components/ParameterUptForm.vue";
import axios from 'axios';


export default{
  name: "ParameterDetails",
  components: {ParameterUptForm, Delete},
  props: ['id', 'route'],
  created(){
    this.getDetailedObject();
  },
  data(){
    return{
      showDelete: false,
      showForm: false,
      selectedObject: null,
    }
  },
  methods:{
    openForm(){
      this.showForm = true;
    },
    async getDetailedObject(){
      this.selectedObject = await axios.get(`${this.route}/int/${this.id}`)
          .then(response => response.data)
          .catch(error => console.log(error));
    },
    async updateObject(updatedObject){
      this.selectedObject.name = updatedObject.name;
      this.selectedObject.unit = updatedObject.unit;
      this.showForm= false;
      console.log(updatedObject);
      await axios.post(`${this.route}/update`, updatedObject)
          .then(response => response.data)
          .catch(error => console.log(error));
    }

  }
}
</script>

<template>
  <div v-if="selectedObject && !showForm">
    <h1>Parameter details</h1>
    <p>Id: {{ selectedObject.id }}</p>
    <p>Name: {{ selectedObject.name }}</p>
    <p>Unit: {{ selectedObject.unit }}</p>
      <h2>Process parameters:</h2>
    <table class="listOfObjects">
      <tr>
        <th>Id</th>
        <th>Value</th>
        <th>Process Id:</th>
      </tr>
      <tr v-for="processParam in selectedObject.processParameters.$values" :key="processParam.id" data-test="order">
        <td>{{ processParam.id }}</td>
        <td>{{ processParam.value }}</td>
        <td>{{ processParam.processId }}</td>
      </tr>
    </table>
    <div class="buttons">
      <button type="reset" @click="$emit('cancel-details')">Cancel</button>
      <button type="button" @click="showDelete = true">Delete</button>
      <button type="button" @click="openForm">Update</button>
    </div>
    <Delete
        v-if="showDelete"
        @cancel-delete="showDelete = false"
        @submit-delete="$emit('delete', selectedObject)"
    />
  </div>
  <ParameterUptForm
      v-if="showForm"
      :selectedObject="selectedObject"
      @cancelForm="showForm = false"
      @submitForm="updateObject"
  />
</template>

<style scoped>

</style>