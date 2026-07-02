class Player {
  final String? id;
  final String caminhoFoto;
  final String nomeCompleto;
  final DateTime dataNascimento;
  final int pePreferencial; // 1 = Esquerdo, 2 = Direito, 3 = Ambos
  final int altura;
  final double peso;
  final int posicaoPrincipal;
  final int? posicaoSecundaria;
  final String bioHistorico;
  final String? clubeId;

  const Player({
    this.id,
    required this.caminhoFoto,
    required this.nomeCompleto,
    required this.dataNascimento,
    required this.pePreferencial,
    required this.altura,
    required this.peso,
    required this.posicaoPrincipal,
    this.posicaoSecundaria,
    required this.bioHistorico,
    this.clubeId,
  });

  Player copyWith({
    String? id,
    String? caminhoFoto,
    String? nomeCompleto,
    DateTime? dataNascimento,
    int? pePreferencial,
    int? altura,
    double? peso,
    int? posicaoPrincipal,
    int? posicaoSecundaria,
    String? bioHistorico,
    String? clubeId,
  }) {
    return Player(
      id: id ?? this.id,
      caminhoFoto: caminhoFoto ?? this.caminhoFoto,
      nomeCompleto: nomeCompleto ?? this.nomeCompleto,
      dataNascimento: dataNascimento ?? this.dataNascimento,
      pePreferencial: pePreferencial ?? this.pePreferencial,
      altura: altura ?? this.altura,
      peso: peso ?? this.peso,
      posicaoPrincipal: posicaoPrincipal ?? this.posicaoPrincipal,
      posicaoSecundaria: posicaoSecundaria ?? this.posicaoSecundaria,
      bioHistorico: bioHistorico ?? this.bioHistorico,
      clubeId: clubeId ?? this.clubeId,
    );
  }
}
