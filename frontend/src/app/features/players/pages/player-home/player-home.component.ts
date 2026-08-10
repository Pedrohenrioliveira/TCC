import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { PlayerHomeNavbarComponent } from '../../components/player-home-navbar/player-home-navbar.component';
import { PlayerSummaryCardComponent } from '../../components/player-summary-card/player-summary-card.component';
import { StatCardComponent } from '../../components/stat-card/stat-card.component';
import { PlayerHomeService, HomeDashboardDto } from '../../services/player-home.service';

@Component({
  selector: 'app-player-home',
  standalone: true,
  imports: [
    CommonModule,
    PlayerSummaryCardComponent,
    StatCardComponent
  ],
  templateUrl: './player-home.component.html',
  styleUrl: './player-home.component.css'
})
export class PlayerHomeComponent implements OnInit {
  dashboardData: HomeDashboardDto | null = null;
  loading: boolean = true;
  error: string | null = null;

  private router = inject(Router);

  constructor(private playerHomeService: PlayerHomeService) {}

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    const playerId = localStorage.getItem('loggedUserId') || '';
    if (!playerId) {
      this.router.navigate(['/login']);
      return;
    }
    
    this.playerHomeService.getHomeDashboard(playerId).subscribe({
      next: (response) => {
        if (response.ok && response.dados) {
          this.dashboardData = response.dados;
          if (this.dashboardData.caminhoFoto && !this.dashboardData.caminhoFoto.startsWith('data:') && !this.dashboardData.caminhoFoto.startsWith('http') && !this.dashboardData.caminhoFoto.startsWith('assets/')) {
            this.dashboardData.caminhoFoto = `data:image/png;base64,${this.dashboardData.caminhoFoto}`;
          }
        } else {
          this.error = response.mensagem;
        }
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Não foi possível carregar os dados. ' + err.message;
        this.loading = false;
      }
    });
  }
  
  getFirstName(fullName: string): string {
    if (!fullName) return '';
    return fullName.split(' ')[0];
  }
}
