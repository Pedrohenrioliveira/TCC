import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PlayerProfileService, PlayerDetails } from '../../services/player-profile.service';

@Component({
  selector: 'app-player-profile-edit',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './player-profile-edit.component.html',
  styleUrls: ['./player-profile-edit.component.css']
})
export class PlayerProfileEditComponent implements OnInit {
  // TODO: Obter do auth
  jogadorId = localStorage.getItem('loggedUserId') || '00000000-0000-0000-0000-000000000001'; 
  player!: PlayerDetails;
  loading = true;
  error: string | null = null;
  
  activeTab: 'personal' | 'physical' = 'personal';

  personalForm!: FormGroup;
  physicalForm!: FormGroup;
  photoUrl: string = '';

  constructor(
    private fb: FormBuilder,
    private profileService: PlayerProfileService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initForms();
    this.loadProfile();
  }

  initForms(): void {
    this.personalForm = this.fb.group({
      nomeCompleto: ['', Validators.required],
      dataNascimento: ['', Validators.required],
      bioHistorico: ['']
    });

    this.physicalForm = this.fb.group({
      pePreferencial: [1, Validators.required],
      altura: [0, [Validators.required, Validators.min(100)]],
      peso: [0, [Validators.required, Validators.min(30)]],
      posicaoPrincipal: [1, Validators.required],
      posicaoSecundaria: [null]
    });
  }

  loadProfile(): void {
    this.loading = true;
    this.profileService.getProfile(this.jogadorId).subscribe({
      next: (res) => {
        if (res.ok) {
          this.player = res.dados;
          this.photoUrl = this.player.caminhoFoto;
          
          // Preencher formulários
          this.personalForm.patchValue({
            nomeCompleto: this.player.nomeCompleto,
            dataNascimento: this.player.dataNascimento ? new Date(this.player.dataNascimento).toISOString().split('T')[0] : '',
            bioHistorico: this.player.bioHistorico
          });

          this.physicalForm.patchValue({
            pePreferencial: this.player.pePreferencial,
            altura: this.player.altura,
            peso: this.player.peso,
            posicaoPrincipal: this.player.posicaoPrincipal,
            posicaoSecundaria: this.player.posicaoSecundaria
          });
        } else {
          this.error = res.mensagem;
        }
        this.loading = false;
      },
      error: () => {
        this.error = 'Erro ao carregar o perfil.';
        this.loading = false;
      }
    });
  }

  switchTab(tab: 'personal' | 'physical'): void {
    this.activeTab = tab;
  }

  savePersonalData(): void {
    if (this.personalForm.invalid) return;
    this.profileService.updatePersonalData(this.jogadorId, this.personalForm.value).subscribe({
      next: (res) => {
        if (res.ok) {
          alert('Dados pessoais atualizados!');
        } else {
          alert('Erro: ' + res.mensagem);
        }
      },
      error: () => alert('Erro na comunicação com a API.')
    });
  }

  savePhysicalData(): void {
    if (this.physicalForm.invalid) return;
    this.profileService.updatePhysicalData(this.jogadorId, this.physicalForm.value).subscribe({
      next: (res) => {
        if (res.ok) {
          alert('Dados físicos atualizados!');
        } else {
          alert('Erro: ' + res.mensagem);
        }
      },
      error: () => alert('Erro na comunicação com a API.')
    });
  }

  updatePhoto(url: string): void {
    if (!url) return;
    this.profileService.updatePhoto(this.jogadorId, url).subscribe({
      next: (res) => {
        if (res.ok) {
          this.photoUrl = url;
          alert('Foto atualizada!');
        } else {
          alert('Erro: ' + res.mensagem);
        }
      },
      error: () => alert('Erro na comunicação com a API.')
    });
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        const base64String = reader.result as string;
        this.updatePhoto(base64String);
      };
      reader.readAsDataURL(file);
    }
  }

  logout(): void {
    if (confirm('Tem certeza que deseja sair?')) {
      localStorage.removeItem('loggedUserId');
      localStorage.removeItem('userRole');
      this.router.navigate(['/login']);
    }
  }
}
