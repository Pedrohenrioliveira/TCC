import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';
import { ClubProfileService } from '../../../features/clubs/services/club-profile.service';
import { inject } from '@angular/core';

@Component({
  selector: 'app-club-layout',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './club-layout.component.html',
  styleUrls: ['./club-layout.component.css']
})
export class ClubLayoutComponent implements OnInit {
  clubPhoto: string = '';
  isProfilePage = false;
  private profileService = inject(ClubProfileService);

  constructor(private router: Router) {
    this.router.events.pipe(
      filter((event): event is NavigationEnd => event instanceof NavigationEnd)
    ).subscribe((event: NavigationEnd) => {
      this.isProfilePage = event.urlAfterRedirects.includes('/club/profile');
    });
  }

  ngOnInit(): void {
    const clubId = localStorage.getItem('loggedClubId');
    if (clubId) {
      this.profileService.getProfile(clubId).subscribe({
        next: (res: any) => {
          if (res.ok && res.dados && res.dados.caminhoEscudo) {
            let shield = res.dados.caminhoEscudo;
            if (shield !== 'assets/default-shield.png') {
              if (!shield.startsWith('data:') && !shield.startsWith('http') && !shield.startsWith('assets/')) {
                shield = `data:image/png;base64,${shield}`;
              }
              this.clubPhoto = shield;
            }
          }
        },
        error: () => {}
      });
    }
  }
}
