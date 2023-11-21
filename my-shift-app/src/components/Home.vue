<template>
    <v-app>
      <v-navigation-drawer>
        <v-list>
          <v-list-item v-for="user in users" :key="user.id" @click="selectUser(user)">
            <v-list-item-title>{{ user.name + " " + user.surname}}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-navigation-drawer>
  
      <!-- Contenu principal à droite du drawer -->
      <v-main>
        <v-row class="fill-height">
          <v-col>
            <v-sheet height="600">
              <FullCalendar :options="calendarOptions"/>
            </v-sheet>
          </v-col>
        </v-row>
      </v-main>
    </v-app>
  </template>

<script>
import axios from 'axios';
import FullCalendar from '@fullcalendar/vue3'
import dayGridPlugin from '@fullcalendar/daygrid'

export default {
name: 'Home',
components: {
  FullCalendar // make the <FullCalendar> tag available
},
data() {
  return {
    calendarOptions: {
      plugins: [ dayGridPlugin ],
      initialView: 'dayGridMonth',
      allDay: true,
      displayEventEnd: true
    },
    drawer: true,
    users: [],
    selectedUser: null,
    calendarEvents: []
  };
},
mounted() {
  this.fetchUsers();
},
methods: {
  fetchUsers() {
    axios
      .get('https://localhost:7094/Users')
      .then((response) => {
        this.users = response.data;
      })
      .catch((error) => {
        console.error('Erreur lors de la récupération des utilisateurs', error);
      });
  },

  selectUser(user) {
    this.selectedUser = user;
    this.fetchShifts(user.id);
  },

  fetchShifts(userId) {
axios
  .get(`https://localhost:7094/Users/${userId}/shift`)
  .then((response) => {
    this.shifts = response.data;
    const events = response.data.map(shift => ({
      id: shift.id,
      title: "Disponible",
      start: new Date(shift.start).toISOString(),
      end: new Date(shift.end).toISOString() 
    }));
    this.calendarOptions.events = events;
    this.calendarOptions.eventTimeFormat = {
   hour: '2-digit',
   minute: '2-digit',
   hour12: false
  };
    // Vérification avant d'accéder à la référence
    // if (this.$refs.fullCalendarRef) {
    //   this.$refs.fullCalendarRef.getApi().refetchEvents(); // Exemple de méthode pour rafraîchir les événements
    // }
  })
  .catch((error) => {
    console.error('Erreur lors de la récupération des utilisateurs', error);
  });
}

}
};
</script>

<style>
</style>
  