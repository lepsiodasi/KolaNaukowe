import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';

import 'rxjs/add/observable/of';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

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
export class HttpAddScienceClubService {

  constructor(private http: HttpClient) { }
  insertScienceClub(scienceClub: IScienceClub): Observable<IScienceClub> {
    return this.http.post<IScienceClub>('http://localhost:50000/api/StudentResearchGroup/Add', scienceClub);
  }
}
