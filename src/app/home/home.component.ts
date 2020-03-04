import {AfterViewInit, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {MatCalendar} from '@angular/material';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, AfterViewInit {

  @ViewChild('calendar', {static: false}) calendar: MatCalendar<Date>;
  meetings: string[] = [
    '27. Februar 2020 (06:30 - 07:00)',
    '27. Februar 2020 - 28. Februar 2020 (17:30 - 07:30)',
    '28. Februar 2020 - 29. Februar 2020 (22:00 - 12:30)',
    '1. März 2020 (10:00 - 22:00)' ];

  constructor() {
  }

  ngOnInit() {
  }

  ngAfterViewInit(): void {
    this.calendar.selectedChange.subscribe(s => {
      const elements = document.querySelectorAll('.mat-calendar-body-cell-content');
      const selectedDate = elements[s.getDate() - 1];
      this.addCorrespondingClass(selectedDate);
    });
  }

  private addCorrespondingClass(selectedDate: Element) {
    if (selectedDate.classList.contains('available')) {
      if (selectedDate.classList.contains('planned')) {
        selectedDate.classList.remove('available', 'planned');
        selectedDate.classList.add('not-available');
      } else {
        selectedDate.classList.add('planned');
      }
    } else {
      selectedDate.classList.remove('not-available');
      selectedDate.classList.add('available');
    }
  }
}

