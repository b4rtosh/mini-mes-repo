<script>
import '@/assets/forms.css';
import axios from "axios";
export default{
  name: 'OrderUptForm',
  props: {
    route: {
      type: String,
      default: '',
    },
    machine: {
      type: Object,
      default: null,
    },
  },
  data(){
    return{
      localObject: {...this.machine},
      machines: [],
      products: [],
    }

  },
  created(){
    this.getAllMachines();
    this.getAllProducts();
  },
  methods: {
    submitForm() {
      this.$emit('submitForm', this.localObject);
    },
    async getAllMachines(){
      this.machines = await axios.get(`${this.route}/Machine/all`)
          .then(response => response.data.$values)
          .catch(error => console.log(error));
      console.log(this.machines);
    },
    async getAllProducts(){
      this.products = await axios.get(`${this.route}/Product/all`)
          .then(response => response.data.$values)
          .catch(error => console.log(error));
      console.log(this.products);
    },
  },
}

</script>

<template>
  <!--  add machine dialog which covers list -->
  <div>
    <h1>Update Order</h1>
    <form @submit.prevent="submitForm">
      <div>
        <label for="codeInput">Code: </label>
        <input class="inputTxt" 
               type="text" 
               id="codeInput" 
               v-model="localObject.code" 
               pattern="^(?!\s)[\w]+(?<!\s)$"
               placeholder="Letters, digits"
               required><br>
      </div>
      <div>
        <label for="quantityInput">Quantity: </label>
        <input class="inputTxt" 
               type="number" 
               id="quantityInput"
               placeholder="Number"
               pattern="^(?!\s)[\w]+(?<!\s)$"
               v-model="localObject.quantity" 
               required><br>
      </div>
      <div>
        <label for="machine-select">Machine: </label>
        <select id="machine-select" v-model="localObject.machineId">
          <option v-for="machine in machines" :key="machine.id" :value="machine.id">
            {{machine.name}}
          </option>
        </select>
      </div>
      <div>
        <label for="product-select">Product: </label>
        <select id="product-select" v-model="localObject.productId">
          <option v-for="product in products" :key="product.id" :value="product.id">
            {{product.code}}
          </option>
        </select>
      </div>
      <div class='buttons'>
        <button type="submit">Save</button>
        <button type="reset" @click="$emit('cancelForm')">Cancel</button>
      </div>
    </form>
  </div>
</template>

<style scoped>

</style>