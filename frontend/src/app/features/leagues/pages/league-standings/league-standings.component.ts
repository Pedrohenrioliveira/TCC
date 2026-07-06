import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LeagueTabsComponent } from '../../components/league-tabs/league-tabs.component';
import { StandingsTableComponent } from '../../components/standings-table/standings-table.component';
import { LeagueService, TabelaClassificacaoDto } from '../../services/league.service';

@Component({
  selector: 'app-league-standings',
  standalone: true,
  imports: [CommonModule, LeagueTabsComponent, StandingsTableComponent],
  templateUrl: './league-standings.component.html',
  styleUrl: './league-standings.component.css'
})
export class LeagueStandingsComponent implements OnInit {
  tabelaData: TabelaClassificacaoDto | null = null;
  loading: boolean = false;
  error: string | null = null;
  
  activeLeagueId: string = '1';
  
  leagues = [
    { id: '1', name: 'Liga Nacional A' },
    { id: '2', name: 'Liga Nacional B' },
    { id: '3', name: 'Copa Regional' }
  ];

  constructor(private leagueService: LeagueService) {}

  ngOnInit(): void {
    this.loadStandings(this.activeLeagueId);
  }

  onTabChange(leagueId: string): void {
    this.activeLeagueId = leagueId;
    this.loadStandings(leagueId);
  }

  loadStandings(leagueId: string): void {
    this.loading = true;
    this.error = null;
    
    // Simulação com Guid vazio temporário para não dar erro na API
    const mockId = '00000000-0000-0000-0000-000000000000';

    this.leagueService.getStandings(mockId).subscribe({
      next: (res) => {
        if (res.ok) {
          this.tabelaData = res.dados;
          // Sobrescreve o nome da liga com base na tab selecionada
          const selected = this.leagues.find(l => l.id === leagueId);
          if (selected) {
             this.tabelaData.nomeLiga = selected.name;
          }
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
