import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface LoginRequest {
  login: string;
  senha: string;
  manterConectado?: boolean;
}

export interface LoginResponseData {
  accessToken: string;
  expiraEm: string;
  nomeCompleto: string;
  nomeUsuario: string;
  email: string;
  perfil: string;
  jogadorId?: string | null;
  clubeId?: string | null;
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
export class AuthService {
  private apiUrl = `http://localhost:5000/api/autenticacao`;

  constructor(private http: HttpClient) {}

  login(payload: LoginRequest): Observable<ApiResponse<LoginResponseData>> {
    return this.http.post<ApiResponse<LoginResponseData>>(`${this.apiUrl}/login`, payload);
  }

  logout(): void {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('loggedUserId');
    localStorage.removeItem('loggedClubId');
    localStorage.removeItem('userRole');
  }

  salvarSessao(dados: LoginResponseData): void {
    localStorage.setItem('accessToken', dados.accessToken);
    localStorage.setItem('userRole', dados.perfil);

    // Se tiver jogador vinculado
    if (dados.jogadorId) {
      localStorage.setItem('loggedUserId', dados.jogadorId);
    }
    
    // Se tiver clube vinculado
    if (dados.clubeId) {
      localStorage.setItem('loggedClubId', dados.clubeId);
    }
  }
}
