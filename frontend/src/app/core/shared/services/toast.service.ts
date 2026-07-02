import { Injectable, signal } from '@angular/core';

export interface ToastMessage {
  id: number;
  type: 'success' | 'error' | 'warning' | 'info';
  title: string;
  message: string;
}

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  toasts = signal<ToastMessage[]>([]);
  private counter = 0;

  show(type: 'success' | 'error' | 'warning' | 'info', title: string, message: string, duration = 4000) {
    const id = ++this.counter;
    this.toasts.update(current => [...current, { id, type, title, message }]);

    setTimeout(() => {
      this.remove(id);
    }, duration);
  }

  success(title: string, message: string) {
    this.show('success', title, message);
  }

  error(title: string, message: string) {
    this.show('error', title, message);
  }

  warning(title: string, message: string) {
    this.show('warning', title, message);
  }

  remove(id: number) {
    this.toasts.update(current => current.filter(t => t.id !== id));
  }
}
