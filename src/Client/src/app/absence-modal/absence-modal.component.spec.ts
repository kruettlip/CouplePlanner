import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MatIconModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

import { AbsenceModalComponent } from './absence-modal.component';

describe('AbsenceModalComponent', () => {
  let component: AbsenceModalComponent;
  let fixture: ComponentFixture<AbsenceModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AbsenceModalComponent],
      imports: [MatIconModule],
      providers: [
        {provide: MatDialogRef, useValue: {}},
        {provide: MAT_DIALOG_DATA, useValue: {}}
        ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AbsenceModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
