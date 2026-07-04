import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-player-home-navbar',
  standalone: true,
  templateUrl: './player-home-navbar.component.html',
  styleUrl: './player-home-navbar.component.css'
})
export class PlayerHomeNavbarComponent {
  @Input() firstName: string = '';
}
