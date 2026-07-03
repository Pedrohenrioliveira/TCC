export interface PlayerCreateRequest {
  caminhoFoto: string;
  email: string;
  senha: string;
  nomeCompleto: string;
  dataNascimento: string; // ISO ou string yyyy-MM-dd
  pePreferencial: number;
  altura: number;
  peso: number;
  posicaoPrincipal: number;
  posicaoSecundaria?: number | null;
  bioHistorico: string;
  clubeId?: string | null;
}

export interface PlayerResponseData {
  id: string;
  nomeCompleto: string;
  posicaoPrincipal: number;
  // Campos detalhados adicionais se necessário
  caminhoFoto?: string;
  dataNascimento?: string;
  pePreferencial?: number;
  altura?: number;
  peso?: number;
  posicaoSecundaria?: number | null;
  bioHistorico?: string;
  clubeId?: string | null;
}

export interface RespostaApi<T> {
  ok: boolean;
  mensagem: string;
  dados: T;
  erros?: string[] | null;
}
