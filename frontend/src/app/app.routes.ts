import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./features/players/pages/player-register/player-register.component').then(m => m.PlayerRegisterComponent)
  },
  {
    path: '**',
    redirectTo: ''
  }
];

