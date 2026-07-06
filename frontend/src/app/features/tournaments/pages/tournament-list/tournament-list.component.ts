import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Tournament, TournamentService } from '../../services/tournament.service';

@Component({
  selector: 'app-tournament-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './tournament-list.component.html',
  styleUrls: ['./tournament-list.component.css']
})
export class TournamentListComponent implements OnInit {
  tournaments: Tournament[] = [];
  loading = false;
  error: string | null = null;
  activeStatus: string | undefined = undefined;

  constructor(private tournamentService: TournamentService) {}

  ngOnInit(): void {
    this.loadTournaments();
  }

  loadTournaments(status?: string): void {
    this.loading = true;
    this.error = null;
    this.activeStatus = status;

    this.tournamentService.getTournaments(status).subscribe({
      next: (res) => {
        if (res.sucesso) {
          this.tournaments = res.dados;
        } else {
          this.error = res.mensagem;
        }
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Erro ao carregar campeonatos.';
        this.loading = false;
      }
    });
  }

  getStatusName(statusId: number): string {
    switch(statusId) {
      case 1: return 'Aberto';
      case 2: return 'Em Andamento';
      case 3: return 'Finalizado';
      default: return 'Desconhecido';
    }
  }

  getStatusClass(statusId: number): string {
    switch(statusId) {
      case 1: return 'status-aberto';
      case 2: return 'status-andamento';
      case 3: return 'status-finalizado';
      default: return '';
    }
  }

  onFilterClick(status: string | undefined): void {
    this.loadTournaments(status);
  }
}
