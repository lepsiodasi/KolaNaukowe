import { ScienceClubService, IScienceClub, ISubject } from './../advanced-search/science-club.service';

import { Component, OnInit } from '@angular/core';
import {MatDialog, MatDialogConfig, MatDialogRef} from '@angular/material';
import { Router } from '@angular/router';
import {MatDatepickerInputEvent} from '@angular/material/datepicker';
@Component({
  selector: 'app-add-science-club',
  templateUrl: './add-science-club.component.html',
  styleUrls: ['./add-science-club.component.css']
})
export class AddScienceClubComponent implements OnInit {

  constructor( public dialog: MatDialog, private router: Router) {
    const dialogRef = this.dialog.open(AddScienceClubDialogComponent , {
      width: '300px',
      height: '500px'
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
  selectedDepartment = 'option2';
  constructor(
  private httpService: ScienceClubService,
  public dialogRef: MatDialogRef<AddScienceClubDialogComponent>) {}
  events: string[] = [];
  event = '';
  scienceClub: IScienceClub = {};
  subject: ISubject = {};
  arrayOfSubjects: Array<ISubject> = [];

  addEvent(type: string, event: MatDatepickerInputEvent<Date>) {
    this.events.push(`${type}: ${event.value}`);
    this.event = event.value.toString();
    console.log('Choosen date: ' + this.event);
    console.log(event.value);
  }

  closeClick(): void {
    this.dialogRef.close();
  }

  addScienceClub() {
    this.scienceClub.name = (<HTMLInputElement>document.getElementById('enteredName')).value;
    this.scienceClub.date = this.event;
    this.scienceClub.department = 'Chemiczny';
    this.scienceClub.attendant = (<HTMLInputElement>document.getElementById('attendant')).value;
    this.scienceClub.leader = (<HTMLInputElement>document.getElementById('leader')).value;
    this.scienceClub.description = (<HTMLInputElement>document.getElementById('description')).value;
    this.subject.name = (<HTMLInputElement>document.getElementById('subject')).value;
    this.arrayOfSubjects.push(this.subject);
    this.scienceClub.subjects = this.arrayOfSubjects;

    this.httpService.insertScienceClub(this.scienceClub).subscribe(data => {

      console.log(data);
  });
  }

  updateScienceClub() {

  }
}
