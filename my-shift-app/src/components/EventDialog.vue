<template>
    <v-dialog v-model="dialog" max-width="290">
     <v-card>
       <v-card-title>
         {{ event.title }}
       </v-card-title>
       <v-card-text>
         {{ event.description }}
         <p>User: {{ user.name }} {{ user.surname }}</p>
       </v-card-text>
       <v-card-actions>
         <v-spacer></v-spacer>
         <v-btn color="green darken-1" text @click="dialog = false">Close</v-btn>
       </v-card-actions>
     </v-card>
    </v-dialog>
   </template>
   
   <script>
   export default {
    props: ['user'], // Ajout de 'user' en tant que prop
    data() {
     return {
       dialog: false,
       event: {}
     };
    },
    methods: {
     open(event) {
       this.event = event;
       this.dialog = true;
     },
     setUser(user) {
        console.log(user)
        this.$emit('update:user', user);
        console.log(user)
    }
    }
   };
   </script>
   