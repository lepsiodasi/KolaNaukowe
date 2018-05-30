import { AdvancedSearchComponent } from './../advanced-search/advanced-search.component';
import { ScienceClubService, IScienceClub, ISubject } from './../advanced-search/science-club.service';

import { Component, OnInit, Inject } from '@angular/core';
import {MatDialog, MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { Router } from '@angular/router';
import {MatDatepickerInputEvent} from '@angular/material/datepicker';

@Component({
  selector: 'app-add-science-club',
  templateUrl: './add-science-club.component.html',
  styleUrls: ['./add-science-club.component.css']
})
export class AddScienceClubComponent implements OnInit {
  id = 0;
  constructor( public dialog: MatDialog, private router: Router, private searchComponent: AdvancedSearchComponent) {
  this.id = searchComponent.id;
  console.log('Passed id:' + this.id);
    const dialogRef = this.dialog.open(AddScienceClubDialogComponent , {
      width: '300px',
      height: '500px',
      data: { number: this.id }
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
export class AddScienceClubDialogComponent implements OnInit{
  selectedDepartment = 'option2';
  constructor(
  private httpService: ScienceClubService,
  public dialogRef: MatDialogRef<AddScienceClubDialogComponent>,
  @Inject(MAT_DIALOG_DATA) public data: any) {
  }

  events: string[] = [];
  event = '';
  scienceClub: IScienceClub = {};
  subject: ISubject = {};
  arrayOfSubjects: Array<ISubject> = [];
  public id: number;
  value ?: number;
  isNewScienceClub = false;

  ngOnInit() {
    this.id = this.data.number;
    if (this.id > 0) {
      this.getDetails(this.id);
    } else {
      this.isNewScienceClub = true;
    }
  }

  addEvent(type: string, event: MatDatepickerInputEvent<Date>) {
    this.events.push(`${type}: ${event.value}`);
    this.event = event.value.toString();
    console.log('Choosen date: ' + this.event);
    console.log(event.value);
  }

  closeClick(): void {
    this.dialogRef.close();
  }

  fillWithData() {
    (<HTMLInputElement>document.getElementById('enteredName')).value = this.scienceClub.name;
    (<HTMLInputElement>document.getElementById('datePicker')).value = this.scienceClub.date;
    // (<HTMLInputElement>document.getElementById('departments')).value = this.scienceClub.department;
    this.selectedDepartment =  this.scienceClub.department;
    (<HTMLInputElement>document.getElementById('attendant')).value = this.scienceClub.attendant;
    (<HTMLInputElement>document.getElementById('leader')).value = this.scienceClub.leader;
    (<HTMLInputElement>document.getElementById('description')).value = this.scienceClub.description;
    (<HTMLInputElement>document.getElementById('subject')).value = this.scienceClub.subjects[0].name;
  }

  addScienceClub() {
    this.scienceClub.id = this.value;
    this.scienceClub.name = (<HTMLInputElement>document.getElementById('enteredName')).value;
    this.scienceClub.date = this.event;
    this.scienceClub.department = this.selectedDepartment; // 'Chemiczny'
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
    this.scienceClub.name = (<HTMLInputElement>document.getElementById('enteredName')).value;
    this.scienceClub.date = this.event;
    this.scienceClub.department = 'Chemiczny';
    this.scienceClub.attendant = (<HTMLInputElement>document.getElementById('attendant')).value;
    this.scienceClub.leader = (<HTMLInputElement>document.getElementById('leader')).value;
    this.scienceClub.description = (<HTMLInputElement>document.getElementById('description')).value;
    this.subject.name = (<HTMLInputElement>document.getElementById('subject')).value;
    this.arrayOfSubjects.push(this.subject);
    this.scienceClub.subjects = this.arrayOfSubjects;
    // ----------------------------
    this.httpService.updateScienceClub(this.id, this.scienceClub).subscribe(data => {

      console.log(data);
    });
  }

  getDetails(id: number) {
    this.httpService.getDetails(id).subscribe(data => {
       this.scienceClub = data;
       console.log(data);
       this.fillWithData();
   });
 }
}
