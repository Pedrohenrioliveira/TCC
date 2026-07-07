import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ClubProfileService, ClubDetails } from '../../services/club-profile.service';
import { ImageUploadComponent } from '../../../players/components/image-upload/image-upload.component';

@Component({
  selector: 'app-club-profile-edit',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ImageUploadComponent],
  templateUrl: './club-profile-edit.component.html',
  styleUrls: ['./club-profile-edit.component.css']
})
export class ClubProfileEditComponent implements OnInit {
  // TODO: Obter do auth / login
  clubeId = localStorage.getItem('loggedClubId') || '00000000-0000-0000-0000-000000000001'; 
  club!: ClubDetails;
  loading = true;
  error: string | null = null;
  
  profileForm!: FormGroup;
  photoUrl: string = 'assets/default-club.png';

  constructor(
    private fb: FormBuilder,
    private profileService: ClubProfileService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadProfile();
  }
  initForm(): void {
    this.profileForm = this.fb.group({
      nome: ['', Validators.required],
      cidadeEstado: ['', Validators.required],
      contato: [''], // Não tem no entity Clube nativamente, mas vamos deixar no form
      anoFundacao: ['', Validators.required],
      ligaCompeticao: ['', Validators.required],
      estadioPrincipal: [''],
      breveHistoria: ['']
    });
  }

  loadProfile(): void {
    this.loading = true;
    this.profileService.getProfile(this.clubeId).subscribe({
      next: (res) => {
        if (res.ok) {
          this.club = res.dados;
          this.photoUrl = this.club.caminhoEscudo || 'assets/default-club.png';
          
          this.profileForm.patchValue({
            nome: this.club.nome,
            cidadeEstado: this.club.cidadeEstado,
            anoFundacao: this.club.anoFundacao,
            ligaCompeticao: this.club.ligaCompeticao,
            estadioPrincipal: this.club.estadioPrincipal,
            breveHistoria: this.club.breveHistoria
          });
        } else {
          this.error = res.mensagem;
        }
        this.loading = false;
      },
      error: () => {
        this.error = 'Erro ao carregar o perfil do clube.';
        this.loading = false;
      }
    });
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        this.photoUrl = reader.result as string;
      };
      reader.readAsDataURL(file);
    }
  }

  saveData(): void {
    if (this.profileForm.invalid) {
      this.profileForm.markAllAsTouched();
      return;
    }
    
    const command = {
      id: this.clubeId,
      caminhoEscudo: this.photoUrl,
      nome: this.profileForm.value.nome,
      anoFundacao: Number(this.profileForm.value.anoFundacao),
      cidadeEstado: this.profileForm.value.cidadeEstado,
      ligaCompeticao: this.profileForm.value.ligaCompeticao,
      estadioPrincipal: this.profileForm.value.estadioPrincipal,
      breveHistoria: this.profileForm.value.breveHistoria
    };

    this.profileService.updateProfile(this.clubeId, command).subscribe({
      next: (res) => {
        if (res.ok) {
          alert('Dados do clube atualizados com sucesso!');
        } else {
          alert('Erro: ' + res.mensagem);
        }
      },
      error: () => alert('Erro na comunicação com a API.')
    });
  }

  logout(): void {
    if (confirm('Tem certeza que deseja sair?')) {
      localStorage.removeItem('loggedUserId');
      localStorage.removeItem('userRole');
      this.router.navigate(['/login']);
    }
  }
}
