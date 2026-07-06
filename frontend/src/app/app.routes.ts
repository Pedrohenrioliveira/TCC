import { Routes } from '@angular/router';
import { PlayerHomeComponent } from './features/players/pages/player-home/player-home.component';
import { LeagueStandingsComponent } from './features/leagues/pages/league-standings/league-standings.component';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./features/auth/pages/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'player/home',
    component: PlayerHomeComponent
  },
  {
    path: 'player/leagues',
    component: LeagueStandingsComponent
  },
  {
    path: 'player/tournaments',
    loadComponent: () => import('./features/tournaments/pages/tournament-list/tournament-list.component').then(m => m.TournamentListComponent)
  },
  {
    path: 'player/club-requests',
    loadComponent: () => import('./features/club-requests/pages/request-list/request-list.component').then(m => m.RequestListComponent)
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

