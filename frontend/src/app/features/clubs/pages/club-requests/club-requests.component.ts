import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClubRequestForClub, ClubRequestService } from '../../../club-requests/services/club-request.service';
import { PlayerProfileService, PlayerDetails } from '../../../players/services/player-profile.service';

@Component({
  selector: 'app-club-requests',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './club-requests.component.html',
  styleUrls: ['./club-requests.component.css']
})
export class ClubRequestsComponent implements OnInit {
  requests: ClubRequestForClub[] = [];
  loading = false;
  error: string | null = null;
  activeStatus: number = 1; // Default to Pendentes
  selectedPlayer: PlayerDetails | null = null;

  constructor(
    private requestService: ClubRequestService,
    private playerProfileService: PlayerProfileService
  ) {}

  ngOnInit(): void {
    this.loadRequests();
  }

  verPerfilJogador(jogadorId: string): void {
    this.playerProfileService.getProfile(jogadorId).subscribe({
      next: (res: any) => {
        if (res.ok) {
          this.selectedPlayer = res.dados;
        }
      }
    });
  }

  fecharPerfil(): void {
    this.selectedPlayer = null;
  }

  loadRequests() {
    this.loading = true;
    this.error = null;
    
    // Para facilitar o teste sem tela de login, lemos o último clube que o jogador solicitou entrada.
    // Se não houver, tenta o loggedUserId ou usa o ID do "Clube Atlético Teste" de fallback.
    let clubeId = localStorage.getItem('lastAppliedClubId');
    if (!clubeId) {
       clubeId = localStorage.getItem('loggedUserId') || '11111111-1111-1111-1111-111111111111';
    }

    this.requestService.getRequestsForClub(clubeId).subscribe({
      next: (res: any) => {
        if (res.ok) {
          this.requests = res.dados;
        } else {
          this.error = res.mensagem;
        }
        this.loading = false;
      },
      error: () => {
        this.error = 'Erro ao carregar solicitações do clube.';
        this.loading = false;
      }
    });
  }

  get filteredRequests() {
    return this.requests.filter(r => r.status === this.activeStatus);
  }

  onFilterClick(status: number): void {
    this.activeStatus = status;
  }

  acceptRequest(id: string): void {
    this.updateStatus(id, 2);
  }

  rejectRequest(id: string): void {
    this.updateStatus(id, 3);
  }

  private updateStatus(id: string, status: number): void {
    this.requestService.updateStatus(id, status).subscribe({
      next: (res: any) => {
        if (res.ok) {
          this.loadRequests();
        } else {
          alert('Erro: ' + res.mensagem);
        }
      },
      error: () => alert('Erro ao atualizar status.')
    });
  }

  getStatusName(statusId: number): string {
    switch(statusId) {
      case 1: return 'Pendente';
      case 2: return 'Aprovado';
      case 3: return 'Recusado';
      default: return 'Desconhecido';
    }
  }

  getStatusClass(statusId: number): string {
    switch(statusId) {
      case 1: return 'status-pendente';
      case 2: return 'status-aceita';
      case 3: return 'status-recusada';
      default: return '';
    }
  }
}
