import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ScienceClubDetailsComponent } from './science-club-details.component';

describe('ScienceClubDetailsComponent', () => {
  let component: ScienceClubDetailsComponent;
  let fixture: ComponentFixture<ScienceClubDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ScienceClubDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScienceClubDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
