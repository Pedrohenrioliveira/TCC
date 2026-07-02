import { Component, EventEmitter, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastService } from '../../../../core/shared/services/toast.service';

@Component({
  selector: 'app-image-upload',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './image-upload.component.html',
  styleUrls: ['./image-upload.component.css']
})
export class ImageUploadComponent {
  private toastService = inject(ToastService);

  @Output() imageSelected = new EventEmitter<string>();

  imagePreview: string | null = null;
  fileName: string | null = null;

  onFileChange(event: Event) {
    const input = event.target as HTMLInputElement;
    if (!input.files || input.files.length === 0) return;

    const file = input.files[0];
    
    // Validação de formato
    const allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
    if (!allowedExtensions.exec(file.name)) {
      this.toastService.error('Arquivo Inválido', 'Selecione apenas imagens (.jpg, .jpeg, .png).');
      input.value = '';
      return;
    }

    // Validação de tamanho (2MB)
    const maxSize = 2 * 1024 * 1024;
    if (file.size > maxSize) {
      this.toastService.error('Arquivo muito grande', 'A foto deve ter no máximo 2MB.');
      input.value = '';
      return;
    }

    this.fileName = file.name;
    const reader = new FileReader();
    reader.onload = () => {
      const base64String = reader.result as string;
      this.imagePreview = base64String;
      this.imageSelected.emit(base64String);
    };
    reader.readAsDataURL(file);
  }

  triggerFileInput(fileInput: HTMLInputElement) {
    fileInput.click();
  }

  clearImage(event: Event) {
    event.stopPropagation();
    this.imagePreview = null;
    this.fileName = null;
    this.imageSelected.emit('');
  }
}
