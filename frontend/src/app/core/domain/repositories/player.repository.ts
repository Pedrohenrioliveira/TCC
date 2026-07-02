import { Observable } from 'rxjs';
import { Player } from '../entities/player.entity';

export abstract class PlayerRepository {
  abstract create(player: Player): Observable<Player>;
  abstract update(player: Player): Observable<Player>;
  abstract findById(id: string): Observable<Player>;
  abstract findAll(): Observable<Player[]>;
}
