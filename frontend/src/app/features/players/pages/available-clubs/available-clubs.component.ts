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

  constructor(
    private clubService: ClubProfileService,
    private requestService: ClubRequestService
  ) {}

  ngOnInit(): void {
    this.loadClubs();
  }

  loadClubs(): void {
    this.loading = true;
    this.clubService.getAllClubs().subscribe({
      next: (res) => {
        if (res.ok) {
          this.clubs = res.dados;
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
    this.requestService.createRequest(jogadorId, clubeId, this.mensagem || 'Gostaria de participar do clube!').subscribe({
      next: (res) => {
        if (res.ok) {
          alert('Solicitação enviada com sucesso!');
        } else {
          alert('Erro: ' + res.mensagem);
        }
      },
      error: () => alert('Erro ao enviar solicitação.')
    });
  }
}
