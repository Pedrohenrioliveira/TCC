import { Injectable, inject, signal } from '@angular/core';
import { finalize, tap } from 'rxjs';
import { Player } from '../../../core/domain/entities/player.entity';
import { CreatePlayerUseCase } from '../../../core/domain/usecases/create-player.usecase';
import { ToastService } from '../../../core/shared/services/toast.service';

@Injectable({
  providedIn: 'root'
})
export class PlayersFacade {
  private createPlayerUseCase = inject(CreatePlayerUseCase);
  private toastService = inject(ToastService);

  // Estados reativos com Signals
  loading = signal<boolean>(false);
  error = signal<string | null>(null);
  success = signal<boolean>(false);

  registerPlayer(player: Player) {
    this.loading.set(true);
    this.error.set(null);
    this.success.set(false);

    return this.createPlayerUseCase.execute(player).pipe(
      tap({
        next: (createdPlayer) => {
          this.success.set(true);
          this.toastService.success(
            'Sucesso!',
            `O jogador ${createdPlayer.nomeCompleto} foi cadastrado com sucesso.`
          );
        },
        error: (err: Error) => {
          this.error.set(err.message);
          this.toastService.error('Erro no cadastro', err.message);
        }
      }),
      finalize(() => {
        this.loading.set(false);
      })
    );
  }
}
