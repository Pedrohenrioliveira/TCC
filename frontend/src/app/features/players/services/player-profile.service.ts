import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ApiResponse<T> {
  sucesso: boolean;
  mensagem: string;
  dados: T;
  erros: string[];
}

export interface PlayerDetails {
  id: string;
  usuarioId: string;
  caminhoFoto: string;
  nomeCompleto: string;
  dataNascimento: string;
  pePreferencial: number;
  altura: number;
  peso: number;
  posicaoPrincipal: number;
  posicaoSecundaria: number | null;
  bioHistorico: string;
  clubeId: string | null;
  nomeClube: string | null;
}

@Injectable({
  providedIn: 'root'
})
export class PlayerProfileService {
  private apiUrl = 'http://localhost:5000/api/jogadores';

  constructor(private http: HttpClient) { }

  getProfile(id: string): Observable<ApiResponse<PlayerDetails>> {
    return this.http.get<ApiResponse<PlayerDetails>>(`${this.apiUrl}/${id}`);
  }

  updatePersonalData(id: string, data: any): Observable<ApiResponse<any>> {
    return this.http.put<ApiResponse<any>>(`${this.apiUrl}/${id}/pessoais`, { jogadorId: id, ...data });
  }

  updatePhysicalData(id: string, data: any): Observable<ApiResponse<any>> {
    return this.http.put<ApiResponse<any>>(`${this.apiUrl}/${id}/fisicos`, { jogadorId: id, ...data });
  }

  updatePhoto(id: string, photoUrl: string): Observable<ApiResponse<any>> {
    return this.http.put<ApiResponse<any>>(`${this.apiUrl}/${id}/foto`, { jogadorId: id, caminhoFoto: photoUrl });
  }
}
