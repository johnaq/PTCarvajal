<template>
    <v-layout align-center justify-center>
        <v-flex xs12 sm8 md6 lg5 xl4>
            <v-card>
                <v-toolbar dark color="blue darken-3">
                    <v-toolbar-title>
                        Acceso al Sistema
                    </v-toolbar-title>
                </v-toolbar>
                <v-card-text>
                    <v-text-field v-model="identificacion" autofocus color="accent" label="Identificacion" required>
                    </v-text-field>
                    <v-text-field v-model="password" type="password" color="accent" label="Contraseña" required>
                    </v-text-field>
                </v-card-text>
                <v-card-actions class="px-3 pb-3">
                    <v-flex text-xs-right>
                        <v-btn color="primary"  @click="login">Ingresar</v-btn>
                    </v-flex>
                </v-card-actions>
            </v-card>
        </v-flex>
    </v-layout>
</template>
<script>
    import axios from 'axios'
    export default {
        data() {
            return {
                valid: true,
                modelstate: [],
                identificacion: '',
                password: ''
            }
        },
        methods: {
            login() {
                var me = this
                axios.post('api/Usuario/Login', {
                    'identificacion': parseFloat(me.identificacion),
                    'password': me.password
                }).then(function (response) {
                    //Redireccion
                    if (response.status == 200)
                        me.$router.push({name:'usuario'})
                }).catch(function () {
                        alert("Usuario o contraseña invalidos");
                });
            }
        }
    }
</script>

