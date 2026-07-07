import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-club-layout',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './club-layout.component.html',
  styleUrls: ['./club-layout.component.css']
})
export class ClubLayoutComponent implements OnInit {
  clubPhoto: string = 'assets/default-club.png';
  isProfilePage = false;

  constructor(private router: Router) {
    this.router.events.pipe(
      filter((event): event is NavigationEnd => event instanceof NavigationEnd)
    ).subscribe((event: NavigationEnd) => {
      this.isProfilePage = event.urlAfterRedirects.includes('/club/profile');
    });
  }

  ngOnInit(): void {
    // Buscar foto do clube logado se existir
    const savedPhoto = localStorage.getItem('loggedClubPhoto');
    if (savedPhoto) {
      this.clubPhoto = savedPhoto;
    }
  }
}
