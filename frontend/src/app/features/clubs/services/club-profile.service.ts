import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ClubDetails {
  id: string;
  usuarioId: string;
  caminhoEscudo: string;
  nome: string;
  anoFundacao: number;
  cidadeEstado: string;
  ligaCompeticao: string;
  estadioPrincipal: string | null;
  breveHistoria: string;
}

export interface ApiResponse<T> {
  ok: boolean;
  mensagem: string;
  dados: T;
}

@Injectable({
  providedIn: 'root'
})
export class ClubProfileService {
  private apiUrl = 'http://localhost:5000/api/clubes';

  constructor(private http: HttpClient) {}

  getProfile(id: string): Observable<ApiResponse<ClubDetails>> {
    return this.http.get<ApiResponse<ClubDetails>>(`${this.apiUrl}/${id}`);
  }

  updateProfile(id: string, data: any): Observable<ApiResponse<any>> {
    return this.http.put<ApiResponse<any>>(`${this.apiUrl}/${id}`, data);
  }
}
