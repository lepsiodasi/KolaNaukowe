import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { ScienceClubService } from './science-club.service';

@Component({
  selector: 'app-advanced-search',
  templateUrl: './advanced-search.component.html',
  styleUrls: ['./advanced-search.component.css']
})
export class AdvancedSearchComponent implements OnInit {
  testText: String = 'cos';
  data: any;
  constructor(private httpService: ScienceClubService) { }

  ngOnInit() {
    this.getTest();
    this.getAllScienceClubs();
  }

  getTest() {
     this.httpService.getTest().subscribe(test => {
        this.testText = test;
        console.log(this.testText);
    });
  }

  getAllScienceClubs() {
    this.httpService.getAllScienceClubs().subscribe(data => {
        this.data = data;
        console.log(this.data);
        console.log('One: ' + this.data[0]);
    });
  }

  deleteScienceClub(id: number) {
    this.deleteScienceClub(id);
  }

  passValues(id: number) {
  }

}
