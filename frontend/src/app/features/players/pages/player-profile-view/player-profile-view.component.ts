import { Component, OnInit } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { PlayerApiService } from '../../../../core/infrastructure/api/player-api.service';
import { PlayerResponseData } from '../../../../core/infrastructure/api/dtos/player.dto';

@Component({
  selector: 'app-player-profile-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './player-profile-view.component.html',
  styleUrl: './player-profile-view.component.css'
})
export class PlayerProfileViewComponent implements OnInit {
  player: PlayerResponseData | null = null;
  loading = true;
  error = '';

  constructor(
    private route: ActivatedRoute,
    private playerApi: PlayerApiService,
    private location: Location
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.carregarJogador(id);
    } else {
      this.error = 'ID do jogador não fornecido.';
      this.loading = false;
    }
  }

  carregarJogador(id: string): void {
    this.loading = true;
    this.playerApi.getPlayerById(id).subscribe({
      next: (data) => {
        this.player = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Não foi possível carregar o perfil do jogador.';
        this.loading = false;
        console.error(err);
      }
    });
  }

  voltar(): void {
    this.location.back();
  }

  getPosicaoNome(idPosicao: number | undefined | null): string {
    if (!idPosicao) return 'Desconhecida';
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

  getPePreferencialNome(pe: number | undefined | null): string {
    if (!pe) return 'Não informado';
    const pes: Record<number, string> = {
      1: 'Esquerdo',
      2: 'Direito',
      3: 'Ambidestro'
    };
    return pes[pe] || 'Não informado';
  }
}
