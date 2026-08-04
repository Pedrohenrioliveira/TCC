import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CampeonatoApiService, InscricaoCampeonatoDto } from '../../../../core/infrastructure/api/campeonato-api.service';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-club-tournament-details',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './club-tournament-details.component.html',
  styleUrl: './club-tournament-details.component.css'
})
export class ClubTournamentDetailsComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private api = inject(CampeonatoApiService);
  private http = inject(HttpClient); // To call Partidas API directly if needed

  campeonatoId: string = '';
  classificacao: any[] = [];
  rodadas: any[] = [];
  inscricoes: InscricaoCampeonatoDto[] = [];
  isAdmin = false;
  
  viewMode: 'tabela' | 'calendario' | 'inscricoes' = 'tabela';

  ngOnInit(): void {
    this.campeonatoId = this.route.snapshot.paramMap.get('id') || '';
    this.isAdmin = this.router.url.includes('/admin');
    if (this.campeonatoId) {
      this.carregarClassificacao();
      this.carregarRodadas();
      if (this.isAdmin) {
        this.carregarInscricoes();
      }
    }
  }

  carregarClassificacao() {
    this.api.obterClassificacao(this.campeonatoId).subscribe({
      next: (res: any) => {
        if (res.ok) {
          this.classificacao = res.dados;
        }
      },
      error: (err: any) => console.error(err)
    });
  }

  carregarRodadas() {
    this.api.obterRodadas(this.campeonatoId).subscribe({
      next: (res: any) => {
        if (res.ok) {
          this.rodadas = res.dados;
        }
      },
      error: (err: any) => console.error(err)
    });
  }

  carregarInscricoes() {
    this.api.obterInscricoesCampeonato(this.campeonatoId).subscribe({
      next: (res: any) => {
        if (res.ok) {
          this.inscricoes = res.dados;
        }
      },
      error: (err: any) => console.error(err)
    });
  }

  processarInscricao(inscricaoId: string, aprovar: boolean) {
    const acao = aprovar ? 'aprovar' : 'rejeitar';
    if (!confirm(`Tem certeza que deseja ${acao} esta inscrição?`)) return;

    this.api.processarInscricao(inscricaoId, aprovar).subscribe({
      next: (res: any) => {
        if (res.ok) {
          alert(res.mensagem);
          this.carregarInscricoes();
          if (aprovar) {
            this.carregarClassificacao();
          }
        } else {
          alert('Erro: ' + res.mensagem);
        }
      },
      error: (err: any) => console.error(err)
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

    this.http.put<any>(`http://localhost:5000/api/Partidas/${partidaId}/placar`, {
      golsMandante: golsMandante,
      golsVisitante: golsVisitante
    }).subscribe({
      next: (res: any) => {
        if (res.ok) {
          alert('Placar atualizado!');
          this.carregarClassificacao(); // Reload table
          this.carregarRodadas(); // Reload matches
        } else {
          alert('Erro: ' + res.mensagem);
        }
      },
      error: (err: any) => {
        alert('Erro de comunicação.');
        console.error(err);
      }
    });
  }
}
