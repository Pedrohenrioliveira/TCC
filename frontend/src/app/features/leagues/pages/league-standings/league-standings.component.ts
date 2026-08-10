import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeagueTabsComponent } from '../../components/league-tabs/league-tabs.component';
import { StandingsTableComponent } from '../../components/standings-table/standings-table.component';
import { CampeonatoApiService } from '../../../../core/infrastructure/api/campeonato-api.service';

@Component({
  selector: 'app-league-standings',
  standalone: true,
  imports: [CommonModule, LeagueTabsComponent, StandingsTableComponent],
  templateUrl: './league-standings.component.html',
  styleUrl: './league-standings.component.css'
})
export class LeagueStandingsComponent implements OnInit {
  loading: boolean = false;
  error: string | null = null;
  
  activeLeagueId: string = '';
  
  leagues: { id: string, name: string }[] = [];
  tabelaData: any = null;

  constructor(private campeonatoApi: CampeonatoApiService) {}

  ngOnInit(): void {
    this.carregarCampeonatos();
  }

  carregarCampeonatos(): void {
    this.loading = true;
    this.campeonatoApi.listarCampeonatos().subscribe({
      next: (res: any) => {
        if (res.ok && res.dados && res.dados.length > 0) {
          this.leagues = res.dados.map((c: any) => ({
            id: c.id,
            name: c.nome
          }));
          this.activeLeagueId = this.leagues[0].id;
          this.loadStandings(this.activeLeagueId);
        } else {
          this.error = 'Nenhum campeonato encontrado.';
          this.loading = false;
        }
      },
      error: (err: any) => {
        this.error = 'Erro ao carregar campeonatos.';
        this.loading = false;
      }
    });
  }

  onTabChange(leagueId: string): void {
    this.activeLeagueId = leagueId;
    this.loadStandings(leagueId);
  }

  loadStandings(leagueId: string): void {
    this.loading = true;
    this.error = null;

    this.campeonatoApi.obterClassificacao(leagueId).subscribe({
      next: (res: any) => {
        if (res.ok) {
          // Transform response to match StandingsTableComponent expectations
          const times = res.dados.map((c: any, index: number) => ({
            posicao: index + 1,
            clubeId: c.clubeId,
            nomeClube: c.nomeClube,
            escudoUrl: c.caminhoEscudo ? `http://localhost:5000${c.caminhoEscudo}` : '',
            pontos: c.pontos,
            jogos: c.partidasJogadas,
            vitorias: c.vitorias,
            empates: c.empates,
            derrotas: c.derrotas,
            golsPro: c.golsPro,
            golsContra: c.golsContra,
            saldoGols: c.saldoGols
          }));
          
          this.tabelaData = { times };
        } else {
          this.error = res.mensagem;
        }
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Não foi possível carregar a classificação.';
        this.loading = false;
      }
    });
  }
}
