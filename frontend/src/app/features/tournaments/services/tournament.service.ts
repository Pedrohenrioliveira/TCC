import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Tournament {
  id: string;
  nome: string;
  local: string;
  dataInicio: string;
  dataFim: string;
  status: number; // 1 = Aberto, 2 = Em Andamento, 3 = Finalizado
  caminhoLogo: string;
  limiteEquipes: number;
}

export interface ApiResponse<T> {
  sucesso: boolean;
  mensagem: string;
  dados: T;
  erros: string[];
}

@Injectable({
  providedIn: 'root'
})
export class TournamentService {
  private apiUrl = 'http://localhost:5000/api/campeonatos';

  constructor(private http: HttpClient) { }

  getTournaments(status?: string): Observable<ApiResponse<Tournament[]>> {
    let params = new HttpParams();
    if (status) {
      params = params.set('status', status);
    }
    return this.http.get<ApiResponse<Tournament[]>>(this.apiUrl, { params });
  }
}
