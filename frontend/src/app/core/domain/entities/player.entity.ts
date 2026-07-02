export interface Player {
  id?: string;
  caminhoFoto: string;
  nomeCompleto: string;
  dataNascimento: string; // Formato yyyy-MM-dd
  pePreferencial: number; // 1 = Esquerdo, 2 = Direito, 3 = Ambos
  altura: number; // em cm
  peso: number; // em kg
  posicaoPrincipal: number; // Enums de 1 a 8
  posicaoSecundaria?: number | null;
  bioHistorico: string;
  clubeId?: string | null;
}
