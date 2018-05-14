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
  constructor(private httpService: ScienceClubService) { }

  ngOnInit() {
    this.getTest();
  }

  getTest() {
     this.httpService.getTest().subscribe(test => {
      this.testText = test;
    });
    console.log(this.testText);
  }
}
