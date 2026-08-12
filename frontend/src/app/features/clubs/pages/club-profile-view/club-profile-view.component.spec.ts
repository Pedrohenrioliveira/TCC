import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClubProfileViewComponent } from './club-profile-view.component';

describe('ClubProfileViewComponent', () => {
  let component: ClubProfileViewComponent;
  let fixture: ComponentFixture<ClubProfileViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClubProfileViewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClubProfileViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
