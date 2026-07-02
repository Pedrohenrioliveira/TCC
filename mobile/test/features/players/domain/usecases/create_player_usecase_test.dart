import 'package:flutter_test/flutter_test.dart';
import 'package:tcc_mobile/features/players/domain/entities/player.dart';
import 'package:tcc_mobile/features/players/domain/repositories/player_repository.dart';
import 'package:tcc_mobile/features/players/domain/usecases/create_player_usecase.dart';

class MockPlayerRepository implements PlayerRepository {
  final List<Player> db = [];
  bool createCalled = false;

  @override
  Future<Player> create(Player player) async {
    createCalled = true;
    final newPlayer = player.copyWith(id: 'mock-guid-123');
    db.add(newPlayer);
    return newPlayer;
  }

  @override
  Future<List<Player>> findAll() async => db;

  @override
  Future<Player> findById(String id) async => db.firstWhere((e) => e.id == id);

  @override
  Future<Player> update(Player player) async => player;
}

void main() {
  late MockPlayerRepository mockRepository;
  late CreatePlayerUseCase useCase;

  setUp(() {
    mockRepository = MockPlayerRepository();
    useCase = CreatePlayerUseCase(mockRepository);
  });

  test('deve criar o jogador quando todos os campos forem validos', () async {
    final player = Player(
      nomeCompleto: 'Gabriel Barbosa',
      dataNascimento: DateTime(1996, 8, 30),
      pePreferencial: 2,
      altura: 178,
      peso: 73.0,
      posicaoPrincipal: 8,
      caminhoFoto: 'assets/photo.jpg',
      bioHistorico: 'Atacante do Flamengo',
    );

    final result = await useCase(player);

    expect(result.id, 'mock-guid-123');
    expect(mockRepository.createCalled, true);
  });

  test('deve lancar excecao quando o nome estiver vazio', () async {
    final player = Player(
      nomeCompleto: '',
      dataNascimento: DateTime(1996, 8, 30),
      pePreferencial: 2,
      altura: 178,
      peso: 73.0,
      posicaoPrincipal: 8,
      caminhoFoto: 'assets/photo.jpg',
      bioHistorico: 'Atacante',
    );

    expect(() => useCase(player), throwsException);
    expect(mockRepository.createCalled, false);
  });

  test('deve lancar excecao quando a altura for menor ou igual a zero', () async {
    final player = Player(
      nomeCompleto: 'Gabigol',
      dataNascimento: DateTime(1996, 8, 30),
      pePreferencial: 2,
      altura: 0,
      peso: 73.0,
      posicaoPrincipal: 8,
      caminhoFoto: 'assets/photo.jpg',
      bioHistorico: 'Atacante',
    );

    expect(() => useCase(player), throwsException);
    expect(mockRepository.createCalled, false);
  });
}
