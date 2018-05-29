import { HttpLoginService } from './http-login.service';
import { Component, OnInit, Inject, Injectable } from '@angular/core';
import {MatDialog, MatDialogConfig, MatDialogRef} from '@angular/material';
import { Router } from '@angular/router';
import {MatTabsModule} from '@angular/material/tabs';

import { LoginAccess } from './login-access';

@Component({
  selector: 'app-modal-login',
  templateUrl: './modal-login.component.html',
  styleUrls: ['./modal-login.component.css'],
})
export class ModalLoginComponent implements OnInit {
  constructor(public dialog: MatDialog, private router: Router) {
    const dialogRef = this.dialog.open(ModalLoginDialogComponent , {
      width: '380px',
      height: '310px'
    });
    this.router.navigate([{ outlets: { loginModal: null }}]);
  }
  ngOnInit() {
  }
}

const hide = true;
@Component({
  selector: 'app-modal-login-dialog',
  templateUrl: './modal-login-dialog.component.html'
})
export class ModalLoginDialogComponent {
  hide = true;
  loginAccess: LoginAccess = {};

  userLogin: any;
  userPassword: any;
  constructor(
    public dialogRef: MatDialogRef<ModalLoginDialogComponent>,
    private httpService: HttpLoginService) {}

  closeClick(): void {
    this.dialogRef.close();
  }

  login() {
    this.userLogin = (<HTMLInputElement>document.getElementById('enteredEmail')).value;
    this.userPassword = (<HTMLInputElement>document.getElementById('enteredPassword')).value;

    // ----------------------------------------
    this.loginAccess.client_id = 'ResourceOwnerClient';
    this.loginAccess.grant_type = 'password';
    this.loginAccess.username = 'admin@test.com';
    this.loginAccess.password = 'Password1!';
    this.loginAccess.scope = 'StudentResearchGroupAPI';
    this.httpService.login(this.userLogin, this.userPassword).subscribe(data => {
      console.log('Token: ' + data.access_token);
      this.httpService.accesToken = data.access_token;
      console.log(data);
  });
  }
}
