import { Component, inject } from '@angular/core';
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
export class LoginComponent {
  private construtorDeFormulario = inject(FormBuilder);
  private roteador = inject(Router);
  private authService = inject(AuthService);

  public tipoLogin: 'jogador' | 'clube' = 'jogador';
  public mostrarSenha = false;
  public loading = false;
  public errorMessage: string | null = null;

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
            this.authService.salvarSessao(res.dados);
            if (res.dados.clubeId || res.dados.perfil?.toLowerCase() === 'clube') {
              this.roteador.navigate(['/club/home']);
            } else {
              this.roteador.navigate(['/player/home']);
            }
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
            this.authService.salvarSessao(res.dados);
            if (res.dados.jogadorId && !res.dados.clubeId) {
              this.roteador.navigate(['/player/home']);
            } else {
              this.roteador.navigate(['/club/home']);
            }
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
