<template>
    <v-layout align-start>
        <v-flex>
            <v-data-table :headers="headers"
                          :items="usuarios"
                          :search="search"
                          class="elevation-1">
                <template v-slot:top>
                    <v-toolbar flat color="white">
                        <v-toolbar-title>Usuarios</v-toolbar-title>
                        <v-divider class="mx-4"
                                   inset
                                   vertical></v-divider>
                        <v-spacer></v-spacer>
                        <v-text-field class="text-xs-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
                        <v-spacer></v-spacer>
                        <v-dialog v-model="dialog" max-width="500px">
                            <template v-slot:activator="{ on }">
                                <v-btn color="primary" dark class="mb-2" v-on="on">Nuevo Usuario</v-btn>
                            </template>
                            <v-card>
                                <v-card-title>
                                    <span class="headline">{{ formTitle }}</span>
                                </v-card-title>
                                <v-form ref="form" v-model="valid" lazy-validation>
                                    <v-card-text>
                                        <v-container grid-list-md>
                                            <v-layout wrap>
                                                <v-flex xs12 sm12 md12>
                                                    <v-text-field v-model="nombre" label="Nombre" autofocus :error-messages="modelstate['Nombre']"></v-text-field>
                                                </v-flex>
                                                <v-flex xs12 sm12 md12 required>
                                                    <v-text-field v-model="apellido" label="Apellido" :error-messages="modelstate['Apellido']"></v-text-field>
                                                </v-flex>
                                                <v-flex xs12 sm12 md12 required>
                                                    <v-select :items="listadocumentos"
                                                              filled
                                                              v-model="tipidentificacion"
                                                              label="Tipo Identificacion"
                                                              item-text="nombre"
                                                              item-value="id"
                                                              :error-messages="modelstate['TipIdentificacionId']"></v-select>
                                                </v-flex>
                                                <v-flex xs12 sm12 md12 required>
                                                    <v-text-field type="number"  maxlength="10" counter v-model="identificacion" label="Identificacion" :error-messages="modelstate['Identificcion']"></v-text-field>
                                                </v-flex>
                                                <v-flex xs12 sm12 md12 required>
                                                    <v-text-field type="email" v-model="correo" label="Correo" :error-messages="modelstate['Correo']"></v-text-field>
                                                </v-flex>
                                                <v-flex xs12 sm12 md12 required>
                                                    <v-text-field type="password" v-model="password" label="Password" :error-messages="modelstate['Password']"></v-text-field>
                                                </v-flex>
                                            </v-layout>
                                        </v-container>
                                    </v-card-text>

                                    <v-card-actions>
                                        <v-spacer></v-spacer>
                                        <v-btn color="blue darken-1" text @click="close">Cancelar</v-btn>
                                        <v-btn color="blue darken-1" text @click="guardar">Guardar</v-btn>
                                    </v-card-actions>
                                </v-form>
                            </v-card>
                        </v-dialog>
                    </v-toolbar>
                </template>
                <template v-slot:item.action="{ item }">
                    <v-icon small
                            class="mr-2"
                            @click="editItem(item)">
                        edit
                    </v-icon>
                    <v-icon small
                            @click="deleteItem(item)">
                        delete
                    </v-icon>
                </template>
            </v-data-table>
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
                usuarios: [],
                dialog: false,
                headers: [
                    { text: 'Opciones', value: 'action', sortable: false },
                    { text: 'Nombre', value: 'nombre' },
                    { text: 'Apellido', value: 'apellido' },
                    { text: 'Tipo Identificacion', value: 'tipoIdentificacion' },
                    { text: 'Identificacion', value: 'identificacion' },
                    { text: 'Correo', value: 'correo' }
                ],
                search: '',
                desserts: [],
                editedIndex: -1,
                usuarioId: '',
                nombre: '',
                apellido: '',
                tipidentificacion: '',
                identificacion: '',
                correo: '',
                password: '',
                valor: '',
                listadocumentos: [],
                valida: 0,


            }
        },
        computed: {
            formTitle() {
                return this.editedIndex === -1 ? 'Nuevo Usuario' : 'Editar Usuario'
            },
        },

        watch: {
            dialog(val) {
                val || this.close();
                this.modelstate = [];
            },
        },

        created() {

            this.listar();
            this.cargarListaDocumentos();
        },
        methods: {
            listar() {
                var me = this;
                axios.get('api/Usuario/Listar').then(function (response) {
                    me.usuarios = response.data;
                }).catch(function (error) {
                    alert(error);
                });
            },

            editItem(item) {
                this.usuarioId = item.usuarioId,
                this.nombre = item.nombre,
                this.apellido = item.apellido,
                this.tipidentificacion = item.tipIdentificacionId,
                this.correo = item.correo,
                this.password = item.password,
                this.identificacion = item.identificacion,

                this.editedIndex = 1;
                this.dialog = true
            },

            deleteItem(item) {
                const index = this.desserts.indexOf(item)
                var borrar= confirm('Desea eliminar este usuario?') && this.desserts.splice(index, 1)
                if (borrar) {
                    var me = this
                    axios.delete('api/Usuario/Eliminar/' + item.usuarioId).then(function () {
                        me.listar();
                        alert('Usuario eliminado')
                    }).catch(function (error) {
                        alert(error);
                    });
                }
            },

            close() {
                this.dialog = false;
                this.limpiar();
            },

            limpiar() {
                this.usuarioId = '',
                this.nombre = '',
                this.apellido = '',
                this.tipidentificacion = '',
                this.correo = '',
                this.password = '',
                this.identificacion = '',
                this.editedIndex = -1

            },

            guardar() {

                this.modelstate = []
                var me = this;
                if (this.editedIndex > -1) {
                    axios.put('api/Usuario/Actualizar', {
                        'usuarioId': me.usuarioId,
                        'nombre': me.nombre,
                        'apellido': me.apellido,
                        'tipidentificacionid': parseFloat(me.tipidentificacion),
                        'identificcion': parseFloat(me.identificacion),
                        'correo': me.correo,
                        'password': me.password,
                    }).then(function () {
                        me.close();
                        me.listar();
                        me.limpiar();
                    }).catch(function (error) {
                        if (error.response.status == 400) {
                            me.modelstate = error.response.data.errors;
                        }
                    });
                } else {
                    axios.post('api/Usuario/Crear', {
                        'nombre': me.nombre,
                        'apellido': me.apellido,
                        'tipidentificacionid': parseFloat(me.tipidentificacion),
                        'identificcion': parseFloat(me.identificacion),
                        'correo': me.correo,
                        'password': me.password,
                    }).then(function () {
                        me.close();
                        me.listar();
                        me.limpiar();
                        alert("Usuario registrado con exito")
                    }).catch(function (error) {
                        if (error.response.status == 400) {
                            me.modelstate = error.response.data.errors;
                        }
                    });
                }
            },
            cargarListaDocumentos() {
                var me = this;
                axios.get('api/TipoIdentificacion/Listar').then(function (response) {
                    me.listadocumentos = response.data;
                }).catch(function (error) {
                    alert(error);
                });
            }
        }
    }
</script>