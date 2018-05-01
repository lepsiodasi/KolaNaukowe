import { Component, OnInit, Inject, Injectable } from '@angular/core';
import {MatDialog, MatDialogConfig, MatDialogRef} from '@angular/material';
import {MatTabsModule} from '@angular/material/tabs';

@Component({
  selector: 'app-modal-login',
  templateUrl: './modal-login.component.html',
  styleUrls: ['./modal-login.component.css'],
})
export class ModalLoginComponent implements OnInit {

  constructor(public dialog: MatDialog) {
    const dialogRef = this.dialog.open(ModalLoginDialogComponent , {
      width: '250px'
    });
  }

  openDialog() {
    const dialogRef = this.dialog.open(ModalLoginComponent, {
      height: '400px',
      width: '600px',
      hasBackdrop : false
    });
  }
  ngOnInit() {
  }
}


@Component({
  selector: 'app-modal-login-dialog',
  templateUrl: './modal-login-dialog.component.html'
})
export class ModalLoginDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<ModalLoginDialogComponent>) {}

  closeClick(): void {
    this.dialogRef.close();
  }
}
