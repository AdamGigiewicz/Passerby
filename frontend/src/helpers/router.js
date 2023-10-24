import { createRouter, createWebHistory } from 'vue-router';

import { useAuthStore } from '@/stores';
import { HomeView, LoginView, AdminView, AddUser, EditUser} from '@/views';


export const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    linkActiveClass: 'active',
    routes: [
        { path: '/', component: HomeView },
        { path: '/login', component: LoginView },
        { path: '/admin', component: AdminView },
        { path: '/edit', component: EditUser },
        { path: '/add', component: AddUser },
    ]
});

router.beforeEach(async (to) => {
    const publicPages = ['/login'];
    const authRequired = !publicPages.includes(to.path);
    const auth = useAuthStore();

    if (authRequired && !auth.token) {
        auth.returnUrl = to.fullPath;
        return '/login';
    }

});
