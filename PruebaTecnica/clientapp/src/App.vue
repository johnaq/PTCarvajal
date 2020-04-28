<template>
    <v-app id="inspire">


        <v-app-bar :clipped-left="$vuetify.breakpoint.lgAndUp"
                   app
                   color="blue darken-3"
                   dark>
            <v-toolbar-title style="width: 300px"
                             class="ml-0 pl-4">
                <span class="hidden-sm-and-down">Prueba Tecnica</span>
            </v-toolbar-title>
            <v-spacer />
        </v-app-bar>
        <v-content>
            <v-container class="fill-height"
                         fluid>
                <v-slide-y-transition mode="out-in">
                    <router-view></router-view>
                </v-slide-y-transition>
            </v-container>
        </v-content>
        <v-footer app color="blue darken-3" class="white--text">
            <span>John Quintero</span>
            <v-spacer></v-spacer>
            <span>&copy; 2020</span>
        </v-footer>
    </v-app>
</template>

<script>
    export default {
        name: 'App',

        data: () => ({
            drawer: null,
        })
    };
</script>