import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

export interface PlayerRequest {
  id: string;
  jogadorId: string;
  nomeJogador: string;
  fotoJogador: string | null;
  posicao: string;
  mensagem: string;
  dataSolicitacao: Date;
  status: number; // 1 = Pendente, 2 = Aceita, 3 = Recusada
}

@Component({
  selector: 'app-club-requests',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './club-requests.component.html',
  styleUrls: ['./club-requests.component.css']
})
export class ClubRequestsComponent implements OnInit {
  requests: PlayerRequest[] = [];
  loading = false;
  activeStatus: number = 1; // Default to Pendentes

  ngOnInit(): void {
    this.loadMockRequests();
  }

  loadMockRequests() {
    this.loading = true;
    setTimeout(() => {
      this.requests = [
        {
          id: 'req1',
          jogadorId: 'jog1',
          nomeJogador: 'Marcos Silva',
          fotoJogador: 'https://robohash.org/Marcos?set=set5',
          posicao: 'Meio-Campo',
          mensagem: 'Gostaria de fazer um teste no time!',
          dataSolicitacao: new Date('2023-10-01T10:00:00'),
          status: 1
        },
        {
          id: 'req2',
          jogadorId: 'jog2',
          nomeJogador: 'João Pedro',
          fotoJogador: 'https://robohash.org/Joao?set=set5',
          posicao: 'Atacante',
          mensagem: 'Tenho experiência em campeonatos regionais.',
          dataSolicitacao: new Date('2023-10-02T14:30:00'),
          status: 1
        },
        {
          id: 'req3',
          jogadorId: 'jog3',
          nomeJogador: 'Lucas Martins',
          fotoJogador: null,
          posicao: 'Zagueiro',
          mensagem: '',
          dataSolicitacao: new Date('2023-09-28T09:15:00'),
          status: 2
        }
      ];
      this.loading = false;
    }, 500);
  }

  get filteredRequests() {
    return this.requests.filter(r => r.status === this.activeStatus);
  }

  onFilterClick(status: number): void {
    this.activeStatus = status;
  }

  acceptRequest(id: string): void {
    const req = this.requests.find(r => r.id === id);
    if (req) {
      req.status = 2; // Aceita
    }
  }

  rejectRequest(id: string): void {
    const req = this.requests.find(r => r.id === id);
    if (req) {
      req.status = 3; // Recusada
    }
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
