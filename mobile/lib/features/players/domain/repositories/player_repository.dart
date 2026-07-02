import '../entities/player.dart';

abstract class PlayerRepository {
  Future<Player> create(Player player);
  Future<Player> update(Player player);
  Future<Player> findById(String id);
  Future<List<Player>> findAll();
}
