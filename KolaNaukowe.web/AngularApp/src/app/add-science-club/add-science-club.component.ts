import { Component, OnInit } from '@angular/core';
import {MatDialog, MatDialogConfig, MatDialogRef} from '@angular/material';
import { Router } from '@angular/router';
@Component({
  selector: 'app-add-science-club',
  templateUrl: './add-science-club.component.html',
  styleUrls: ['./add-science-club.component.css']
})
export class AddScienceClubComponent implements OnInit {

  constructor( public dialog: MatDialog, private router: Router) {
    const dialogRef = this.dialog.open(AddScienceClubDialogComponent , {
      width: '300px',
      height: '400px'
    });
    this.router.navigate(['/advancedSearch']);
  }

  ngOnInit() {
  }
}

@Component({
  selector: 'app-add-science-club-dialog',
  templateUrl: './add-science-club-dialog.component.html'
})
export class AddScienceClubDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<AddScienceClubDialogComponent>) {}

  closeClick(): void {
    this.dialogRef.close();
  }
}
