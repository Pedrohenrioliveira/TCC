import '../../domain/entities/player.dart';

class PlayerDto {
  static Map<String, dynamic> toJson(Player player) {
    return {
      if (player.id != null) 'id': player.id,
      'caminhoFoto': player.caminhoFoto,
      'nomeCompleto': player.nomeCompleto.trim(),
      'dataNascimento': player.dataNascimento.toIso8601String(),
      'pePreferencial': player.pePreferencial,
      'altura': player.altura,
      'peso': player.peso,
      'posicaoPrincipal': player.posicaoPrincipal,
      'posicaoSecundaria': player.posicaoSecundaria,
      'bioHistorico': player.bioHistorico.trim(),
      'clubeId': player.clubeId,
    };
  }

  static Player fromJson(Map<String, dynamic> json) {
    return Player(
      id: json['id'] as String?,
      caminhoFoto: json['caminhoFoto'] as String? ?? '',
      nomeCompleto: json['nomeCompleto'] as String? ?? '',
      dataNascimento: json['dataNascimento'] != null 
          ? DateTime.parse(json['dataNascimento'] as String)
          : DateTime.now(),
      pePreferencial: json['pePreferencial'] as int? ?? 2,
      altura: json['altura'] as int? ?? 0,
      peso: (json['peso'] as num?)?.toDouble() ?? 0.0,
      posicaoPrincipal: json['posicaoPrincipal'] as int? ?? 1,
      posicaoSecundaria: json['posicaoSecundaria'] as int?,
      bioHistorico: json['bioHistorico'] as String? ?? '',
      clubeId: json['clubeId'] as String?,
    );
  }
}
