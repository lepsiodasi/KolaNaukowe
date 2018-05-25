import { HttpLoginService } from './modal-login/http-login.service';
import { Component } from '@angular/core';
import { MatDialogModule } from '@angular/material/dialog';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  accesToken: String;
  tokenExist: boolean;
  userName: String = 'Witaj';
  constructor(private authToken:  HttpLoginService ) { }

  checkToken() {
    this.accesToken = this.authToken.accesToken;

    if (this.accesToken !== '') {
      this.tokenExist = true;
    }

    return this.tokenExist;
  }

  public logout = () => {
    this.authToken.accesToken = '';
  }

}



