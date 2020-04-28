import Vue from 'vue'
import VueRouter from 'vue-router'
import Usuario from '../components/Usuario.vue'
import Login from '../components/Login.vue'

Vue.use(VueRouter)

const routes = [
    {
        path: '/usuario',
        name: 'usuario',
        component: Usuario
    },
    {
        path: '/',
        name: 'login',
        component: Login
    }
]

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

export default router
