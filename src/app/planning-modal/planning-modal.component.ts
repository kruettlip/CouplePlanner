import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';

@Component({
  selector: 'app-planning-modal',
  templateUrl: './planning-modal.component.html',
  styleUrls: ['./planning-modal.component.scss']
})
export class PlanningModalComponent {
  isEvent = true;

  constructor(
    public dialogRef: MatDialogRef<PlanningModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  setType(isEvent: boolean) {
    this.isEvent = isEvent;
  }
}
