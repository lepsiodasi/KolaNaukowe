import { TestBed, inject } from '@angular/core/testing';

import { ScienceClubService } from './science-club.service';

describe('ScienceClubService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ScienceClubService]
    });
  });

  it('should be created', inject([ScienceClubService], (service: ScienceClubService) => {
    expect(service).toBeTruthy();
  }));
});
