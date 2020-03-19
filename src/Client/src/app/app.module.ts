import {BrowserModule} from '@angular/platform-browser';
import {LOCALE_ID, NgModule} from '@angular/core';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {HomeComponent} from './home/home.component';
import {
  MAT_DATE_LOCALE,
  MatButtonModule,
  MatButtonToggleModule,
  MatCardModule,
  MatDatepickerModule,
  MatDialogModule,
  MatFormFieldModule,
  MatIconModule,
  MatInputModule,
  MatNativeDateModule,
  MatToolbarModule
} from '@angular/material';
import {FlexLayoutModule} from '@angular/flex-layout';
import {EventModalComponent} from './event-modal/event-modal.component';
import {PlanningModalComponent} from './planning-modal/planning-modal.component';
import {FormsModule} from '@angular/forms';
import {MatDatetimepickerModule, MatNativeDatetimeModule} from '@mat-datetimepicker/core';
import {MatMomentDateModule} from '@angular/material-moment-adapter';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    EventModalComponent,
    PlanningModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatNativeDatetimeModule,
    MatDatetimepickerModule,
    FlexLayoutModule,
    MatButtonModule,
    MatDialogModule,
    MatButtonToggleModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule
  ],
  entryComponents: [EventModalComponent, PlanningModalComponent],
  providers: [
    {provide: MAT_DATE_LOCALE, useValue: 'de-CH'},
    {provide: LOCALE_ID, useValue: 'de-CH'}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
