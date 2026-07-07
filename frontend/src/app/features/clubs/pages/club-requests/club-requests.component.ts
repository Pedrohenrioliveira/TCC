import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClubRequest, ClubRequestService } from '../../../club-requests/services/club-request.service';

@Component({
  selector: 'app-club-requests',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './club-requests.component.html',
  styleUrls: ['./club-requests.component.css']
})
export class ClubRequestsComponent implements OnInit {
  requests: ClubRequest[] = [];
  loading = false;
  error: string | null = null;
  activeStatus: number = 1; // Default to Pendentes

  constructor(private requestService: ClubRequestService) {}

  ngOnInit(): void {
    this.loadRequests();
  }

  loadRequests() {
    this.loading = true;
    this.error = null;
    const clubeId = localStorage.getItem('loggedUserId') || '11111111-1111-1111-1111-111111111111';

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
