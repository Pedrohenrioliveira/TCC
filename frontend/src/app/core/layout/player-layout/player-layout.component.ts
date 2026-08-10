import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet, RouterLink, RouterLinkActive, Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';
import { PlayerProfileService } from '../../../features/players/services/player-profile.service';

@Component({
  selector: 'app-player-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './player-layout.component.html',
  styleUrl: './player-layout.component.css'
})
export class PlayerLayoutComponent implements OnInit {
  private router = inject(Router);
  public isProfilePage = false;
  // TODO: Obter a foto do usuário do estado de autenticação/API
  public userPhoto = 'assets/default-avatar.png';
  private profileService = inject(PlayerProfileService);

  constructor() {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe((event: any) => {
      this.isProfilePage = event.urlAfterRedirects.includes('/player/profile');
    });
  }

  ngOnInit(): void {
    const userId = localStorage.getItem('loggedUserId');
    if (userId) {
      this.profileService.getProfile(userId).subscribe({
        next: (res: any) => {
          if (res.ok && res.dados && res.dados.caminhoFoto) {
            let photo = res.dados.caminhoFoto;
            if (!photo.startsWith('data:') && !photo.startsWith('http') && !photo.startsWith('assets/')) {
              photo = `data:image/png;base64,${photo}`;
            }
            this.userPhoto = photo;
          }
        },
        error: () => {}
      });
    }
  }
}
