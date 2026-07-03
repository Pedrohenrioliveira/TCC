import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';

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

  public tipoLogin: 'jogador' | 'clube' = 'jogador';
  public mostrarSenha = false;

  // Formulário do Jogador
  public formularioLogin: FormGroup = this.construtorDeFormulario.group({
    email: ['', [Validators.required]],
    senha: ['', [Validators.required]],
    manterConectado: [false]
  });

  // Formulário do Clube
  public formularioClube: FormGroup = this.construtorDeFormulario.group({
    emailCorporativo: ['', [Validators.required]],
    senhaAcesso: ['', [Validators.required]],
    manterConectado: [false]
  });

  public setTipoLogin(tipo: 'jogador' | 'clube'): void {
    this.tipoLogin = tipo;
    this.mostrarSenha = false; // reseta a visualização de senha ao trocar
  }

  public alternarVisualizacaoSenha(): void {
    this.mostrarSenha = !this.mostrarSenha;
  }

  public aoSubmeter(): void {
    if (this.formularioLogin.valid) {
      const dadosLogin = {
        ...this.formularioLogin.value,
        tipo: 'jogador'
      };
      console.log('Login Jogador Submetido:', dadosLogin);
      alert('Login de Atleta realizado com sucesso! (Integração mockada)');
    } else {
      this.marcarCamposComoTocados(this.formularioLogin);
    }
  }

  public aoSubmeterClube(): void {
    if (this.formularioClube.valid) {
      const dadosLoginClube = {
        ...this.formularioClube.value,
        tipo: 'clube'
      };
      console.log('Login Clube Submetido:', dadosLoginClube);
      alert('Login de Clube realizado com sucesso! (Integração mockada)');
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
