import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./features/auth/pages/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: '',
    loadComponent: () => import('./features/players/pages/player-register/player-register.component').then(m => m.PlayerRegisterComponent)
  },
  {
    path: 'clube',
    loadComponent: () => import('./features/clubs/pages/club-register/club-register.component').then(m => m.ClubRegisterComponent)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

