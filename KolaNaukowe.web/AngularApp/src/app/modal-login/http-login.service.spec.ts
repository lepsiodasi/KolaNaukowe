import { TestBed, inject } from '@angular/core/testing';

import { HttpLoginService } from './http-login.service';

describe('HttpLoginService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HttpLoginService]
    });
  });

  it('should be created', inject([HttpLoginService], (service: HttpLoginService) => {
    expect(service).toBeTruthy();
  }));
});
