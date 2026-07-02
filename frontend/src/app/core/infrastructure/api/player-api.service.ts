import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { PlayerCreateRequest, PlayerResponseData, RespostaApi } from './dtos/player.dto';

@Injectable({
  providedIn: 'root'
})
export class PlayerApiService {
  private apiUrl = 'http://localhost:5200/api/jogadores'; // URL do backend C# (porta 5200 local, conforme README)

  constructor(private http: HttpClient) {}

  createPlayer(request: PlayerCreateRequest): Observable<PlayerResponseData> {
    return this.http.post<RespostaApi<PlayerResponseData>>(this.apiUrl, request).pipe(
      map(response => {
        if (!response.ok) {
          throw new Error(response.mensagem || 'Falha ao cadastrar jogador.');
        }
        return response.dados;
      })
    );
  }

  updatePlayer(id: string, request: PlayerCreateRequest): Observable<void> {
    return this.http.put<RespostaApi<any>>(`${this.apiUrl}/${id}`, request).pipe(
      map(response => {
        if (!response.ok) {
          throw new Error(response.mensagem || 'Falha ao atualizar jogador.');
        }
      })
    );
  }

  getPlayerById(id: string): Observable<PlayerResponseData> {
    return this.http.get<RespostaApi<PlayerResponseData>>(`${this.apiUrl}/${id}`).pipe(
      map(response => {
        if (!response.ok) {
          throw new Error(response.mensagem || 'Jogador não encontrado.');
        }
        return response.dados;
      })
    );
  }

  getPlayers(pagina = 1, tamanhoPagina = 50, busca?: string): Observable<PlayerResponseData[]> {
    let url = `${this.apiUrl}?pagina=${pagina}&tamanhoPagina=${tamanhoPagina}`;
    if (busca) {
      url += `&busca=${encodeURIComponent(busca)}`;
    }
    return this.http.get<RespostaApi<any>>(url).pipe(
      map(response => {
        if (!response.ok) {
          throw new Error(response.mensagem || 'Falha ao listar jogadores.');
        }
        // O backend retorna um ResultadoPaginado com a propriedade "itens" ou semelhante
        return response.dados?.itens || [];
      })
    );
  }
}
