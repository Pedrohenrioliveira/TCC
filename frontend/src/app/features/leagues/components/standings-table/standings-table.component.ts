import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimeClassificacaoDto } from '../../services/league.service';

@Component({
  selector: 'app-standings-table',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './standings-table.component.html',
  styleUrl: './standings-table.component.css'
})
export class StandingsTableComponent {
  @Input() teams: TimeClassificacaoDto[] = [];
}
