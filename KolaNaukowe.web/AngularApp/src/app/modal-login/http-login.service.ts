import { Injectable } from '@angular/core';
import { Headers, Http, HttpModule } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { TokenParams } from './TokenParams';
import { LoginAccess } from './login-access';

import 'rxjs/add/observable/of';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';


@Injectable()
export class HttpLoginService {
  public accesToken: String = '';

  constructor(private http: Http) { }
  private TokenAPI = '';

  login(loginAccess: LoginAccess): Observable<TokenParams> {
    // const headersForTokenApi = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
    // const data = 'grant_type=password&username=' + userName + '&password=' + password;
    return this.http.post('http://localhost:50000/connect/token', loginAccess).map(res => res.json());
  }
}
