import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../../../core/theme/app_theme.dart';
import '../controllers/player_register_controller.dart';
import '../state/player_register_state.dart';
import '../widgets/image_picker_widget.dart';

class PlayerRegisterPage extends ConsumerStatefulWidget {
  const PlayerRegisterPage({super.key});

  @override
  ConsumerState<PlayerRegisterPage> createState() => _PlayerRegisterPageState();
}

class _PlayerRegisterPageState extends ConsumerState<PlayerRegisterPage> {
  final _formKey = GlobalKey<FormState>();

  final _nomeController = TextEditingController();
  final _alturaController = TextEditingController();
  final _pesoController = TextEditingController();
  final _bioController = TextEditingController();

  DateTime? _selectedDate;
  int _selectedFoot = 2; // Direito por padrão (1 = Esquerdo, 2 = Direito, 3 = Ambos)
  int? _selectedPrimaryPosition;
  int? _selectedSecondaryPosition;

  final List<Map<String, dynamic>> _posicoes = [
    {'value': 1, 'label': 'Goleiro'},
    {'value': 2, 'label': 'Lateral Direito'},
    {'value': 3, 'label': 'Zagueiro'},
    {'value': 4, 'label': 'Lateral Esquerdo'},
    {'value': 5, 'label': 'Volante'},
    {'value': 6, 'label': 'Meio-Campo'},
    {'value': 7, 'label': 'Ponta'},
    {'value': 8, 'label': 'Centroavante (Atacante)'},
  ];

  @override
  void dispose() {
    _nomeController.dispose();
    _alturaController.dispose();
    _pesoController.dispose();
    _bioController.dispose();
    super.dispose();
  }

  Future<void> _selectDate(BuildContext context) async {
    final DateTime? picked = await showDatePicker(
      context: context,
      initialDate: DateTime.now().subtract(const Duration(days: 365 * 18)), // 18 anos
      firstDate: DateTime(1980),
      lastDate: DateTime.now(),
      builder: (context, child) {
        return Theme(
          data: Theme.of(context).copyWith(
            colorScheme: const ColorScheme.light(
              primary: AppTheme.primaryColor,
              onPrimary: Colors.white,
              onSurface: AppTheme.textColor,
            ),
          ),
          child: child!,
        );
      },
    );

    if (picked != null && picked != _selectedDate) {
      setState(() {
        _selectedDate = picked;
      });
    }
  }

