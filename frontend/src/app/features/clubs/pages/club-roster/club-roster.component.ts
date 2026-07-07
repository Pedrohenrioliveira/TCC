import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { PlayerApiService } from '../../../../core/infrastructure/api/player-api.service';
import { PlayerResponseData } from '../../../../core/infrastructure/api/dtos/player.dto';

@Component({
  selector: 'app-club-roster',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './club-roster.component.html',
  styleUrl: './club-roster.component.css'
})
export class ClubRosterComponent implements OnInit {
  players: PlayerResponseData[] = [];
  loading = true;
  error = '';

  constructor(
    private playerApi: PlayerApiService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.carregarJogadores();
  }

  carregarJogadores(): void {
    this.loading = true;
    this.playerApi.getPlayers(1, 50).subscribe({
      next: (data) => {
        this.players = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Não foi possível carregar os jogadores.';
        this.loading = false;
        console.error(err);
      }
    });
  }

  verPerfil(id: string): void {
    this.router.navigate(['/club/player', id]);
  }

  getPosicaoNome(idPosicao: number): string {
    const posicoes: Record<number, string> = {
      1: 'Goleiro',
      2: 'Lateral Direito',
      3: 'Zagueiro',
      4: 'Lateral Esquerdo',
      5: 'Volante',
      6: 'Meio-Campo',
      7: 'Ponta',
      8: 'Centroavante'
    };
    return posicoes[idPosicao] || 'Desconhecida';
  }

  getNivelMock(id: string): string {
    // Apenas para simular o pill secundário já que não vem da API
    return 'Profissional';
  }
}
