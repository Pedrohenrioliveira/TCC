import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CampeonatoApiService } from '../../../../core/infrastructure/api/campeonato-api.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-club-tournament-details',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './club-tournament-details.component.html',
  styleUrl: './club-tournament-details.component.css'
})
export class ClubTournamentDetailsComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private api = inject(CampeonatoApiService);
  private http = inject(HttpClient); // To call Partidas API directly if needed

  campeonatoId: string = '';
  classificacao: any[] = [];
  rodadas: any[] = [];
  
  viewMode: 'tabela' | 'calendario' = 'tabela';

  ngOnInit(): void {
    this.campeonatoId = this.route.snapshot.paramMap.get('id') || '';
    if (this.campeonatoId) {
      this.carregarClassificacao();
      this.carregarRodadas();
    }
  }

  carregarClassificacao() {
    this.api.obterClassificacao(this.campeonatoId).subscribe({
      next: (res) => {
        if (res.deuCerto) {
          this.classificacao = res.dados;
        }
      },
      error: (err) => console.error(err)
    });
  }

  carregarRodadas() {
    this.api.obterRodadas(this.campeonatoId).subscribe({
      next: (res) => {
        if (res.deuCerto) {
          this.rodadas = res.dados;
        }
      },
      error: (err) => console.error(err)
    });
  }

  // Method to launch a score (Lançamento de Placar)
  lancarPlacar(partidaId: string) {
    const placar = prompt('Digite o placar (ex: 2x1):');
    if (!placar || !placar.includes('x')) return;
    
    const partes = placar.split('x');
    const golsMandante = parseInt(partes[0].trim(), 10);
    const golsVisitante = parseInt(partes[1].trim(), 10);

    if (isNaN(golsMandante) || isNaN(golsVisitante)) {
      alert('Placar inválido.');
      return;
    }

    this.http.put<any>(`${environment.apiUrl}/Partidas/${partidaId}/placar`, {
      golsMandante: golsMandante,
      golsVisitante: golsVisitante
    }).subscribe({
      next: (res) => {
        if (res.deuCerto) {
          alert('Placar atualizado!');
          this.carregarClassificacao(); // Reload table
          this.carregarRodadas(); // Reload matches
        } else {
          alert('Erro: ' + res.mensagem);
        }
      },
      error: (err) => {
        alert('Erro de comunicação.');
        console.error(err);
      }
    });
  }
}
