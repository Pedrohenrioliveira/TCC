import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-player-home-navbar',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './player-home-navbar.component.html',
  styleUrl: './player-home-navbar.component.css'
})
export class PlayerHomeNavbarComponent {
  @Input() firstName: string = '';
}
