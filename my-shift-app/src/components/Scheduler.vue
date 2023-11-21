<template>
  <v-app>
    <!-- Contenu principal -->
    <v-main>
      <v-row class="fill-height">
        <v-col>
          <v-sheet height="600">
            <FullCalendar :options="calendarOptions" ref="fullCalendarRef" />
          </v-sheet>
        </v-col>
      </v-row>
    </v-main>
  </v-app>
</template>

<script>
import axios from 'axios';
import FullCalendar from '@fullcalendar/vue3';
import dayGridPlugin from '@fullcalendar/daygrid';

export default {
  name: 'SecondCalendar',
  components: {
    FullCalendar
  },
  data() {
    return {
      calendarOptions: {
        plugins: [dayGridPlugin],
        initialView: 'dayGridMonth',
        allDay: true,
        displayEventEnd: true,
        events: [],
        eventDidMount: this.showTooltipOnEvent
      }
    };
  },
  mounted() {
    this.fetchEventData();
  },
  methods: {
    fetchEventData() {
      axios
        .get('https://localhost:7094/Scheduler/run')
        .then(response => {
          const dataFromAPI = response.data;
          const events = dataFromAPI.map(item => {
            return item.shifts.map(shift => {
              return {
                title: `${item.user.name} ${item.user.surname}, Capacité: ${shift.capacity} restante`,
                start: shift.start,
                end: shift.end
              };
            });
          }).flat();
          this.calendarOptions.events = events;
          this.calendarOptions.eventTimeFormat = {
            hour: '2-digit',
            minute: '2-digit',
            hour12: false
          };
        })
        .catch(error => {
          console.error('Erreur lors de la récupération des données', error);
        });
    },
    showTooltipOnEvent(info) {
      info.el.setAttribute('title', info.event.title);
    }
  }
};
</script>

<style>
/* Ajoute tes styles CSS ici si nécessaire */
</style>
