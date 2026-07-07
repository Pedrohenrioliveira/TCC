import { Routes } from '@angular/router';
import { PlayerLayoutComponent } from './core/layout/player-layout/player-layout.component';

import { ClubLayoutComponent } from './core/layout/club-layout/club-layout.component';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./features/auth/pages/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'club',
    component: ClubLayoutComponent,
    children: [
      {
        path: 'home',
        loadComponent: () => import('./features/clubs/pages/club-home/club-home.component').then(m => m.ClubHomeComponent)
      },
      {
        path: 'roster',
        loadComponent: () => import('./features/clubs/pages/club-roster/club-roster.component').then(m => m.ClubRosterComponent)
      },
      {
        path: 'tournaments',
        loadComponent: () => import('./features/clubs/pages/club-tournaments/club-tournaments.component').then(m => m.ClubTournamentsComponent)
      },
      {
        path: 'profile',
        loadComponent: () => import('./features/clubs/pages/club-profile-edit/club-profile-edit.component').then(m => m.ClubProfileEditComponent)
      },
      {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full'
      }
    ]
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
