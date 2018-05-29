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
  name ?: string;
}

@Injectable()
export class ScienceClubDetailsService {

  constructor(private http: HttpClient) { }
  getDetails(id: number): Observable<IScienceClub> {
    return this.http.get<IScienceClub>('http://localhost:50000/api/StudentResearchGroup/Details/' + id);
  }
}
