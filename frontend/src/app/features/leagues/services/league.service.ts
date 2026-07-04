import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RespostaApi } from '../../players/services/player-home.service';

export interface TimeClassificacaoDto {
  clubeId: string;
  nomeClube: string;
  escudoUrl: string;
  posicao: number;
  pontos: number;
  jogos: number;
  vitorias: number;
  empates: number;
  derrotas: number;
  golsPro: number;
  golsContra: number;
  saldoGols: number;
}

export interface TabelaClassificacaoDto {
  ligaId: string;
  nomeLiga: string;
  times: TimeClassificacaoDto[];
}

@Injectable({
  providedIn: 'root'
})
export class LeagueService {
  private apiUrl = 'http://localhost:5000/api/ligas';

  constructor(private http: HttpClient) { }

  getStandings(leagueId: string): Observable<RespostaApi<TabelaClassificacaoDto>> {
    return this.http.get<RespostaApi<TabelaClassificacaoDto>>(`${this.apiUrl}/${leagueId}/standings`);
  }
}
