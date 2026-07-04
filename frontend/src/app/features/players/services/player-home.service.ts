import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface HomeDashboardDto {
  jogadorId: string;
  caminhoFoto: string;
  nomeCompleto: string;
  posicaoPrincipal: string;
  nivel: number;
  golsNaTemporada: number;
  assistencias: number;
  variacaoGols: number;
  variacaoAssistencias: number;
}

export interface RespostaApi<T> {
  sucesso: boolean;
  mensagem: string;
  dados: T;
  erros: string[];
}

@Injectable({
  providedIn: 'root'
})
export class PlayerHomeService {
  private apiUrl = 'http://localhost:5000/api/jogadores'; // TODO: Usar environment

  constructor(private http: HttpClient) { }

  getHomeDashboard(playerId: string): Observable<RespostaApi<HomeDashboardDto>> {
    return this.http.get<RespostaApi<HomeDashboardDto>>(`${this.apiUrl}/${playerId}/home`);
  }
}
