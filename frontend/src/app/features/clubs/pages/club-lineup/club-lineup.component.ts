import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ClubProfileService } from '../../services/club-profile.service';
import { ClubRequestService, ClubRequestForClub } from '../../../club-requests/services/club-request.service';

export interface LineupPlayer {
  id: string;
  name: string;
  photoUrl: string;
  position: string;
  overall: number;
}

export interface ClubLineupData {
  formation: string;
  titulares: { [slotIndex: number]: LineupPlayer | null };
  reservas: LineupPlayer[];
}

@Component({
  selector: 'app-club-lineup',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './club-lineup.component.html',
  styleUrls: ['./club-lineup.component.css']
})
export class ClubLineupComponent implements OnInit {
  clubId = '';
  loading = true;
  saving = false;
  
  // Available players to pick from (Approved requests)
  availablePlayers: LineupPlayer[] = [];
  
  // The current lineup state
  lineup: ClubLineupData = {
    formation: '4-3-3',
    titulares: {
      1: null, 2: null, 3: null, 4: null, 5: null,
      6: null, 7: null, 8: null, 9: null, 10: null, 11: null
    },
    reservas: []
  };

  // 1 to 11 slots for 4-3-3
  slots = [
    { index: 9, pos: 'ATA', label: 'Ponta Esq' }, // top left
    { index: 10, pos: 'ATA', label: 'Centroavante' }, // top center
    { index: 11, pos: 'ATA', label: 'Ponta Dir' }, // top right
    { index: 6, pos: 'MEI', label: 'Meia Esq' }, // mid left
    { index: 7, pos: 'VOL', label: 'Volante' }, // mid center
    { index: 8, pos: 'MEI', label: 'Meia Dir' }, // mid right
    { index: 2, pos: 'LAT', label: 'Lat Esq' }, // bot left
    { index: 3, pos: 'ZAG', label: 'Zagueiro' }, // bot mid left
    { index: 4, pos: 'ZAG', label: 'Zagueiro' }, // bot mid right
    { index: 5, pos: 'LAT', label: 'Lat Dir' }, // bot right
    { index: 1, pos: 'GOL', label: 'Goleiro' } // very bottom center
  ];

  // UI state for modal
  showPlayerModal = false;
  selectingSlotIndex: number | null = null;
  selectingForBench = false;
  searchQuery = '';

  constructor(
    private profileService: ClubProfileService,
    private requestService: ClubRequestService
  ) {}

  ngOnInit(): void {
    this.clubId = localStorage.getItem('loggedClubId') || '';
    if (this.clubId) {
      this.loadData();
    }
  }

  loadData(): void {
    this.loading = true;
    
    // 1. Fetch club players (approved requests)
    this.requestService.getRequestsForClub(this.clubId).subscribe({
      next: (res: any) => {
        if (res.ok && res.dados) {
          // Status 2 = Aceita
          const approved = res.dados.filter((r: any) => r.status === 2);
          this.availablePlayers = approved.map((r: any) => ({
            id: r.jogadorId,
            name: r.nomeJogador || 'Jogador',
            photoUrl: r.caminhoFotoJogador || '',
            position: 'ATA', // Mock position since it's not in the request view yet
            overall: 70 + Math.floor(Math.random() * 20) // Mock overall
          }));
        }
        
        // 2. Fetch saved lineup
        this.profileService.getEscalacao(this.clubId).subscribe({
          next: (escRes) => {
            if (escRes.ok && escRes.dados && escRes.dados !== '{}') {
              try {
                const parsed = JSON.parse(escRes.dados);
                if (parsed.titulares) {
                  this.lineup = parsed;
                }
              } catch (e) {
                console.error('Error parsing escalacao', e);
              }
            }
            this.loading = false;
          },
          error: () => {
            this.loading = false;
          }
        });
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  saveLineup(): void {
    this.saving = true;
    const jsonStr = JSON.stringify(this.lineup);
    this.profileService.updateEscalacao(this.clubId, jsonStr).subscribe({
      next: (res) => {
        this.saving = false;
        alert('Escalação salva com sucesso!');
      },
      error: () => {
        this.saving = false;
        alert('Erro ao salvar escalação.');
      }
    });
  }

  openPlayerSelection(slotIndex: number): void {
    this.selectingSlotIndex = slotIndex;
    this.selectingForBench = false;
    this.showPlayerModal = true;
    this.searchQuery = '';
  }

  openBenchSelection(): void {
    this.selectingSlotIndex = null;
    this.selectingForBench = true;
    this.showPlayerModal = true;
    this.searchQuery = '';
  }

  closeModal(): void {
    this.showPlayerModal = false;
  }

  get filteredPlayers(): LineupPlayer[] {
    // Exclude players already in the lineup (optional, but good UX)
    const inLineupIds = new Set<string>();
    
    Object.values(this.lineup.titulares).forEach(p => {
      if (p) inLineupIds.add(p.id);
    });
    
    this.lineup.reservas.forEach(p => {
      if (p) inLineupIds.add(p.id);
    });

    return this.availablePlayers.filter(p => {
      if (inLineupIds.has(p.id)) return false;
      
      if (!this.searchQuery) return true;
      return p.name.toLowerCase().includes(this.searchQuery.toLowerCase());
    });
  }

  selectPlayer(player: LineupPlayer): void {
    if (this.selectingForBench) {
      this.lineup.reservas.push(player);
    } else if (this.selectingSlotIndex !== null) {
      this.lineup.titulares[this.selectingSlotIndex] = player;
    }
    this.closeModal();
  }

  removePlayer(slotIndex: number, event: Event): void {
    event.stopPropagation();
    this.lineup.titulares[slotIndex] = null;
  }

  handleImageError(player: any): void { if(player) player.photoUrl = 'data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSI0NCIgaGVpZ2h0PSI0NCIgdmlld0JveD0iMCAwIDI0IDI0Ij48Y2lyY2xlIGN4PSIxMiIgY3k9IjgiIHI9IjQiIGZpbGw9IiM1NTUiLz48cGF0aCBkPSJNMTIgMTRjLTQuNDIgMC04IDMuNTgtOCA4aDE2YzAtNC40Mi0zLjU4LTgtOC04eiIgZmlsbD0iIzU1NSIvPjwvc3ZnPg=='; } 

  removeBenchPlayer(index: number): void {
    this.lineup.reservas.splice(index, 1);
  }
}
