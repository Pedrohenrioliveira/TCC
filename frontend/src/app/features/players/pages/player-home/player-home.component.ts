import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
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

  constructor(private playerHomeService: PlayerHomeService) {}

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    // Usar o ID do usuário cadastrado, ou o mock de teste caso não tenha feito login/cadastro
    const playerId = localStorage.getItem('loggedUserId') || '00000000-0000-0000-0000-000000000001';
    
    this.playerHomeService.getHomeDashboard(playerId).subscribe({
      next: (response) => {
        if (response.ok) {
          this.dashboardData = response.dados;
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
