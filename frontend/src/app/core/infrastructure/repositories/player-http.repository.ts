import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { Player } from '../../domain/entities/player.entity';
import { PlayerRepository } from '../../domain/repositories/player.repository';
import { PlayerApiService } from '../api/player-api.service';
import { PlayerMapper } from '../api/mappers/player.mapper';

@Injectable({
  providedIn: 'root'
})
export class PlayerHttpRepository implements PlayerRepository {
  constructor(private apiService: PlayerApiService) {}

  create(player: Player): Observable<Player> {
    const request = PlayerMapper.toDto(player);
    return this.apiService.createPlayer(request).pipe(
      map(data => PlayerMapper.toEntity(data))
    );
  }

  update(player: Player): Observable<Player> {
    if (!player.id) {
      throw new Error('ID é obrigatório para atualização.');
    }
    const request = PlayerMapper.toDto(player);
    return this.apiService.updatePlayer(player.id, request).pipe(
      map(() => player)
    );
  }

  findById(id: string): Observable<Player> {
    return this.apiService.getPlayerById(id).pipe(
      map(data => PlayerMapper.toEntity(data))
    );
  }

  findAll(): Observable<Player[]> {
    return this.apiService.getPlayers().pipe(
      map(list => list.map(data => PlayerMapper.toEntity(data)))
    );
  }
}
