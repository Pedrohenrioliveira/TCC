import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClubTournamentDetailsComponent } from './club-tournament-details.component';

describe('ClubTournamentDetailsComponent', () => {
  let component: ClubTournamentDetailsComponent;
  let fixture: ComponentFixture<ClubTournamentDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClubTournamentDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClubTournamentDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
