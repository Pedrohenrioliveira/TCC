import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ImageUploadComponent } from '../../../players/components/image-upload/image-upload.component';

@Component({
  selector: 'app-club-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ImageUploadComponent],
  templateUrl: './club-register.component.html',
  styleUrls: ['./club-register.component.css']
})
export class ClubRegisterComponent {
  private construtorDeFormulario = inject(FormBuilder);

  public formularioCadastro: FormGroup = this.construtorDeFormulario.group({
    caminhoEscudo: [''],
    nome: ['', Validators.required],
    anoFundacao: ['', [Validators.required, Validators.pattern('^[0-9]{4}$')]],
    cidadeEstado: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    contato: ['', Validators.required],
    senha: ['', [Validators.required, Validators.minLength(8)]],
    confirmarSenha: ['', Validators.required],
    competicao: ['', Validators.required],
    estadio: [''],
    historia: ['', Validators.required]
  }, { validators: this.passwordMatchValidator });

  passwordMatchValidator(g: FormGroup) {
    return g.get('senha')?.value === g.get('confirmarSenha')?.value
      ? null : { 'mismatch': true };
  }

  onImageSelected(base64Image: string) {
    this.formularioCadastro.patchValue({ caminhoEscudo: base64Image });
  }

  isFieldInvalid(field: string): boolean {
    const control = this.formularioCadastro.get(field);
    return !!(control && control.invalid && (control.dirty || control.touched));
  }

  public aoSubmeter(): void {
    if (this.formularioCadastro.invalid) {
      this.marcarCamposComoTocados();
      return;
    }

    const dadosDoClube = this.formularioCadastro.value;
    console.log('Dados prontos para envio:', dadosDoClube);
    alert('Clube cadastrado com sucesso! (Integração com backend pendente)');
  }

  private marcarCamposComoTocados(): void {
    Object.values(this.formularioCadastro.controls).forEach(controle => {
      controle.markAsTouched();
    });
  }
}
