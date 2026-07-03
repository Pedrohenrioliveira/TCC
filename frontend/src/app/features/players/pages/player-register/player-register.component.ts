import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PlayersFacade } from '../../services/players.facade';
import { ImageUploadComponent } from '../../components/image-upload/image-upload.component';
import { ToastComponent } from '../../../../core/shared/components/toast/toast.component';
import { Player } from '../../../../core/domain/entities/player.entity';

@Component({
  selector: 'app-player-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ImageUploadComponent, ToastComponent],
  templateUrl: './player-register.component.html',
  styleUrls: ['./player-register.component.css']
})
export class PlayerRegisterComponent {
  private fb = inject(FormBuilder);
  playersFacade = inject(PlayersFacade);

  playerForm: FormGroup = this.fb.group({
    nomeCompleto: ['', [Validators.required]],
    dataNascimento: ['', [Validators.required]],
    pePreferencial: [2, [Validators.required]], // 1 = Esquerdo, 2 = Direito, 3 = Ambos
    email: ['', [Validators.required, Validators.email]],
    numeroContato: [''],
    senha: ['', [Validators.required, Validators.minLength(6)]],
    confirmarSenha: ['', [Validators.required]],
    altura: [null as number | null, [Validators.required, Validators.min(1)]],
    peso: [null as number | null, [Validators.required, Validators.min(0.1)]],
    posicaoPrincipal: ['', [Validators.required]],
    posicaoSecundaria: [null],
    bioHistorico: [''],
    caminhoFoto: [''],
    clubeId: [null]
  }, { validators: this.passwordMatchValidator });

  // Lista de posições táticas baseadas no Enum do backend
  posicoes = [
    { value: 1, label: 'Goleiro' },
    { value: 2, label: 'Lateral Direito' },
    { value: 3, label: 'Zagueiro' },
    { value: 4, label: 'Lateral Esquerdo' },
    { value: 5, label: 'Volante' },
    { value: 6, label: 'Meio-Campo' },
    { value: 7, label: 'Ponta' },
    { value: 8, label: 'Centroavante (Atacante)' }
  ];

  passwordMatchValidator(g: FormGroup) {
    return g.get('senha')?.value === g.get('confirmarSenha')?.value
      ? null : { 'mismatch': true };
  }

  onImageSelected(base64Image: string) {
    this.playerForm.patchValue({ caminhoFoto: base64Image });
  }

  setPePreferencial(value: number) {
    this.playerForm.patchValue({ pePreferencial: value });
  }

  isFieldInvalid(field: string): boolean {
    const control = this.playerForm.get(field);
    return !!(control && control.invalid && (control.dirty || control.touched));
  }

  onSubmit() {
    if (this.playerForm.invalid) {
      this.markFormGroupTouched(this.playerForm);
      return;
    }

    const formValue = this.playerForm.value;
    
    // Mesclando o número de contato na Bio para não alterar o backend
    let bioFinal = formValue.bioHistorico || '';
    if (formValue.numeroContato) {
      bioFinal = `Contato: ${formValue.numeroContato}\n\n${bioFinal}`;
    }

    const player: Player = {
      ...formValue,
      bioHistorico: bioFinal,
      // Se não enviou foto, podemos passar uma string vazia ou um mock de avatar
      caminhoFoto: formValue.caminhoFoto || 'assets/default-avatar.png',
      // Garantir conversões de tipo corretas
      altura: Number(formValue.altura),
      peso: Number(formValue.peso),
      posicaoPrincipal: Number(formValue.posicaoPrincipal),
      posicaoSecundaria: formValue.posicaoSecundaria ? Number(formValue.posicaoSecundaria) : null
    };

    this.playersFacade.registerPlayer(player).subscribe({
      next: () => {
        this.resetForm();
      }
    });
  }

  private resetForm() {
    this.playerForm.reset({
      pePreferencial: 2,
      altura: null,
      peso: null,
      posicaoPrincipal: '',
      posicaoSecundaria: null,
      bioHistorico: '',
      caminhoFoto: '',
      clubeId: null
    });
    // Forçar a recriação do componente de upload de imagem limpando o estado (se necessário, ou tratamos na UI)
  }

  private markFormGroupTouched(formGroup: FormGroup) {
    Object.values(formGroup.controls).forEach(control => {
      control.markAsTouched();
      if ((control as any).controls) {
        this.markFormGroupTouched(control as FormGroup);
      }
    });
  }
}
