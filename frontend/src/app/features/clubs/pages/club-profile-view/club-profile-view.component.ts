import { Component, OnInit } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ClubProfileService, ClubDetails } from '../../services/club-profile.service';
import { FeedApiService } from '../../../../core/infrastructure/api/feed-api.service';
import { PostagemDto } from '../../../../core/infrastructure/api/dtos/feed.dto';

@Component({
  selector: 'app-club-profile-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './club-profile-view.component.html',
  styleUrl: './club-profile-view.component.css'
})
export class ClubProfileViewComponent implements OnInit {
  club: ClubDetails | null = null;
  postagens: PostagemDto[] = [];
  loading = true;
  error = '';
  isOwner = false;
  perfilId = '';

  constructor(
    private route: ActivatedRoute,
    private clubService: ClubProfileService,
    private feedApi: FeedApiService,
    private location: Location
  ) {}

  ngOnInit(): void {
    let id = this.route.snapshot.paramMap.get('id');
    const loggedClubId = localStorage.getItem('loggedClubId');
    
    if (!id) {
      id = loggedClubId;
    }

    if (id) {
      this.perfilId = id;
      this.isOwner = loggedClubId === id;
      this.carregarClube(id);
    } else {
      this.error = 'ID do clube não fornecido.';
      this.loading = false;
    }
  }

  carregarClube(id: string): void {
    this.loading = true;
    this.clubService.getProfile(id).subscribe({
      next: (res) => {
        if (res.ok) {
          this.club = res.dados;
          this.carregarFeed(id);
        } else {
          this.error = res.mensagem;
          this.loading = false;
        }
      },
      error: (err) => {
        this.error = 'Não foi possível carregar o perfil do clube.';
        this.loading = false;
        console.error(err);
      }
    });
  }

  carregarFeed(perfilId: string): void {
    this.feedApi.obterPostagens(perfilId).subscribe({
      next: (res) => {
        if (res.ok) {
          this.postagens = res.dados;
        }
        this.loading = false;
      },
      error: (err) => {
        console.error('Erro ao carregar o feed', err);
        this.loading = false;
      }
    });
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      const descricao = prompt('Digite uma legenda (opcional):') || '';

      this.loading = true;
      this.feedApi.adicionarPostagem({
        perfilId: this.perfilId,
        foto: file,
        descricao: descricao
      }).subscribe({
        next: (res) => {
          if (res.ok) {
            this.carregarFeed(this.perfilId);
          }
        },
        error: (err) => {
          console.error('Erro ao postar foto', err);
          alert(`Erro ao realizar a postagem. Detalhes: ${err?.error?.mensagem || err?.message || 'Erro desconhecido'}`);
          this.loading = false;
        }
      });
    }
  }

  voltar(): void {
    this.location.back();
  }
}
