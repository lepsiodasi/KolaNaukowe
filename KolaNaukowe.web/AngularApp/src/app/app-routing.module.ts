import { ModalLoginComponent } from './modal-login/modal-login.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';


import { HomeComponent } from './home/home.component';
import { AdvancedSearchComponent } from './advanced-search/advanced-search.component';
import { AddScienceClubComponent } from './add-science-club/add-science-club.component';
const routes: Routes = [
  // { path: '', component: HomeComponent, pathMatch: 'full' },
  {
    path: 'advancedSearch',
    component: AdvancedSearchComponent,
    children: [
      {
        path: 'addScienceClub',
        component: AddScienceClubComponent,
        outlet: 'addScienceClubModal'
       }
    ]
  },
  {
    path: 'login',
    component: ModalLoginComponent,
    outlet: 'loginModal'
  },
  {
    path: 'addScienceClub',
    component: AddScienceClubComponent,
    outlet: 'addScienceClubModal'
  }
];
@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [
    RouterModule
  ],
  declarations: []
})

export class AppRoutingModule { }
