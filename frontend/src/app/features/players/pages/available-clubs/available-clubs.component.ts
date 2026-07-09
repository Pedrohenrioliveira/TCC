import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClubProfileService, ClubDetails } from '../../../clubs/services/club-profile.service';
import { ClubRequestService } from '../../../club-requests/services/club-request.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-available-clubs',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './available-clubs.component.html',
  styleUrls: ['./available-clubs.component.css']
})
export class AvailableClubsComponent implements OnInit {
  clubs: ClubDetails[] = [];
  loading = false;
  error: string | null = null;
  mensagem: string = '';
  selectedClub: ClubDetails | null = null;
  pendingClubsIds: Set<string> = new Set();

  constructor(
    private clubService: ClubProfileService,
    private requestService: ClubRequestService
  ) {}

  ngOnInit(): void {
    this.loadClubs();
    this.loadPendingRequests();
  }

  loadPendingRequests(): void {
    const jogadorId = localStorage.getItem('loggedUserId') || '00000000-0000-0000-0000-000000000001';
    this.requestService.getRequests(jogadorId).subscribe({
      next: (res: any) => {
        if (res.ok && res.dados) {
          const pendentes = res.dados.filter((r: any) => r.status === 1);
          this.pendingClubsIds = new Set(pendentes.map((r: any) => r.clubeId));
        }
      }
    });
  }

  verPerfil(club: ClubDetails): void {
    this.selectedClub = club;
  }

  fecharPerfil(): void {
    this.selectedClub = null;
  }

  loadClubs(): void {
    this.loading = true;
    this.clubService.getAllClubs().subscribe({
      next: (res: any) => {
        if (res.ok) {
          // O backend retorna um ResultadoPaginado, então a lista está em res.dados.itens
          this.clubs = res.dados.itens || res.dados;
        } else {
          this.error = res.mensagem;
        }
        this.loading = false;
      },
      error: () => {
        this.error = 'Erro ao carregar clubes.';
        this.loading = false;
      }
    });
  }

  solicitarEntrada(clubeId: string): void {
    const jogadorId = localStorage.getItem('loggedUserId') || '00000000-0000-0000-0000-000000000001';
    localStorage.setItem('lastAppliedClubId', clubeId); // Salva para facilitar o teste no dashboard do clube

    this.requestService.createRequest(jogadorId, clubeId, this.mensagem || 'Gostaria de participar do clube!').subscribe({
      next: (res: any) => {
        if (res.ok) {
          alert('Solicitação enviada com sucesso!');
          this.mensagem = '';
          this.pendingClubsIds.add(clubeId);
        } else {
          alert('Erro: ' + res.mensagem);
        }
      },
      error: () => alert('Erro ao enviar solicitação.')
    });
  }
}
