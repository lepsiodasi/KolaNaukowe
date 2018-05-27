import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';
// import { Http, Response, Headers, URLSearchParams, RequestOptions } from '@angular/http';

import 'rxjs/add/operator/catch';
export interface IScienceClub {
  id ?: number;
  name ?: string;
  date ?: string;
  department ?: string;
  attendant ?: string;
  leader ?: string;
  description ?: string;
  subjects ?: Array<ISubject>;
}

export interface ISubject {
  id ?: number;
  name ?: string;
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
    return this.http.post<IScienceClub>('http://localhost:50000/api/StudentResearchGroup/Add', scienceClub);
  }

  updateScienceClub(id: number, scienceClub: IScienceClub): Observable<void> {
    return this.http.put<void>('http://localhost:50000/api/StudentResearchGroup/' + id, scienceClub);
  }

  deleteScienceClub(id: number) {
    // const cpHeaders = new Headers({ 'Content-Type': 'application/json' });
    // const options = new RequestOptions({ headers: cpHeaders });
    console.log('DELETE');
    return this.http.delete('http://localhost:50000/api/StudentResearchGroup/Delete/' + id)
    .subscribe(res => console.log(res));
  }

  private handleError (error: Response | any) {
    console.error(error.message || error);
    return Observable.throw(error.status);
  }
}
