import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddScienceClubComponent } from './add-science-club.component';

describe('AddScienceClubComponent', () => {
  let component: AddScienceClubComponent;
  let fixture: ComponentFixture<AddScienceClubComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddScienceClubComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddScienceClubComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
