import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClubRequest, ClubRequestService } from '../../services/club-request.service';

@Component({
  selector: 'app-request-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './request-list.component.html',
  styleUrls: ['./request-list.component.css']
})
export class RequestListComponent implements OnInit {
  requests: ClubRequest[] = [];
  loading = false;
  error: string | null = null;
  // TODO: Pegar do token autenticado
  jogadorId = '00000000-0000-0000-0000-000000000001'; 

  constructor(private requestService: ClubRequestService) {}

  ngOnInit(): void {
    this.loadRequests();
  }

  loadRequests(): void {
    this.loading = true;
    this.error = null;

    this.requestService.getRequests(this.jogadorId).subscribe({
      next: (res) => {
        if (res.sucesso) {
          this.requests = res.dados;
        } else {
          this.error = res.mensagem;
        }
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Erro ao carregar solicitações.';
        this.loading = false;
      }
    });
  }

  getStatusName(statusId: number): string {
    switch(statusId) {
      case 1: return 'Pendente';
      case 2: return 'Aceita';
      case 3: return 'Recusada';
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

  acceptRequest(id: string): void {
    this.updateStatus(id, 2);
  }

  rejectRequest(id: string): void {
    this.updateStatus(id, 3);
  }

  private updateStatus(id: string, status: number): void {
    this.requestService.updateStatus(id, status).subscribe({
      next: (res) => {
        if (res.sucesso) {
          this.loadRequests();
        } else {
          alert('Erro: ' + res.mensagem);
        }
      },
      error: () => alert('Erro ao atualizar status.')
    });
  }
}
