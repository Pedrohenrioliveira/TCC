import '../../domain/entities/player.dart';
import '../../domain/repositories/player_repository.dart';
import '../datasources/player_remote_datasource.dart';
import '../dto/player_dto.dart';

class PlayerRepositoryImpl implements PlayerRepository {
  final PlayerRemoteDataSource dataSource;

  PlayerRepositoryImpl(this.dataSource);

  @override
  Future<Player> create(Player player) async {
    final jsonRequest = PlayerDto.toJson(player);
    final jsonResponse = await dataSource.createPlayer(jsonRequest);
    return PlayerDto.fromJson(jsonResponse);
  }

  @override
  Future<Player> update(Player player) async {
    if (player.id == null) {
      throw Exception('ID é obrigatório para atualização.');
    }
    final jsonRequest = PlayerDto.toJson(player);
    await dataSource.updatePlayer(player.id!, jsonRequest);
    return player;
  }

  @override
  Future<Player> findById(String id) async {
    final jsonResponse = await dataSource.getPlayerById(id);
    return PlayerDto.fromJson(jsonResponse);
  }

  @override
  Future<List<Player>> findAll() async {
    final jsonList = await dataSource.getPlayers();
    return jsonList.map((e) => PlayerDto.fromJson(e)).toList();
  }
}
