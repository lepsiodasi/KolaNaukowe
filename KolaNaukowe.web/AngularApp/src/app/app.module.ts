import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';



import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AppRoutingModule } from './/app-routing.module';
import { AdvancedSearchComponent } from './advanced-search/advanced-search.component';
import { ModalLoginComponent } from './modal-login/modal-login.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ModalLoginDialogComponent } from './modal-login/modal-login.component';


import { MatFormFieldModule, MatInputModule, MatSelectModule } from '@angular/material';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AdvancedSearchComponent,
    ModalLoginComponent,
    ModalLoginDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatDialogModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [ModalLoginComponent, ModalLoginDialogComponent]
})
export class AppModule { }
