import 'package:dio/dio';

class PlayerRemoteDataSource {
  final Dio dio;

  PlayerRemoteDataSource(this.dio);

  Future<Map<String, dynamic>> createPlayer(Map<String, dynamic> playerJson) async {
    try {
      final response = await dio.post('jogadores', data: playerJson);
      if (response.data != null && response.data['ok'] == true) {
        return response.data['dados'] as Map<String, dynamic>;
      } else {
        throw Exception(response.data?['mensagem'] ?? 'Falha ao cadastrar jogador.');
      }
    } on DioException catch (e) {
      final message = e.response?.data?['mensagem'] ?? e.message ?? 'Erro de comunicação com o servidor';
      throw Exception(message);
    }
  }

  Future<Map<String, dynamic>> updatePlayer(String id, Map<String, dynamic> playerJson) async {
    try {
      final response = await dio.put('jogadores/$id', data: playerJson);
      if (response.data != null && response.data['ok'] == true) {
        return response.data;
      } else {
        throw Exception(response.data?['mensagem'] ?? 'Falha ao atualizar jogador.');
      }
    } on DioException catch (e) {
      final message = e.response?.data?['mensagem'] ?? e.message ?? 'Erro de comunicação com o servidor';
      throw Exception(message);
    }
  }

  Future<Map<String, dynamic>> getPlayerById(String id) async {
    try {
      final response = await dio.get('jogadores/$id');
      if (response.data != null && response.data['ok'] == true) {
        return response.data['dados'] as Map<String, dynamic>;
      } else {
        throw Exception(response.data?['mensagem'] ?? 'Jogador não encontrado.');
      }
    } on DioException catch (e) {
      final message = e.response?.data?['mensagem'] ?? e.message ?? 'Erro de comunicação com o servidor';
      throw Exception(message);
    }
  }

  Future<List<Map<String, dynamic>>> getPlayers() async {
    try {
      final response = await dio.get('jogadores');
      if (response.data != null && response.data['ok'] == true) {
        final list = response.data['dados']?['itens'] as List?;
        return list?.map((e) => e as Map<String, dynamic>).toList() ?? [];
      } else {
        throw Exception(response.data?['mensagem'] ?? 'Falha ao listar jogadores.');
      }
    } on DioException catch (e) {
      final message = e.response?.data?['mensagem'] ?? e.message ?? 'Erro de comunicação com o servidor';
      throw Exception(message);
    }
  }
}
