<script>
import axios from 'axios'
import ProcStatusList from "@/components/ProcStatusList.vue";
import ProcStatusDetails from "@/components/ProcStatusDetails.vue";
import ProcStatusForm from "@/components/ProcStatusForm.vue";
import '@/assets/all.css'
import {API_URL} from "@/main.js";

export default {
  name: 'Statuses',
  components: {
    ProcStatusList,
    ProcStatusDetails,
    ProcStatusForm,
  },
  data(){
    return{
      objects: [],
      showForm: false,
      showDetails: false,
      showUptForm: false,
      selectedObject: null,
      route: `${API_URL}/Status`
    }
  },
  created(){
    this.getAllObjects();
  },
  methods: {
    async getAllObjects(){
      this.objects = await axios.get(`${this.route}/all`)
          .then(response => response.data.$values)
          .catch(error => console.log(error));
    },
    async addObject(newObject){
      try {
        await axios.put(`${this.route}/add`, newObject)
        await this.getAllObjects();
        this.closeForm();
      } catch (error){
        console.log('Error', error);
      }
    },

    async deleteObject(object){
      console.log(object);
      await axios.delete(`${this.route}/delete/int/${object.id}`)
          .then(response => response.data)
          .catch(error => console.log('Error', error));
      this.closeForm();
      await this.getAllObjects();
    },

    openForm(){
      this.showForm = true;
      this.showDetails = false;
    },
    closeForm(){
      this.showForm = false;
      this.showDetails = false;
    },
    openDetails(product){
      this.selectedObject = product;
      this.showDetails = true;
      this.showForm = false;
    },
    closeDetails(){
      this.showDetails = false;
      this.showForm = false;
    },
  }
}

</script>

<template>

  <ProcStatusList
      v-if="!showForm && !showDetails && !showUptForm"
      :objects="objects"
      @show-form="openForm"
      @show-details="openDetails"
      @refresh="getAllObjects"
  />
  <ProcStatusForm
      v-if="showForm"
      @add-input="addObject"
      @cancel-form="closeForm"
  />
  <ProcStatusDetails
      v-if="showDetails"
      :id="selectedObject.id"
      @cancel-details="closeDetails"
      @delete="deleteObject"
      :route="route"
  />

</template>

<style>

</style>