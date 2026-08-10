import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CampeonatoApiService, InscricaoCampeonatoDto } from '../../../../core/infrastructure/api/campeonato-api.service';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-club-tournament-details',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './club-tournament-details.component.html',
  styleUrl: './club-tournament-details.component.css'
})
export class ClubTournamentDetailsComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private api = inject(CampeonatoApiService);
  private http = inject(HttpClient); 

  campeonatoId: string = '';
  classificacao: any[] = [];
  rodadas: any[] = [];
  inscricoes: InscricaoCampeonatoDto[] = [];
  clubesAprovados: any[] = [];
  isAdmin = false;
  
  viewMode: 'tabela' | 'calendario' | 'inscricoes' = 'tabela';
  
  // Modal de Análise
  showAnaliseModal = false;
  inscricaoEmAnalise: InscricaoCampeonatoDto | null = null;

  // Modal Agendar
  showAgendarModal = false;
  novaPartida = {
    numeroRodada: 1,
    clubeMandanteId: '',
    clubeVisitanteId: '',
    dataHora: '',
    local: ''
  };

  // Modal Classificação
  showClassificacaoModal = false;
  classificacaoEdicao: any = null;

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
          this.clubesAprovados = this.classificacao.map(c => ({
            id: c.clubeId,
            nome: c.nomeClube
          }));
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
    this.api.obterInscricoes(this.campeonatoId).subscribe({
      next: (res: any) => {
        if (res.ok) {
          this.inscricoes = res.dados;
        }
      },
      error: (err: any) => console.error(err)
    });
  }

  abrirModalAnalise(inscricao: InscricaoCampeonatoDto) {
    this.inscricaoEmAnalise = inscricao;
    this.showAnaliseModal = true;
  }

  fecharModalAnalise() {
    this.showAnaliseModal = false;
    this.inscricaoEmAnalise = null;
  }

  processarInscricao(inscricaoId: string, aprovar: boolean) {
    const acao = aprovar ? 'aprovar' : 'rejeitar';
    if (!confirm(`Tem certeza que deseja ${acao} esta inscrição?`)) return;

    this.api.processarInscricao(inscricaoId, aprovar).subscribe({
      next: (res: any) => {
        if (res.ok) {
          alert(res.mensagem);
          this.carregarInscricoes();
          this.fecharModalAnalise();
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

  abrirModalAgendar() {
    this.showAgendarModal = true;
    this.novaPartida = {
      numeroRodada: 1,
      clubeMandanteId: '',
      clubeVisitanteId: '',
      dataHora: '',
      local: ''
    };
  }

  fecharModalAgendar() {
    this.showAgendarModal = false;
  }

  salvarPartidaManual() {
    if (!this.novaPartida.clubeMandanteId || !this.novaPartida.clubeVisitanteId) {
      alert('Selecione os dois times.');
      return;
    }
    if (!this.novaPartida.dataHora) {
      alert('Informe a data e hora do jogo.');
      return;
    }
    if (!this.novaPartida.local) {
      alert('Informe o local do jogo.');
      return;
    }
    
    this.api.agendarPartidaManual(this.campeonatoId, this.novaPartida).subscribe({
      next: (res: any) => {
        if (res.ok) {
          alert('Partida agendada com sucesso!');
          this.fecharModalAgendar();
          this.carregarRodadas();
        } else {
          alert('Erro: ' + res.mensagem);
        }
      },
      error: (err) => {
        const msg = err.error?.mensagem || err.error?.title || 'Erro desconhecido';
        alert('Erro ao agendar a partida: ' + msg);
        console.error('Erro na requisição:', err);
      }
    });
  }

  abrirModalClassificacao(classif: any) {
    this.classificacaoEdicao = { ...classif };
    this.showClassificacaoModal = true;
  }

  fecharModalClassificacao() {
    this.showClassificacaoModal = false;
    this.classificacaoEdicao = null;
  }

  salvarClassificacaoManual() {
    this.api.atualizarClassificacaoManual(this.campeonatoId, this.classificacaoEdicao).subscribe({
      next: (res: any) => {
        if (res.ok) {
          alert('Classificação atualizada!');
          this.fecharModalClassificacao();
          this.carregarClassificacao();
        } else {
          alert('Erro: ' + res.mensagem);
        }
      },
      error: (err) => {
        alert('Erro ao atualizar classificação.');
        console.error(err);
      }
    });
  }

  lancarPlacar(partidaId: string) {
    const placar = prompt('Digite o placar (ex: 2x1). Lembre-se que a pontuação deve ser inserida manualmente depois.');
    if (!placar) return;
    
    const match = placar.match(/(\d+)\s*[xX]\s*(\d+)/);
    if (!match) {
      alert('Formato de placar inválido. Por favor, digite no formato "2x1".');
      return;
    }
    
    const golsMandante = parseInt(match[1], 10);
    const golsVisitante = parseInt(match[2], 10);

    if (isNaN(golsMandante) || isNaN(golsVisitante)) {
      alert('Valores de gols inválidos.');
      return;
    }

    this.http.put<any>(`http://localhost:5000/api/Partidas/${partidaId}/placar`, {
      golsMandante: golsMandante,
      golsVisitante: golsVisitante
    }).subscribe({
      next: (res: any) => {
        if (res.ok) {
          alert('Placar e Classificação atualizados com sucesso!');
          this.carregarRodadas(); 
          this.carregarClassificacao();
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

  getFullImageUrl(path: string): string {
    if (!path) return '';
    return `http://localhost:5000${path}`;
  }
}
