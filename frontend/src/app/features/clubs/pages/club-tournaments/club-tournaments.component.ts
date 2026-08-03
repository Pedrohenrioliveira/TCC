import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CampeonatoApiService, CampeonatoDto } from '../../../core/infrastructure/api/campeonato-api.service';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-club-tournaments',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './club-tournaments.component.html',
  styleUrl: './club-tournaments.component.css'
})
export class ClubTournamentsComponent implements OnInit {
  private api = inject(CampeonatoApiService);
  private fb = inject(FormBuilder);
  
  // Fake AuthService just for now to get the current Club ID, or read from localStorage if needed
  // In a real app we would get the logged user's club id from auth state.
  private currentClubId = localStorage.getItem('usuarioId') || '11111111-1111-1111-1111-111111111111';

  campeonatos: CampeonatoDto[] = [];
  showForm = false;
  form: FormGroup;

  constructor() {
    this.form = this.fb.group({
      nome: ['', Validators.required],
      local: ['', Validators.required],
      dataInicio: ['', Validators.required],
      dataFim: ['', Validators.required],
      limiteEquipes: [16, [Validators.required, Validators.min(2)]],
      caminhoLogo: ['assets/campeonato1.jpg']
    });
  }

  ngOnInit() {
    this.carregarCampeonatos();
  }

  carregarCampeonatos() {
    this.api.listarCampeonatos().subscribe({
      next: (res) => {
        if (res.deuCerto) {
          this.campeonatos = res.dados;
        }
      },
      error: (err) => console.error(err)
    });
  }

  toggleForm() {
    this.showForm = !this.showForm;
    if (!this.showForm) {
      this.form.reset({ limiteEquipes: 16, caminhoLogo: 'assets/campeonato1.jpg' });
    }
  }

  criarCampeonato() {
    if (this.form.invalid) return;

    this.api.criarCampeonato(this.form.value).subscribe({
      next: (res) => {
        if (res.deuCerto) {
          alert('Campeonato criado com sucesso!');
          this.toggleForm();
          this.carregarCampeonatos();
        } else {
          alert('Erro: ' + res.mensagem);
        }
      },
      error: (err) => console.error(err)
    });
  }

  inscreverClube(campeonatoId: string) {
    this.api.inscreverClube(campeonatoId, this.currentClubId).subscribe({
      next: (res) => {
        if (res.deuCerto) {
          alert('Clube inscrito com sucesso!');
        } else {
          alert('Erro: ' + res.mensagem);
        }
      },
      error: (err) => {
        console.error(err);
        alert('Erro ao inscrever clube.');
      }
    });
  }
}
