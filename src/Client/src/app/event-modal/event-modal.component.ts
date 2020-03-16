import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {Event} from '../models/event';

@Component({
  selector: 'app-event-modal',
  templateUrl: './event-modal.component.html',
  styleUrls: ['./event-modal.component.scss']
})
export class EventModalComponent {

  dateString = '';

  constructor(
    public dialogRef: MatDialogRef<EventModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Event) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}
