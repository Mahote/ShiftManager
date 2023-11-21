<template>
  <v-app>
    <v-app-bar>
      <v-toolbar-title>ManagementShift</v-toolbar-title>
      <v-btn text @click="changeView('Home')">Home</v-btn>
      <v-btn text @click="changeView('Scheduler')">Scheduler</v-btn>
    </v-app-bar>
    <v-main>
      <component :is="currentView"/>
    </v-main>
  </v-app>
</template>

<script>
import Home from './Home.vue';
import Scheduler from './Scheduler.vue'; 


export default {
  name: 'Main',
  components: {
    Home,
    Scheduler,
  },
  data() {
    return {
      currentView: 'Home', // La vue initiale à afficher
    };
  },
  methods: {
    changeView(viewName) {
      this.currentView = viewName;
    },
  },
};
</script>

<style>
/* Vos styles */
</style>
