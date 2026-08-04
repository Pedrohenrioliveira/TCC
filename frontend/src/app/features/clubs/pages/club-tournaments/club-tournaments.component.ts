import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CampeonatoApiService, CampeonatoDto, InscricaoCampeonatoDto } from '../../../../core/infrastructure/api/campeonato-api.service';

@Component({
  selector: 'app-club-tournaments',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './club-tournaments.component.html',
  styleUrl: './club-tournaments.component.css'
})
export class ClubTournamentsComponent implements OnInit {
  private api = inject(CampeonatoApiService);
  private fb = inject(FormBuilder);
  private router = inject(Router);
  
  isAdmin = false;
  
  // Fake AuthService just for now to get the current Club ID, or read from localStorage if needed
  // In a real app we would get the logged user's club id from auth state.
  private currentClubId = localStorage.getItem('usuarioId') || '11111111-1111-1111-1111-111111111111';

  campeonatos: CampeonatoDto[] = [];
  minhasInscricoes: InscricaoCampeonatoDto[] = [];
  
  showForm = false;
  editMode = false;
  campeonatoEditId: string | null = null;
  form: FormGroup;
  
  // Inscricao Modal
  showInscricaoModal = false;
  campeonatoAlvoInscricao: CampeonatoDto | null = null;
  aceitouRegulamento = false;

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
    this.isAdmin = this.router.url.includes('/admin');
    this.carregarCampeonatos();
    if (!this.isAdmin) {
      this.carregarMinhasInscricoes();
    }
  }

  carregarMinhasInscricoes() {
    this.api.obterMinhasInscricoes(this.currentClubId).subscribe({
      next: (res: any) => {
        if (res.ok) {
          this.minhasInscricoes = res.dados;
        }
      },
      error: (err: any) => console.error(err)
    });
  }

  getInscricaoStatus(campeonatoId: string): number | null {
    const inscricao = this.minhasInscricoes.find(i => i.campeonatoId === campeonatoId);
    return inscricao ? inscricao.status : null; // 1 = Pendente, 2 = Aprovada, 3 = Rejeitada
  }

  carregarCampeonatos() {
    this.api.listarCampeonatos().subscribe({
      next: (res: any) => {
        if (res.ok) {
          this.campeonatos = res.dados;
        }
      },
      error: (err: any) => console.error(err)
    });
  }

  toggleForm() {
    this.showForm = !this.showForm;
    if (!this.showForm) {
      this.form.reset();
      this.editMode = false;
      this.campeonatoEditId = null;
    }
  }

  prepararEdicao(camp: CampeonatoDto) {
    this.editMode = true;
    this.campeonatoEditId = camp.id;
    this.form.patchValue({
      nome: camp.nome,
      local: camp.local,
      dataInicio: camp.dataInicio ? new Date(camp.dataInicio).toISOString().split('T')[0] : '',
      dataFim: camp.dataFim ? new Date(camp.dataFim).toISOString().split('T')[0] : '',
      limiteEquipes: camp.limiteEquipes
    });
    this.showForm = true;
  }

  criarCampeonato() {
    if (this.form.invalid) return;

    if (this.editMode && this.campeonatoEditId) {
      this.api.editarCampeonato(this.campeonatoEditId, this.form.value).subscribe({
        next: (res: any) => {
          if (res.ok) {
            alert('Campeonato atualizado com sucesso!');
            this.toggleForm();
            this.carregarCampeonatos();
          } else {
            alert('Erro: ' + res.mensagem);
          }
        },
        error: (err: any) => console.error(err)
      });
    } else {
      this.api.criarCampeonato(this.form.value).subscribe({
        next: (res: any) => {
          if (res.ok) {
            alert('Campeonato criado com sucesso!');
            this.toggleForm();
            this.carregarCampeonatos();
          } else {
            alert('Erro: ' + res.mensagem);
          }
        },
        error: (err: any) => console.error(err)
      });
    }
  }

  excluirCampeonato(id: string) {
    if (confirm('Tem certeza que deseja excluir este campeonato? Isso apagará todos os dados vinculados!')) {
      this.api.excluirCampeonato(id).subscribe({
        next: (res: any) => {
          if (res.ok) {
            alert('Campeonato excluído com sucesso!');
            this.carregarCampeonatos();
          } else {
            alert('Erro: ' + res.mensagem);
          }
        },
        error: (err: any) => console.error(err)
      });
    }
  }

  abrirModalInscricao(camp: CampeonatoDto) {
    this.campeonatoAlvoInscricao = camp;
    this.aceitouRegulamento = false;
    this.showInscricaoModal = true;
  }

  fecharModalInscricao() {
    this.showInscricaoModal = false;
    this.campeonatoAlvoInscricao = null;
  }

  inscreverClube() {
    if (!this.campeonatoAlvoInscricao) return;
    
    if (!this.aceitouRegulamento) {
      alert('Você deve aceitar o regulamento para solicitar a inscrição.');
      return;
    }

    this.api.inscreverClube(this.campeonatoAlvoInscricao.id, this.currentClubId, this.aceitouRegulamento).subscribe({
      next: (res: any) => {
        if (res.ok) {
          alert('Solicitação de inscrição enviada com sucesso! Aguarde a aprovação.');
          this.fecharModalInscricao();
          this.carregarMinhasInscricoes(); // Atualiza a lista de inscrições
        } else {
          alert('Erro: ' + res.mensagem);
        }
      },
      error: (err: any) => {
        console.error(err);
        alert('Erro ao solicitar inscrição.');
      }
    });
  }
}
