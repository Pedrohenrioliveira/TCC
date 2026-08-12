import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PostagemDto, AdicionarPostagemCommand } from './dtos/feed.dto';
import { RespostaApi } from './dtos/player.dto';

@Injectable({
  providedIn: 'root'
})
export class FeedApiService {
  private apiUrl = 'http://localhost:5000/api/feed';

  constructor(private http: HttpClient) {}

  obterPostagens(perfilId: string): Observable<RespostaApi<PostagemDto[]>> {
    return this.http.get<RespostaApi<PostagemDto[]>>(`${this.apiUrl}/${perfilId}`);
  }

  adicionarPostagem(command: AdicionarPostagemCommand): Observable<RespostaApi<string>> {
    const formData = new FormData();
    formData.append('perfilId', command.perfilId);
    formData.append('foto', command.foto);
    formData.append('descricao', command.descricao);

    return this.http.post<RespostaApi<string>>(this.apiUrl, formData);
  }
}
