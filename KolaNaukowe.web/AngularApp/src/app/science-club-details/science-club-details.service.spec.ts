import { TestBed, inject } from '@angular/core/testing';

import { ScienceClubDetailsService } from './science-club-details.service';

describe('ScienceClubDetailsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ScienceClubDetailsService]
    });
  });

  it('should be created', inject([ScienceClubDetailsService], (service: ScienceClubDetailsService) => {
    expect(service).toBeTruthy();
  }));
});
