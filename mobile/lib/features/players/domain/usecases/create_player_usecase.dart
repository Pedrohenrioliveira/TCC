import '../entities/player.dart';
import '../repositories/player_repository.dart';

class CreatePlayerUseCase {
  final PlayerRepository repository;

  CreatePlayerUseCase(this.repository);

  Future<Player> call(Player player) async {
    // Validações de negócio/domínio
    if (player.nomeCompleto.trim().isEmpty) {
      throw Exception('O nome completo é obrigatório.');
    }
    if (player.altura <= 0) {
      throw Exception('A altura deve ser maior que 0 cm.');
    }
    if (player.peso <= 0) {
      throw Exception('O peso deve ser maior que 0 kg.');
    }
    if (player.posicaoPrincipal < 1 || player.posicaoPrincipal > 8) {
      throw Exception('A posição principal é obrigatória e deve ser válida.');
    }
    return await repository.create(player);
  }
}
