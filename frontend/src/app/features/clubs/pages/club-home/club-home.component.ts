import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ClubProfileService, ClubDetails } from '../../services/club-profile.service';
import { ClubRequestService } from '../../../club-requests/services/club-request.service';
import { PlayerApiService } from '../../../../core/infrastructure/api/player-api.service';

@Component({
  selector: 'app-club-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './club-home.component.html',
  styleUrl: './club-home.component.css'
})
export class ClubHomeComponent implements OnInit {
  clubId: string = '';
  clubData: ClubDetails | null = null;
  loading: boolean = true;
  
  activePlayersCount: number = 0;
  newRequestsCount: number = 0;
  
  private router = inject(Router);

  constructor(
    private clubProfileService: ClubProfileService,
    private clubRequestService: ClubRequestService,
    private playerApiService: PlayerApiService
  ) {}

  ngOnInit(): void {
    this.clubId = localStorage.getItem('loggedClubId') || '';
    if (this.clubId) {
      this.loadDashboardData();
    } else {
      this.router.navigate(['/login']);
    }
  }

  loadDashboardData(): void {
    this.loading = true;
    
    // 1. Fetch club details
    this.clubProfileService.getProfile(this.clubId).subscribe({
      next: (res) => {
        if (res.ok) {
          this.clubData = res.dados;
        }
      },
      complete: () => {
        this.loading = false;
      }
    });

    // 2. Fetch roster (active players)
    // Note: If backend supports passing clubeId, we should pass it. For now we use the existing method.
    this.playerApiService.getPlayers(1, 500).subscribe({
      next: (data) => {
        // Ideally we filter by the club, but currently the roster uses this endpoint as well.
        this.activePlayersCount = data ? data.length : 0;
      }
    });

    // 3. Fetch new requests
    this.clubRequestService.getRequestsForClub(this.clubId).subscribe({
      next: (res: any) => {
        if (res.ok && res.dados) {
          // Status 1 = Pendente (new requests)
          const pendentes = res.dados.filter((r: any) => r.status === 1);
          this.newRequestsCount = pendentes.length;
        }
      }
    });
  }

  getClubName(): string {
    return this.clubData?.nome || 'Carregando...';
  }

  getClubShield(): string {
    const shield = this.clubData?.caminhoEscudo;
    if (!shield || shield === 'assets/default-shield.png') return 'assets/default-club.png';
    if (shield.startsWith('data:') || shield.startsWith('http') || shield.startsWith('assets/')) return shield;
    return `data:image/png;base64,${shield}`;
  }
}
