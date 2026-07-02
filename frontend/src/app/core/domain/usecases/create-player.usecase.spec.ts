import { CreatePlayerUseCase } from './create-player.usecase';
import { PlayerRepository } from '../repositories/player.repository';
import { Player } from '../entities/player.entity';
import { of } from 'rxjs';

describe('CreatePlayerUseCase', () => {
  let useCase: CreatePlayerUseCase;
  let mockRepository: jasmine.SpyObj<PlayerRepository>;

  beforeEach(() => {
    mockRepository = jasmine.createSpyObj('PlayerRepository', ['create']);
    useCase = new CreatePlayerUseCase(mockRepository);
  });

  it('should create player when all data is valid', (done) => {
    const player: Player = {
      nomeCompleto: 'Gabriel Barbosa',
      dataNascimento: '1996-08-30',
      pePreferencial: 2,
      altura: 178,
      peso: 73,
      posicaoPrincipal: 8,
      caminhoFoto: 'assets/gabriel.png',
      bioHistorico: 'Atacante do Flamengo.'
    };

    mockRepository.create.and.returnValue(of(player));

    useCase.execute(player).subscribe({
      next: (result) => {
        expect(result).toEqual(player);
        expect(mockRepository.create).toHaveBeenCalledWith(player);
        done();
      }
    });
  });

  it('should throw error when name is empty', (done) => {
    const player: Player = {
      nomeCompleto: '',
      dataNascimento: '1996-08-30',
      pePreferencial: 2,
      altura: 178,
      peso: 73,
      posicaoPrincipal: 8,
      caminhoFoto: 'assets/gabriel.png',
      bioHistorico: 'Atacante'
    };

    useCase.execute(player).subscribe({
      error: (err) => {
        expect(err.message).toBe('O nome completo é obrigatório.');
        expect(mockRepository.create).not.toHaveBeenCalled();
        done();
      }
    });
  });

  it('should throw error when height is zero or negative', (done) => {
    const player: Player = {
      nomeCompleto: 'Gabriel',
      dataNascimento: '1996-08-30',
      pePreferencial: 2,
      altura: 0,
      peso: 73,
      posicaoPrincipal: 8,
      caminhoFoto: 'assets/gabriel.png',
      bioHistorico: 'Atacante'
    };

    useCase.execute(player).subscribe({
      error: (err) => {
        expect(err.message).toBe('A altura deve ser maior que 0 cm.');
        expect(mockRepository.create).not.toHaveBeenCalled();
        done();
      }
    });
  });
});