  void _submitForm() {
    if (!_formKey.currentState!.validate()) return;
    if (_selectedDate == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('A data de nascimento é obrigatória.'),
          backgroundColor: Colors.redAccent,
        ),
      );
      return;
    }
    if (_selectedPrimaryPosition == null) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('A posição principal é obrigatória.'),
          backgroundColor: Colors.redAccent,
        ),
      );
      return;
    }

    ref.read(playerRegisterControllerProvider.notifier).registerPlayer(
          nomeCompleto: _nomeController.text,
          dataNascimento: _selectedDate!,
          pePreferencial: _selectedFoot,
          altura: int.parse(_alturaController.text),
          peso: double.parse(_pesoController.text),
          posicaoPrincipal: _selectedPrimaryPosition!,
          posicaoSecundaria: _selectedSecondaryPosition,
          bioHistorico: _bioController.text,
        );
  }

  void _resetFields() {
    _nomeController.clear();
    _alturaController.clear();
    _pesoController.clear();
    _bioController.clear();
    setState(() {
      _selectedDate = null;
      _selectedFoot = 2;
      _selectedPrimaryPosition = null;
      _selectedSecondaryPosition = null;
    });
  }

  @override
  Widget build(BuildContext context) {
    // Escutar eventos de sucesso e erro do Riverpod Controller
    ref.listen<PlayerRegisterState>(playerRegisterControllerProvider, (previous, next) {
      if (next.status == PlayerRegisterStatus.success) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text('Jogador cadastrado com sucesso!'),
            backgroundColor: AppTheme.accentColor,
          ),
        );
        _resetFields();
        ref.read(playerRegisterControllerProvider.notifier).reset();
      } else if (next.status == PlayerRegisterStatus.failure) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text(next.errorMessage ?? 'Ocorreu um erro ao cadastrar.'),
            backgroundColor: Colors.redAccent,
          ),
        );
      }
    });

    final state = ref.watch(playerRegisterControllerProvider);
    final isLoading = state.status == PlayerRegisterStatus.loading;

    return Scaffold(
      body: SafeArea(
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              // Header degradê cinza
              Container(
                decoration: const BoxDecoration(
                  gradient: LinearGradient(
                    begin: Alignment.topCenter,
                    end: Alignment.bottomCenter,
                    colors: [Color(0xFFEAEAEA), Color(0xFFFAFAFA)],
                  ),
                ),
                padding: const EdgeInsets.symmetric(horizontal: 24, vertical: 32),
                child: const Column(
                  children: [
                    Text(
                      'Cadastro de Novo Talento',
                      style: TextStyle(
                        fontSize: 28,
                        fontWeight: FontWeight.w800,
                        color: AppTheme.primaryColor,
                        letterSpacing: -0.5,
                      ),
                      textAlign: TextAlign.center,
                    ),
                    SizedBox(height: 8),
                    Text(
                      'Registre um novo atleta no banco de dados.',
                      style: TextStyle(
                        fontSize: 14,
                        color: AppTheme.subtextColor,
                      ),
                      textAlign: TextAlign.center,
                    ),
                  ],
                ),
              ),

              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 24, vertical: 8),
                child: Form(
                  key: _formKey,
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                    children: [
                      // Componente de Upload de Imagem
                      const ImagePickerWidget(),

                      // Nome Completo
                      const Text(
                        'Nome Completo',
                        style: TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w600,
                          color: Color(0xFF475569),
                        ),
                      ),
                      const SizedBox(height: 8),
                      TextFormField(
                        controller: _nomeController,
                        style: const TextStyle(fontSize: 15),
                        textCapitalization: TextCapitalization.words,
                        decoration: const InputDecoration(
                          hintText: 'Ex: Gabriel Barbosa',
                        ),
                        validator: (value) {
                          if (value == null || value.trim().isEmpty) {
                            return 'Nome completo é obrigatório.';
                          }
                          return null;
                        },
                      ),
                      const SizedBox(height: 20),

                      // Data de Nascimento
                      const Text(
                        'Data de Nascimento',
                        style: TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w600,
                          color: Color(0xFF475569),
                        ),
                      ),
                      const SizedBox(height: 8),
                      GestureDetector(
                        onTap: () => _selectDate(context),
                        child: Container(
                          padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
                          decoration: BoxDecoration(
                            color: Colors.white,
                            border: Border.all(color: AppTheme.borderColor),
                            borderRadius: BorderRadius.circular(12),
                          ),
                          child: Row(
                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                            children: [
                              Text(
                                _selectedDate == null
                                    ? 'dd/mm/aaaa'
                                    : '${_selectedDate!.day.toString().padLeft(2, '0')}/${_selectedDate!.month.toString().padLeft(2, '0')}/${_selectedDate!.year}',
                                style: TextStyle(
                                  fontSize: 15,
                                  color: _selectedDate == null
                                      ? const Color(0xFF94A3B8)
                                      : AppTheme.textColor,
                                ),
                              ),
                              const Icon(
                                Icons.calendar_today_outlined,
                                size: 18,
                                color: AppTheme.subtextColor,
                              ),
                            ],
                          ),
                        ),
                      ),
                      const SizedBox(height: 20),

                      // Pé Preferencial
                      const Text(
                        'Pé Preferencial',
                        style: TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w600,
                          color: Color(0xFF475569),
                        ),
                      ),
                      const SizedBox(height: 8),
                      Container(
                        padding: const EdgeInsets.all(4),
                        decoration: BoxDecoration(
                          color: const Color(0xFFF1F5F9),
                          border: Border.all(color: const Color(0xFFE2E8F0)),
                          borderRadius: BorderRadius.circular(12),
                        ),
                        child: Row(
                          children: [
                            _buildFootButton(1, 'Esquerdo'),
                            _buildFootButton(2, 'Direito'),
                            _buildFootButton(3, 'Ambos'),
                          ],
                        ),
                      ),
                      const SizedBox(height: 20),

                      // Altura e Peso Lado a Lado
                      Row(
                        children: [
                          // Altura
                          Expanded(
                            child: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                const Text(
                                  'Altura (cm)',
                                  style: TextStyle(
                                    fontSize: 14,
                                    fontWeight: FontWeight.w600,
                                    color: Color(0xFF475569),
                                  ),
                                ),
                                const SizedBox(height: 8),
                                TextFormField(
                                  controller: _alturaController,
                                  keyboardType: TextInputType.number,
                                  style: const TextStyle(fontSize: 15),
                                  decoration: const InputDecoration(
                                    hintText: '185',
                                  ),
                                  validator: (value) {
                                    if (value == null || value.isEmpty) {
                                      return 'Obrigatório.';
                                    }
                                    final n = int.tryParse(value);
                                    if (n == null || n <= 0) {
                                      return 'Inválido.';
                                    }
                                    return null;
                                  },
                                ),
                              ],
                            ),
                          ),
                          const SizedBox(width: 16),
                          // Peso
                          Expanded(
                            child: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                const Text(
                                  'Peso (kg)',
                                  style: TextStyle(
                                    fontSize: 14,
                                    fontWeight: FontWeight.w600,
                                    color: Color(0xFF475569),
                                  ),
                                ),
                                const SizedBox(height: 8),
                                TextFormField(
                                  controller: _pesoController,
                                  keyboardType: const TextInputType.numberWithOptions(decimal: true),
                                  style: const TextStyle(fontSize: 15),
                                  decoration: const InputDecoration(
                                    hintText: '78',
                                  ),
                                  validator: (value) {
                                    if (value == null || value.isEmpty) {
                                      return 'Obrigatório.';
                                    }
                                    final n = double.tryParse(value);
                                    if (n == null || n <= 0) {
                                      return 'Inválido.';
                                    }
                                    return null;
                                  },
                                ),
                              ],
                            ),
                          ),
                        ],
                      ),
                      const SizedBox(height: 20),

                      // Card das Posições com Borda Esquerda Amarela
                      Container(
                        decoration: BoxDecoration(
                          color: Colors.white,
                          border: const Border(
                            left: BorderSide(color: AppTheme.warningColor, width: 4),
                          ),
                          borderRadius: const BorderRadius.only(
                            topRight: Radius.circular(12),
                            bottomRight: Radius.circular(12),
                          ),
                          boxShadow: [
                            BoxShadow(
                              color: Colors.black.withOpacity(0.01),
                              blurRadius: 4,
                              offset: const Offset(0, 2),
                            ),
                          ],
                        ),
                        padding: const EdgeInsets.all(16),
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            // Posição Principal
                            const Text(
                              'Posição Principal',
                              style: TextStyle(
                                fontSize: 14,
                                fontWeight: FontWeight.w600,
                                color: Color(0xFF475569),
                              ),
                            ),
                            const SizedBox(height: 8),
                            DropdownButtonFormField<int>(
                              value: _selectedPrimaryPosition,
                              hint: const Text('Selecionar Posição'),
                              style: const TextStyle(fontSize: 15, color: AppTheme.textColor),
                              dropdownColor: Colors.white,
                              icon: const Icon(Icons.keyboard_arrow_down, color: AppTheme.subtextColor),
                              decoration: const InputDecoration(
                                contentPadding: EdgeInsets.symmetric(horizontal: 16, vertical: 4),
                              ),
                              items: _posicoes
                                  .map((p) => DropdownMenuItem<int>(
                                        value: p['value'] as int,
                                        child: Text(p['label'] as String),
                                      ))
                                  .toList(),
                              onChanged: (value) {
                                setState(() {
                                  _selectedPrimaryPosition = value;
                                });
                              },
                              validator: (value) {
                                if (value == null) {
                                  return 'Campo obrigatório.';
                                }
                                return null;
                              },
                            ),
                            const SizedBox(height: 16),

                            // Posição Secundária (Opcional)
                            const Text(
                              'Posição Secundária (Opcional)',
                              style: TextStyle(
                                fontSize: 14,
                                fontWeight: FontWeight.w600,
                                color: Color(0xFF475569),
                              ),
                            ),
                            const SizedBox(height: 8),
                            DropdownButtonFormField<int>(
                              value: _selectedSecondaryPosition,
                              hint: const Text('Opcional'),
                              style: const TextStyle(fontSize: 15, color: AppTheme.textColor),
                              dropdownColor: Colors.white,
                              icon: const Icon(Icons.keyboard_arrow_down, color: AppTheme.subtextColor),
                              decoration: const InputDecoration(
                                contentPadding: EdgeInsets.symmetric(horizontal: 16, vertical: 4),
                              ),
                              items: [
                                const DropdownMenuItem<int>(
                                  value: null,
                                  child: Text('Opcional'),
                                ),
                                ..._posicoes.map((p) => DropdownMenuItem<int>(
                                      value: p['value'] as int,
                                      child: Text(p['label'] as String),
                                    )),
                              ],
                              onChanged: (value) {
                                setState(() {
                                  _selectedSecondaryPosition = value;
                                });
                              },
                            ),
                          ],
                        ),
                      ),
                      const SizedBox(height: 20),

                      // Bio e Histórico no Futebol
                      const Text(
                        'Bio e Histórico no Futebol',
                        style: TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w600,
                          color: Color(0xFF475569),
                        ),
                      ),
                      const SizedBox(height: 8),
                      TextFormField(
                        controller: _bioController,
                        maxLines: 4,
                        style: const TextStyle(fontSize: 15),
                        decoration: const InputDecoration(
                          hintText: 'Mencione clubes anteriores, títulos e características de jogo...',
                        ),
                      ),
                      const SizedBox(height: 32),

                      // Botão Cadastrar Jogador (Pill Shape)
                      ElevatedButton(
                        onPressed: isLoading ? null : _submitForm,
                        child: isLoading
                            ? const SizedBox(
                                width: 20,
                                height: 20,
                                child: CircularProgressIndicator(
                                  color: Colors.white,
                                  strokeWidth: 2.5,
                                ),
                              )
                            : Row(
                                mainAxisAlignment: MainAxisAlignment.center,
                                children: const [
                                  Icon(Icons.person_add_alt_1_outlined, size: 20),
                                  SizedBox(width: 8),
                                  Text('Cadastrar Jogador'),
                                ],
                              ),
                      ),
                      const SizedBox(height: 24),
                    ],
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildFootButton(int footValue, String label) {
    final isActive = _selectedFoot == footValue;
    return Expanded(
      child: GestureDetector(
        onTap: () {
          setState(() {
            _selectedFoot = footValue;
          });
        },
        child: AnimatedContainer(
          duration: const Duration(milliseconds: 150),
          padding: const EdgeInsets.symmetric(vertical: 10),
          decoration: BoxDecoration(
            color: isActive ? Colors.white : Colors.transparent,
            borderRadius: BorderRadius.circular(8),
            boxShadow: isActive
                ? [
                    const BoxShadow(
                      color: Colors.black12,
                      blurRadius: 4,
                      offset: Offset(0, 2),
                    )
                  ]
                : null,
          ),
          child: Text(
            label,
            style: TextStyle(
              fontSize: 14,
              fontWeight: isActive ? FontWeight.w600 : FontWeight.w500,
              color: isActive ? AppTheme.primaryColor : const Color(0xFF475569),
            ),
            textAlign: TextAlign.center,
          ),
        ),
      ),
    );
  }
}
