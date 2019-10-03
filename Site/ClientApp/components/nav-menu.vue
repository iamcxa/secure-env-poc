<template>
    <div class="main-nav">
        <nav class="navbar navbar-expand-md navbar-dark">
            <button class="navbar-toggler" type="button" @click="toggleCollapsed">
                <span class="navbar-toggler-icon"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <router-link class="navbar-brand" to="/"><!--<icon :icon="['fab', 'microsoft']"/>--> Secure Env Poc</router-link>
            <transition name="slide">
                <div :class="'collapse navbar-collapse' + (!collapsed ? ' show':'')" v-show="!collapsed">
                    <ul class="navbar-nav">
                        <li class="nav-item" v-for="(route, index) in routes" :key="index">
                            <router-link :to="route.path" exact-active-class="active">
                                <span>{{ route.display }}</span>
                            </router-link>
                        </li>
                        <li class="nav-item" :key="99">
                            <a href="/swagger" target="_blank"><span>Swagger</span></a>
                        </li>
                    </ul>

                </div>
            </transition>
        </nav>
    </div>
</template>
<script>
    import { routes } from '../router/routes'

    export default {
        data() {
            return {
                routes,
                collapsed: true
            }
        },
        methods: {
            toggleCollapsed: function (event) {
                this.collapsed = !this.collapsed
            }
        }
    }
</script>
<style scoped>

    .slide-enter-active, .slide-leave-active {
        transition: max-height .35s
    }

    .slide-enter, .slide-leave-to {
        max-height: 0px;
    }

    .slide-enter-to, .slide-leave {
        max-height: 20em;
    }
</style>
