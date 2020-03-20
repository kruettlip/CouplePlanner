import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { Event } from '../models/event';
import { Absence } from '../models/absence';

@Component({
  selector: 'app-planning-modal',
  templateUrl: './planning-modal.component.html',
  styleUrls: ['./planning-modal.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class PlanningModalComponent implements OnInit {
  isEvent = true;
  event: Event;
  absence: Absence;

  constructor(
    public dialogRef: MatDialogRef<PlanningModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
  }

  ngOnInit(): void {
    this.event = new Event();
    this.absence = new Absence();
    this.event.startDate = this.data.date;
    this.event.endDate = this.data.date;
    this.absence.startDate = this.data.date;
    this.absence.endDate = this.data.date;
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  setType(isEvent: boolean) {
    this.isEvent = isEvent;
  }

  onSaveClick() {
    if (this.isEvent) {
      this.dialogRef.close(this.event);
    } else {
      this.dialogRef.close(this.absence);
    }
  }
}
