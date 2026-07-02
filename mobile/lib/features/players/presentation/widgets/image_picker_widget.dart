import 'dart:io';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:image_picker/image_picker.dart';
import '../../../../core/theme/app_theme.dart';
import '../controllers/player_register_controller.dart';

class ImagePickerWidget extends ConsumerWidget {
  const ImagePickerWidget({super.key});

  Future<void> _pickImage(BuildContext context, WidgetRef ref, ImageSource source) async {
    try {
      final picker = ImagePicker();
      final pickedFile = await picker.pickImage(
        source: source,
        maxWidth: 1000,
        maxHeight: 1000,
        imageQuality: 80, // Compressão opcional recomendada
      );

      if (pickedFile != null) {
        final file = File(pickedFile.path);
        final sizeInBytes = await file.length();
        final sizeInMb = sizeInBytes / (1024 * 1024);

        if (sizeInMb > 2.0) {
          if (context.mounted) {
            ScaffoldMessenger.of(context).showSnackBar(
              const SnackBar(
                content: Text('A foto deve ter no máximo 2MB.'),
                backgroundColor: Colors.redAccent,
              ),
            );
          }
          return;
        }

        ref.read(playerRegisterControllerProvider.notifier).setImagePath(pickedFile.path);
      }
    } catch (e) {
      if (context.mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text('Erro ao selecionar imagem: $e'),
            backgroundColor: Colors.redAccent,
          ),
        );
      }
    }
  }

  void _showSourceDialog(BuildContext context, WidgetRef ref) {
    showModalBottomSheet(
      context: context,
      shape: const RoundedRectangleBorder(
        borderRadius: BorderRadius.vertical(top: Radius.circular(16)),
      ),
      builder: (context) => SafeArea(
        child: Wrap(
          children: [
            ListTile(
              leading: const Icon(Icons.camera_alt, color: AppTheme.primaryColor),
              title: const Text('Tirar Foto (Câmera)'),
              onTap: () {
                Navigator.of(context).pop();
                _pickImage(context, ref, ImageSource.camera);
              },
            ),
            ListTile(
              leading: const Icon(Icons.photo_library, color: AppTheme.primaryColor),
              title: const Text('Escolher da Galeria'),
              onTap: () {
                Navigator.of(context).pop();
                _pickImage(context, ref, ImageSource.gallery);
              },
            ),
          ],
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final state = ref.watch(playerRegisterControllerProvider);

    return Card(
      elevation: 0,
      margin: const EdgeInsets.only(bottom: 24),
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(16),
        side: const BorderSide(color: Color(0xFFF1F5F9)),
      ),
      child: Padding(
        padding: const EdgeInsets.all(20),
        child: Column(
          children: [
            GestureDetector(
              onTap: () => _showSourceDialog(context, ref),
              child: Stack(
                children: [
                  CircleAvatar(
                    radius: 56,
                    backgroundColor: const Color(0xFFF1F5F9),
                    backgroundImage: state.selectedImagePath != null
                        ? FileImage(File(state.selectedImagePath!))
                        : null,
                    child: state.selectedImagePath == null
                        ? const Icon(
                            Icons.person_outline,
                            size: 48,
                            color: Color(0xFF94A3B8),
                          )
                        : null,
                  ),
                  Positioned(
                    bottom: 0,
                    right: 4,
                    child: Container(
                      padding: const EdgeInsets.all(6),
                      decoration: const BoxDecoration(
                        color: AppTheme.primaryColor,
                        shape: BoxShape.circle,
                        boxShadow: [
                          BoxShadow(
                            color: Colors.black12,
                            blurRadius: 4,
                            offset: Offset(0, 2),
                          ),
                        ],
                      ),
                      child: const Icon(
                        Icons.camera_alt_outlined,
                        size: 14,
                        color: Colors.white,
                      ),
                    ),
                  ),
                ],
              ),
            ),
            const SizedBox(height: 16),
            const Text(
              'Foto do Atleta',
              style: TextStyle(
                fontSize: 15,
                fontWeight: FontWeight.w600,
                color: AppTheme.textColor,
              ),
            ),
            const SizedBox(height: 4),
            const Text(
              'Envie uma foto com o uniforme do clube.',
              style: TextStyle(
                fontSize: 12,
                color: AppTheme.subtextColor,
              ),
              textAlign: TextAlign.center,
            ),
            const SizedBox(height: 16),
            TextButton(
              onPressed: () => _showSourceDialog(context, ref),
              style: TextButton.styleFrom(
                foregroundColor: AppTheme.primaryColor,
                padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
              ),
              child: Text(
                state.selectedImagePath != null ? 'Alterar Foto' : 'Escolher Arquivo',
                style: const TextStyle(
                  fontWeight: FontWeight.bold,
                  decoration: TextDecoration.underline,
                ),
              ),
            ),
            if (state.selectedImagePath != null)
              TextButton(
                onPressed: () {
                  ref.read(playerRegisterControllerProvider.notifier).setImagePath(null);
                },
                style: TextButton.styleFrom(
                  foregroundColor: Colors.redAccent,
                  padding: EdgeInsets.zero,
                ),
                child: const Text(
                  'Remover Foto',
                  style: TextStyle(fontSize: 11, decoration: TextDecoration.underline),
                ),
              ),
          ],
        ),
      ),
    );
  }
}
