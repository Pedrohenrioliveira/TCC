export interface PostagemDto {
  id: string;
  caminhoFoto: string;
  descricao: string;
  dataPostagem: string;
}

export interface AdicionarPostagemCommand {
  perfilId: string;
  foto: File;
  descricao: string;
}
