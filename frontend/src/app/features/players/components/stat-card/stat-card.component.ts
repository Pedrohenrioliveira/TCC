import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-stat-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './stat-card.component.html',
  styleUrl: './stat-card.component.css'
})
export class StatCardComponent {
  @Input() iconPath: string = '';
  @Input() title: string = '';
  @Input() value: string | number = '';
  @Input() variation: string | number = '';
  @Input() variationIsPositive: boolean = true;
}
