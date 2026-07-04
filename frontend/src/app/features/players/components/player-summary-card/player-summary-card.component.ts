import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-player-summary-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './player-summary-card.component.html',
  styleUrl: './player-summary-card.component.css'
})
export class PlayerSummaryCardComponent {
  @Input() name: string = '';
  @Input() position: string = '';
  @Input() level: number = 0;
  @Input() photoUrl: string = '';
}
