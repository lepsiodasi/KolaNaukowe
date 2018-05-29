import { TestBed, inject } from '@angular/core/testing';

import { HttpAddScienceClubService } from './http-add-science-club.service';

describe('HttpAddScienceClubService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpAddScienceClubService]
    });
  });

  it('should be created', inject([HttpAddScienceClubService], (service: HttpAddScienceClubService) => {
    expect(service).toBeTruthy();
  }));
});
