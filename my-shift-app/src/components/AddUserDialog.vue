<template>
  <v-btn block color="primary" @click="addUserDialog = true">Ajouter un utilisateur
    <v-dialog v-model="dialog" activator="parent" max-width="500px">
      <v-card>
        <v-card-title>
          <span class="headline">Ajouter un utilisateur</span>
        </v-card-title>
        <v-card-text>
          <v-form ref="form" v-model="valid">
            <v-text-field label="Nom" v-model="user.name" :rules="nameRules" required></v-text-field>
            <v-text-field label="Prénom" v-model="user.surname" :rules="surnameRules" required></v-text-field>
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="this.dialog = false">Fermer</v-btn>
          <v-btn color="blue darken-1" text @click="save">Sauvegarder</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-btn>
</template>
   
   <script>
   export default {
    data: () => ({
      dialog: false,
      valid: true,
      user: {
        name: '',
        surname: ''
      },
      nameRules: [
        v => !!v || 'Le nom est requis',
      ],
      surnameRules: [
        v => !!v || 'Le prénom est requis',
      ],
    }),
    methods: {
      save() {
        console.log("save")
        // check if user.name et user.surname sont rempli
          this.$emit("save", JSON.parse(JSON.stringify(this.user)));
          this.user.name = "";
          this.user.surname = "";
          this.dialog = false;
        }
      }
    }

   </script>
   