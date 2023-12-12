<template>
    <v-app>
      <v-navigation-drawer>
        
        <add-user-dialog @save="addUser"></add-user-dialog>
        <div class="mt-4"></div> <!-- Ajout d'un espace de 4 unités entre les composants -->
        <add-shift-dialog @save-shift="addShift"></add-shift-dialog>

        <v-list>
          <v-list-item v-for="user in users" :key="user.id">
            <v-list-item-content>
              <v-list-item-action>
                <v-btn icon size="small" variant="plain" @click="removeUser(user)">
                  <v-icon color="red">mdi-close</v-icon>
                </v-btn>
                <v-btn variant="text" @click="selectUser(user)">
                  {{ user.name + " " + user.surname }}
                </v-btn>
              </v-list-item-action>
            </v-list-item-content>
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
      <EventDialog :user="selectedUser" ref="eventDialog" />
    </v-app>
  </template>

<script>
import AddUserDialog from './AddUserDialog.vue'
import EventDialog from './EventDialog.vue'
import AddShiftDialog from './AddShiftDialog.vue'
import axios from 'axios'
import FullCalendar from '@fullcalendar/vue3'
import dayGridPlugin from '@fullcalendar/daygrid'

export default {
name: 'Home',
components: {
  'add-user-dialog': AddUserDialog,
  'add-shift-dialog': AddShiftDialog,
  FullCalendar,
  EventDialog
},
data() {
  return {
    calendarOptions: {
      plugins: [ dayGridPlugin ],
      initialView: 'dayGridWeek',
      headerToolbar: {
        left: 'prev,next',
        center: 'title',
        right: 'dayGridWeek,dayGridDay'
      },
      allDay: true,
      displayEventEnd: true,
      eventClick: this.handleEventClick // Ajout de cette ligne pour gérer le clic sur l'événement
    },
    drawer: true,
    users: [],
    shifts: [],
    selectedUser: null,
    calendarEvents: []
    
  };
  
},
mounted() {
  this.fetchUsers();
  this.fetchShifts();
},
methods: {
  handleEventClick(clickInfo) {
    if(!this.selectedUser) {
      alert("Please select a user")
    }
    else {
    console.log(this.selectedUser) // Ajout de cette ligne pour passer l'utilisateur
    this.$refs.eventDialog.open(clickInfo.event);
    }
  },
  addUser(user) {
    console.log(user)
    axios.post('http://localhost:5066/api/User',
    { 
      name: user.name,
      surname: user.surname,
      shifts: []
    }).catch(err => {
      console.log(err)
    }).then((response) => {
      console.log(response)
      this.users.push(response.data)
    });

    
  },
  removeUser(userToRemove) {
    console.log(userToRemove)
    axios.delete(`http://localhost:5066/api/User/${userToRemove.id}`).catch(err => {
      console.log(err)
    });
    var indexOfUser = this.users.findIndex(u => u.id === userToRemove.id)
    console.log(indexOfUser)
    console.log(this.users)
    this.users.splice(indexOfUser,1)
    
  },
  addShift(shift) {
    axios.post(`http://localhost:5066/api/Shift`, shift).catch(err => {
      console.log(err)
    })
  },
  fetchUsers() {
    axios
      .get('http://localhost:5066/api/User')
      .then((response) => {
        this.users = response.data;
      })
      .catch((error) => {
        console.error('Erreur lors de la récupération des utilisateurs', error);
      });
  },

  selectUser(user) {
    this.selectedUser = user;
    console.log(this.selectedUser)
  },

  fetchShifts() {
    axios
      .get(`http://localhost:5066/api/Shift`)
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
      })
      .catch((error) => {
        console.error('Erreur lors de la récupération des utilisateurs', error);
      });
    },
    
  }
};
</script>

<style scoped>
.userInfo {
  margin-left: 1em;
}
</style>
  