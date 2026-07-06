import { Routes } from '@angular/router';
import { PlayerLayoutComponent } from './core/layout/player-layout/player-layout.component';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./features/auth/pages/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'player',
    component: PlayerLayoutComponent,
    children: [
      {
        path: 'home',
        loadComponent: () => import('./features/players/pages/player-home/player-home.component').then(m => m.PlayerHomeComponent)
      },
      {
        path: 'leagues',
        loadComponent: () => import('./features/leagues/pages/league-standings/league-standings.component').then(m => m.LeagueStandingsComponent)
      },
      {
        path: 'tournaments',
        loadComponent: () => import('./features/tournaments/pages/tournament-list/tournament-list.component').then(m => m.TournamentListComponent)
      },
      {
        path: 'profile',
        loadComponent: () => import('./features/players/pages/player-profile-edit/player-profile-edit.component').then(m => m.PlayerProfileEditComponent)
      },
      {
        path: 'club-requests',
        loadComponent: () => import('./features/club-requests/pages/request-list/request-list.component').then(m => m.RequestListComponent)
      },
      {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full'
      }
    ]
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
