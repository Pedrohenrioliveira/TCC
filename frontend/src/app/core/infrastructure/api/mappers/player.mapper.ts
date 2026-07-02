import { Player } from '../../../domain/entities/player.entity';
import { PlayerCreateRequest, PlayerResponseData } from '../dtos/player.dto';

export class PlayerMapper {
  static toDto(player: Player): PlayerCreateRequest {
    return {
      caminhoFoto: player.caminhoFoto,
      nomeCompleto: player.nomeCompleto,
      dataNascimento: player.dataNascimento, // no formato yyyy-MM-dd
      pePreferencial: player.pePreferencial,
      altura: player.altura,
      peso: player.peso,
      posicaoPrincipal: player.posicaoPrincipal,
      posicaoSecundaria: player.posicaoSecundaria || null,
      bioHistorico: player.bioHistorico,
      clubeId: player.clubeId || null
    };
  }

  static toEntity(dto: PlayerResponseData): Player {
    return {
      id: dto.id,
      nomeCompleto: dto.nomeCompleto,
      caminhoFoto: dto.caminhoFoto || '',
      dataNascimento: dto.dataNascimento ? dto.dataNascimento.split('T')[0] : '',
      pePreferencial: dto.pePreferencial || 1,
      altura: dto.altura || 0,
      peso: dto.peso || 0,
      posicaoPrincipal: dto.posicaoPrincipal,
      posicaoSecundaria: dto.posicaoSecundaria,
      bioHistorico: dto.bioHistorico || '',
      clubeId: dto.clubeId
    };
  }
}
