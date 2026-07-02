import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { Player } from '../entities/player.entity';
import { PlayerRepository } from '../repositories/player.repository';

@Injectable({
  providedIn: 'root'
})
export class CreatePlayerUseCase {
  constructor(private playerRepository: PlayerRepository) {}

  execute(player: Player): Observable<Player> {
    // Validações de negócio/domínio adicionais
    if (!player.nomeCompleto || player.nomeCompleto.trim() === '') {
      return throwError(() => new Error('O nome completo é obrigatório.'));
    }
    if (!player.dataNascimento) {
      return throwError(() => new Error('A data de nascimento é obrigatória.'));
    }
    if (player.altura <= 0) {
      return throwError(() => new Error('A altura deve ser maior que 0 cm.'));
    }
    if (player.peso <= 0) {
      return throwError(() => new Error('O peso deve ser maior que 0 kg.'));
    }
    if (!player.posicaoPrincipal || player.posicaoPrincipal < 1 || player.posicaoPrincipal > 8) {
      return throwError(() => new Error('A posição principal é obrigatória e deve ser válida.'));
    }
    return this.playerRepository.create(player);
  }
}
