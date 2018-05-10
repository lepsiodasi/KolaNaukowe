import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';

export interface ScienceClub
{
    name: string
}

@Component({
  selector: 'app-add-science-club',
  templateUrl: './add-science-club.component.html',
  styleUrls: ['./add-science-club.component.css']
})
export class AddScienceClubComponent implements OnInit {

  constructor(
    public dialog: MatDialogRef<AddScienceClubComponent>
  ) { }

  ngOnInit() {
  }

  closeDialog() {
    this.dialog.close();
  }

  addScienceClub(scienceClub : ScienceClub)
  {
    this.dialog.close(scienceClub);
  }

}
