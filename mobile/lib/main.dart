import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'core/theme/app_theme.dart';
import 'features/players/presentation/pages/player_register_page.dart';

void main() {
  runApp(
    // ProviderScope é necessário para gerenciar o estado global com Riverpod
    const ProviderScope(
      child: AthleteManagerApp(),
    ),
  );
}

class AthleteManagerApp extends StatelessWidget {
  const AthleteManagerApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Talentos FUT',
      theme: AppTheme.lightTheme,
      debugShowCheckedModeBanner: false,
      home: const PlayerRegisterPage(),
    );
  }
}
