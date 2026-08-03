import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';

export interface CampeonatoDto {
  id: string;
  nome: string;
  local: string;
  dataInicio: string;
  dataFim: string;
  status: number;
  limiteEquipes: number;
  caminhoLogo: string;
}

export interface CriarCampeonatoRequest {
  nome: string;
  local: string;
  dataInicio: string;
  dataFim: string;
  limiteEquipes: number;
  caminhoLogo: string;
}

@Injectable({
  providedIn: 'root'
})
export class CampeonatoApiService {
  private apiUrl = `${environment.apiUrl}/Campeonatos`;

  constructor(private http: HttpClient) {}

  listarCampeonatos(status?: string): Observable<any> {
    let url = this.apiUrl;
    if (status) {
      url += `?status=${status}`;
    }
    return this.http.get<any>(url);
  }

  criarCampeonato(request: CriarCampeonatoRequest): Observable<any> {
    return this.http.post<any>(this.apiUrl, request);
  }

  atualizarStatus(id: string, status: number): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}/status`, status);
  }

  inscreverClube(campeonatoId: string, clubeId: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/${campeonatoId}/clubes`, `"${clubeId}"`, {
      headers: { 'Content-Type': 'application/json' }
    });
  }

  obterClassificacao(campeonatoId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${campeonatoId}/classificacao`);
  }

  obterRodadas(campeonatoId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${campeonatoId}/rodadas`);
  }
}
