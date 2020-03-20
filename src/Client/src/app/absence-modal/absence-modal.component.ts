import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { Absence } from '../models/absence';

@Component({
  selector: 'app-absence-modal',
  templateUrl: './absence-modal.component.html',
  styleUrls: ['./absence-modal.component.scss']
})
export class AbsenceModalComponent {

  dateString = '';

  constructor(
    public dialogRef: MatDialogRef<AbsenceModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Absence) {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onDeleteClick() {
    this.dialogRef.close(this.data.id);
  }
}
