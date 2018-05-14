import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { ScienceClubService } from './advanced-search/science-club.service';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AppRoutingModule } from './/app-routing.module';
import { AdvancedSearchComponent } from './advanced-search/advanced-search.component';
import { ModalLoginComponent } from './modal-login/modal-login.component';
import { MatDialogModule } from '@angular/material';
import { MatDialogRef, MatDialog } from '@angular/material/dialog';
import { ModalLoginDialogComponent } from './modal-login/modal-login.component';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';

import { MatFormFieldModule, MatInputModule, MatSelectModule } from '@angular/material';
import { AddScienceClubComponent, AddScienceClubDialogComponent } from './add-science-club/add-science-club.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AdvancedSearchComponent,
    ModalLoginComponent,
    ModalLoginDialogComponent,
    AddScienceClubComponent,
    AddScienceClubDialogComponent
  ],
  exports: [
    MatIconModule
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatDialogModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatIconModule,
    MatTabsModule,
    HttpClientModule
  ],
  providers: [
    ScienceClubService,
  {
    provide: MatDialogRef,
    useValue: {
    close: (dialogResult: any) => { }}
  }],
  bootstrap: [AppComponent],
  entryComponents: [ModalLoginComponent, ModalLoginDialogComponent,
    AddScienceClubComponent, AddScienceClubDialogComponent]
})
export class AppModule { }
