import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface CampeonatoDto {
  id: string;
  nome: string;
  local: string;
  dataInicio: string;
  dataFim: string;
  limiteEquipes: number;
  caminhoLogo: string;
  status: number;
}

export interface CriarCampeonatoRequest {
  nome: string;
  local: string;
  dataInicio: string;
  dataFim: string;
  limiteEquipes: number;
  caminhoLogo: string;
}

export interface InscricaoCampeonatoDto {
  id: string;
  campeonatoId: string;
  clubeId: string;
  nomeClube: string;
  caminhoEscudo: string;
  status: number; // 1 = Pendente, 2 = Aprovada, 3 = Rejeitada
  aceitouRegulamento: boolean;
  nomeResponsavel: string;
  telefoneResponsavel: string;
  caminhoDocumentoIdentidade: string;
  caminhoComprovantePagamento: string;
  dataSolicitacao: string;
}

@Injectable({
  providedIn: 'root'
})
export class CampeonatoApiService {
  private apiUrl = `http://localhost:5000/api/Campeonatos`;

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

  inscreverClube(campeonatoId: string, payload: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/${campeonatoId}/clubes`, payload);
  }

  obterInscricoesCampeonato(campeonatoId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${campeonatoId}/inscricoes`);
  }

  obterMinhasInscricoes(clubeId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/minhas-inscricoes/${clubeId}`);
  }

  processarInscricao(inscricaoId: string, aprovar: boolean): Observable<any> {
    const payload = { aprovar };
    return this.http.put<any>(`${this.apiUrl}/inscricoes/${inscricaoId}/processar`, payload);
  }

  obterClassificacao(campeonatoId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${campeonatoId}/classificacao`);
  }

  obterRodadas(campeonatoId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${campeonatoId}/rodadas`);
  }

  editarCampeonato(id: string, request: CriarCampeonatoRequest): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, request);
  }

  excluirCampeonato(id: string): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
