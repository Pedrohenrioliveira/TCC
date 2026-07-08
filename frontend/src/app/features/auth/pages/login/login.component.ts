import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  private construtorDeFormulario = inject(FormBuilder);
  private roteador = inject(Router);
  private authService = inject(AuthService);

  public tipoLogin: 'jogador' | 'clube' = 'jogador';
  public mostrarSenha = false;
  public loading = false;
  public errorMessage: string | null = null;

  ngOnInit(): void {
    const savedPlayer = localStorage.getItem('savedPlayerCreds');
    if (savedPlayer) {
      try {
        const creds = JSON.parse(atob(savedPlayer));
        this.formularioLogin.patchValue({
          email: creds.email,
          senha: creds.senha,
          manterConectado: true
        });
      } catch (e) {}
    }

    const savedClub = localStorage.getItem('savedClubCreds');
    if (savedClub) {
      try {
        const creds = JSON.parse(atob(savedClub));
        this.formularioClube.patchValue({
          emailCorporativo: creds.email,
          senhaAcesso: creds.senha,
          manterConectado: true
        });
      } catch (e) {}
    }
  }

  public formularioLogin: FormGroup = this.construtorDeFormulario.group({
    email: ['', [Validators.required]],
    senha: ['', [Validators.required]],
    manterConectado: [false]
  });

  public formularioClube: FormGroup = this.construtorDeFormulario.group({
    emailCorporativo: ['', [Validators.required]],
    senhaAcesso: ['', [Validators.required]],
    manterConectado: [false]
  });

  public setTipoLogin(tipo: 'jogador' | 'clube'): void {
    this.tipoLogin = tipo;
    this.mostrarSenha = false;
    this.errorMessage = null;
  }

  public alternarVisualizacaoSenha(): void {
    this.mostrarSenha = !this.mostrarSenha;
  }

  public aoSubmeter(): void {
    if (this.formularioLogin.valid) {
      this.loading = true;
      this.errorMessage = null;
      
      const payload = {
        login: this.formularioLogin.value.email,
        senha: this.formularioLogin.value.senha,
        manterConectado: this.formularioLogin.value.manterConectado
      };

      this.authService.login(payload).subscribe({
        next: (res) => {
          this.loading = false;
          if (res.ok) {
            const isClube = res.dados.clubeId || res.dados.perfil?.toLowerCase() === 'clube';
            if (isClube) {
              this.errorMessage = 'Esta conta pertence a um clube. Por favor, mude para a aba "Sou Clube".';
              return;
            }
            
            if (this.formularioLogin.value.manterConectado) {
              const creds = {
                email: this.formularioLogin.value.email,
                senha: this.formularioLogin.value.senha
              };
              localStorage.setItem('savedPlayerCreds', btoa(JSON.stringify(creds)));
            } else {
              localStorage.removeItem('savedPlayerCreds');
            }

            this.authService.salvarSessao(res.dados);
            this.roteador.navigate(['/player/home']);
          } else {
            this.errorMessage = res.mensagem || 'Credenciais inválidas.';
          }
        },
        error: (err) => {
          this.loading = false;
          this.errorMessage = err.error?.mensagem || 'Erro ao conectar com o servidor.';
        }
      });
    } else {
      this.marcarCamposComoTocados(this.formularioLogin);
    }
  }

  public aoSubmeterClube(): void {
    if (this.formularioClube.valid) {
      this.loading = true;
      this.errorMessage = null;

      const payload = {
        login: this.formularioClube.value.emailCorporativo,
        senha: this.formularioClube.value.senhaAcesso,
        manterConectado: this.formularioClube.value.manterConectado
      };

      this.authService.login(payload).subscribe({
        next: (res) => {
          this.loading = false;
          if (res.ok) {
            const isJogador = res.dados.jogadorId && !res.dados.clubeId || res.dados.perfil?.toLowerCase() === 'jogador';
            if (isJogador) {
              this.errorMessage = 'Esta conta pertence a um jogador. Por favor, mude para a aba "Sou Jogador".';
              return;
            }

            if (this.formularioClube.value.manterConectado) {
              const creds = {
                email: this.formularioClube.value.emailCorporativo,
                senha: this.formularioClube.value.senhaAcesso
              };
              localStorage.setItem('savedClubCreds', btoa(JSON.stringify(creds)));
            } else {
              localStorage.removeItem('savedClubCreds');
            }

            this.authService.salvarSessao(res.dados);
            this.roteador.navigate(['/club/home']);
          } else {
            this.errorMessage = res.mensagem || 'Credenciais inválidas.';
          }
        },
        error: (err) => {
          this.loading = false;
          this.errorMessage = err.error?.mensagem || 'Erro ao conectar com o servidor.';
        }
      });
    } else {
      this.marcarCamposComoTocados(this.formularioClube);
    }
  }

  private marcarCamposComoTocados(formulario: FormGroup): void {
    Object.values(formulario.controls).forEach(controle => {
      controle.markAsTouched();
    });
  }
}
