import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ClubRequest {
  id: string;
  clubeId?: string;
  nomeClube?: string;
  escudoClube?: string;
  jogadorId: string;
  nomeJogador?: string;
  caminhoFotoJogador?: string;
  mensagem: string;
  status: number; // 1 = Pendente, 2 = Aceita, 3 = Recusada
  dataSolicitacao: string;
}

export interface ClubRequestForClub extends ClubRequest {
  jogadorId: string;
  nomeJogador: string;
  caminhoFotoJogador: string;
}

export interface ApiResponse<T> {
  ok: boolean;
  mensagem: string;
  dados: T;
  erros: string[];
}

@Injectable({
  providedIn: 'root'
})
export class ClubRequestService {
  private apiUrlPlayer = 'http://localhost:5000/api/player/club-requests';
  private apiUrlClub = 'http://localhost:5000/api/club/club-requests';

  constructor(private http: HttpClient) { }

  getRequests(jogadorId: string): Observable<ApiResponse<ClubRequest[]>> {
    return this.http.get<ApiResponse<ClubRequest[]>>(`${this.apiUrlPlayer}/${jogadorId}`);
  }

  getRequestsForClub(clubeId: string): Observable<ApiResponse<ClubRequestForClub[]>> {
    return this.http.get<ApiResponse<ClubRequestForClub[]>>(`${this.apiUrlClub}/${clubeId}`);
  }

  createRequest(jogadorId: string, clubeId: string, mensagem: string): Observable<ApiResponse<string>> {
    return this.http.post<ApiResponse<string>>(this.apiUrlPlayer, { jogadorId, clubeId, mensagem });
  }

  updateStatus(id: string, status: number): Observable<ApiResponse<void>> {
    return this.http.put<ApiResponse<void>>(`${this.apiUrlClub}/${id}/status`, { id, novoStatus: status });
  }
}
