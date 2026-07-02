import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../../../core/network/dio_client.dart';
import '../../data/datasources/player_remote_datasource.dart';
import '../../data/repositories/player_repository_impl.dart';
import '../../domain/entities/player.dart';
import '../../domain/repositories/player_repository.dart';
import '../../domain/usecases/create_player_usecase.dart';
import '../state/player_register_state.dart';

// Provedores de Injeção de Dependências
final dioClientProvider = Provider<DioClient>((ref) => DioClient());

final playerRemoteDataSourceProvider = Provider<PlayerRemoteDataSource>((ref) {
  final dioClient = ref.watch(dioClientProvider);
  return PlayerRemoteDataSource(dioClient.dio);
});

final playerRepositoryProvider = Provider<PlayerRepository>((ref) {
  final dataSource = ref.watch(playerRemoteDataSourceProvider);
  return PlayerRepositoryImpl(dataSource);
});

final createPlayerUseCaseProvider = Provider<CreatePlayerUseCase>((ref) {
  final repository = ref.watch(playerRepositoryProvider);
  return CreatePlayerUseCase(repository);
});

// Classe Controller do estado da tela
class PlayerRegisterController extends StateNotifier<PlayerRegisterState> {
  final CreatePlayerUseCase _createPlayerUseCase;

  PlayerRegisterController(this._createPlayerUseCase) : super(PlayerRegisterState.initial());

  void setImagePath(String? path) {
    state = state.copyWith(selectedImagePath: path);
  }

  Future<void> registerPlayer({
    required String nomeCompleto,
    required DateTime dataNascimento,
    required int pePreferencial,
    required int altura,
    required double peso,
    required int posicaoPrincipal,
    int? posicaoSecundaria,
    required String bioHistorico,
  }) async {
    state = state.copyWith(status: PlayerRegisterStatus.loading);

    try {
      // O banco armazena caminhoFoto de tamanho máximo 500. Enviamos um caminho simbólico
      // para o backend C#, enquanto guardamos o path real da imagem para visualização local.
      final String photoPathSymbol = state.selectedImagePath != null
          ? 'photo_${DateTime.now().millisecondsSinceEpoch}.jpg'
          : '';

      final player = Player(
        nomeCompleto: nomeCompleto,
        dataNascimento: dataNascimento,
        pePreferencial: pePreferencial,
        altura: altura,
        peso: peso,
        posicaoPrincipal: posicaoPrincipal,
        posicaoSecundaria: posicaoSecundaria,
        bioHistorico: bioHistorico,
        caminhoFoto: photoPathSymbol,
      );

      await _createPlayerUseCase(player);
      state = state.copyWith(
        status: PlayerRegisterStatus.success,
        // Limpar imagem selecionada após o sucesso
        selectedImagePath: null,
      );
    } catch (e) {
      state = state.copyWith(
        status: PlayerRegisterStatus.failure,
        errorMessage: e.toString().replaceAll('Exception: ', ''),
      );
    }
  }

  void reset() {
    state = PlayerRegisterState.initial();
  }
}

final playerRegisterControllerProvider =
    StateNotifierProvider<PlayerRegisterController, PlayerRegisterState>((ref) {
  final createPlayerUseCase = ref.watch(createPlayerUseCaseProvider);
  return PlayerRegisterController(createPlayerUseCase);
});
