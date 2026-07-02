enum PlayerRegisterStatus { initial, loading, success, failure }

class PlayerRegisterState {
  final PlayerRegisterStatus status;
  final String? errorMessage;
  final String? selectedImagePath; // Caminho local para preview da imagem

  const PlayerRegisterState({
    required this.status,
    this.errorMessage,
    this.selectedImagePath,
  });

  factory PlayerRegisterState.initial() => const PlayerRegisterState(
        status: PlayerRegisterStatus.initial,
      );

  PlayerRegisterState copyWith({
    PlayerRegisterStatus? status,
    String? errorMessage,
    String? selectedImagePath,
  }) {
    return PlayerRegisterState(
      status: status ?? this.status,
      errorMessage: errorMessage ?? this.errorMessage,
      selectedImagePath: selectedImagePath ?? this.selectedImagePath,
    );
  }
}
