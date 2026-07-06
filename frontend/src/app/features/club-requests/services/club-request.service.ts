import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ClubRequest {
  id: string;
  clubeId: string;
  nomeClube: string;
  escudoClube: string;
  mensagem: string;
  status: number; // 1 = Pendente, 2 = Aceita, 3 = Recusada
  dataSolicitacao: string;
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
  private apiUrl = 'http://localhost:5000/api/player/club-requests';

  constructor(private http: HttpClient) { }

  getRequests(jogadorId: string): Observable<ApiResponse<ClubRequest[]>> {
    return this.http.get<ApiResponse<ClubRequest[]>>(`${this.apiUrl}/${jogadorId}`);
  }

  createRequest(jogadorId: string, clubeId: string, mensagem: string): Observable<ApiResponse<string>> {
    return this.http.post<ApiResponse<string>>(this.apiUrl, { jogadorId, clubeId, mensagem });
  }

  updateStatus(id: string, novoStatus: number): Observable<ApiResponse<any>> {
    return this.http.put<ApiResponse<any>>(`${this.apiUrl}/${id}/status`, { id, novoStatus });
  }
}
