import { Component, OnInit, Inject, Injectable } from '@angular/core';
import {MatDialog, MatDialogConfig, MatDialogRef} from '@angular/material';
import { Router } from '@angular/router';
import {MatTabsModule} from '@angular/material/tabs';

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

// https://material.angular.io/components/input/overview E-mail

const hide = true;
@Component({
  selector: 'app-modal-login-dialog',
  templateUrl: './modal-login-dialog.component.html'
})
export class ModalLoginDialogComponent {
  hide = true;
  constructor(
    public dialogRef: MatDialogRef<ModalLoginDialogComponent>) {}

  closeClick(): void {
    this.dialogRef.close();
  }
}
