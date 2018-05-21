import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';

export interface IScienceClub {
  id: number;
  name: string;
  date: string;
  department: string;
  attendant: string;
  leader: string;
  description: string;
  subjects: Array<ISubject>;
}

export interface ISubject {
  id: number;
  name: string;
}


@Injectable()
export class ScienceClubService {
  accessToken: String = '';
  private URL = 'https://localhost:50000/api/StudentResearchGroup/';

  constructor(private http: HttpClient) { }
  getTest(): Observable<String> {
    return this.http.get<String>('http://localhost:50000/api/StudentResearchGroup/Test');
  }

  getAllScienceClubs(): Observable<Array<IScienceClub>> {
    return this.http.get<Array<IScienceClub>>('http://localhost:50000/api/StudentResearchGroup/WriteAll');
  }

  getDetails(id: number): Observable<IScienceClub> {
    return this.http.get<IScienceClub>('http://localhost:50000/api/StudentResearchGroup/');
  }

  insertScienceClub(scienceClub: IScienceClub): Observable<IScienceClub> {
    return this.http.post<IScienceClub>('http://localhost:50000/api/StudentResearchGroup/', scienceClub);
  }

  updateScienceClub(id: number, scienceClub: IScienceClub): Observable<void> {
    return this.http.put<void>('http://localhost:50000/api/StudentResearchGroup/' + id, scienceClub);
  }

  deleteScienceClub(id: number) {
    return this.http.delete('http://localhost:50000/api/StudentResearchGroup/' + id);
  }
}
