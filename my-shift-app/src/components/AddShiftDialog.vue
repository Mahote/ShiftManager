<template>
  <v-btn block color="primary" @click="addShiftDialog = true">Ajouter un créneau horaire</v-btn>
  <v-dialog v-model="addShiftDialog" max-width="500px">
    <v-card>
      <v-card-title>
        <span class="headline">Ajouter un créneau horaire</span>
      </v-card-title>
      <v-card-text>
        <!-- Champs pour la date, les créneaux horaires et la capacité -->
        <v-form ref="form" v-model="valid">
          <v-row>
            <v-col cols="6">
              <v-select
                v-model="startTime"
                :items="generateTimeOptions()"
                label="Heure de début"
                required
              ></v-select>
            </v-col>
            <v-col cols="6">
              <v-select
                v-model="endTime"
                :items="generateTimeOptions()"
                label="Heure de fin"
                :rules="endTimeRules"
                required
              ></v-select>
            </v-col>
            <v-col cols="12" md="6">
              <v-date-picker v-model="selectedDate" label="Sélectionnez une date"></v-date-picker>
            </v-col>
            <v-col cols="6">
              <v-text-field v-model="capacity" label="Capacité" type="number" required></v-text-field>
            </v-col>
          </v-row>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="blue darken-1" text @click="addShiftDialog = false">Fermer</v-btn>
        <v-btn color="blue darken-1" text @click="saveShift">Sauvegarder</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
 </template>
 
 <script>
 import moment from 'moment';
 
 export default {
  data() {
    return {
      addShiftDialog: false,
      valid: true,
      selectedDate: null,
      startTime: null,
      endTime: null,
      capacity: null,
      timeInterval: 30,
    };
  },
  computed: {
    endTimeRules() {
      return [
        (v) => !!v || "L'heure de fin est requise",
        (v) => this.isEndTimeValid(v) || "L'heure de fin doit être postérieure à l'heure de début",
      ];
    },
  },
  methods: {
    generateTimeOptions() {
      const options = [];
      for (let hour = 0; hour < 24; hour++) {
        for (let minute = 0; minute < 60; minute += this.timeInterval) {
          const time = `${hour.toString().padStart(2, '0')}:${minute.toString().padStart(2, '0')}`;
          options.push(time);
        }
      }
      return options;
    },
    isEndTimeValid(endTime) {
      if (!this.startTime || !endTime) return true;
 
      const start = this.convertTimeToMinutes(this.startTime);
      const end = this.convertTimeToMinutes(endTime);
      return end > start;
    },
 
    convertTimeToMinutes(time) {
      const [hours, minutes] = time.split(":").map(Number);
      return hours * 60 + minutes;
    },
    
    saveShift() {
      if (!this.selectedDate || !this.startTime || !this.endTime || !this.capacity) {
        console.error("Veuillez remplir tous les champs obligatoires.");
        return;
      }
      
      const startTime = new Date(`1970-01-01T${this.startTime}`);
      const endTime = new Date(`1970-01-01T${this.endTime}`);


      // Créer les objets Date complets pour la date de début et de fin
      const startDateTime = new Date(this.selectedDate.getFullYear(), this.selectedDate.getMonth(), this.selectedDate.getDate(), startTime.getHours()+1, startTime.getMinutes());
      const endDateTime = new Date(this.selectedDate.getFullYear(), this.selectedDate.getMonth(), this.selectedDate.getDate(), endTime.getHours()+1, endTime.getMinutes());
      
      const shiftData = {
        start: startDateTime.toISOString(),
        end: endDateTime.toISOString(),
        capacity: parseInt(this.capacity),
        users: []
      };

      console.log("Sauvegarde du créneau horaire", shiftData);
      this.$emit("save-shift", shiftData);
      this.addShiftDialog = false;
    },
  },
 };
 </script>
 