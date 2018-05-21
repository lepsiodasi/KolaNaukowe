import { ScienceClubDetailsService } from './science-club-details.service';
import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-science-club-details',
  templateUrl: './science-club-details.component.html',
  styleUrls: ['./science-club-details.component.css']
})
export class ScienceClubDetailsComponent implements OnInit {
  scienceClubId: number;
  data: any;
  constructor(route: ActivatedRoute, private httpService: ScienceClubDetailsService) {
    this.scienceClubId = route.snapshot.params['id'];
    this.getDetails();
  }

  ngOnInit() {
  }

  getDetails() {
    this.httpService.getDetails(this.scienceClubId).subscribe(data => {
       this.data = data;
       console.log(this.data);
   });
 }

}
