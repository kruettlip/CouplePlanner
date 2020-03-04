import {AfterViewInit, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {MatCalendar, MatDialog} from '@angular/material';
import {EventModalComponent} from '../event-modal/event-modal.component';
import {Event} from '../models/event';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, AfterViewInit {

  @ViewChild('calendar', {static: false}) calendar: MatCalendar<Date>;
  events: Event[] = [
    {
      startDate: new Date(2020, 2, 27, 6, 30),
      endDate: new Date(2020, 2, 27, 7, 0),
      location: 'Meli',
      travel: 'Zug',
      dateString: ''
    },
    {
      startDate: new Date(2020, 2, 27, 17, 30),
      endDate: new Date(2020, 2, 28, 7, 30),
      location: 'Phippu',
      travel: 'Auto',
      dateString: ''
    },
    {
      startDate: new Date(2020, 2, 28, 22, 0),
      endDate: new Date(2020, 2, 29, 12, 30),
      location: 'Phippu',
      travel: 'Zug',
      dateString: ''
    },
    {
      startDate: new Date(2020, 3, 1, 10, 0),
      endDate: new Date(2020, 3, 1, 22, 0),
      location: 'Meli',
      travel: 'Auto',
      dateString: ''
    }
    ];

  constructor(private readonly dialog: MatDialog) {
  }

  ngOnInit() {
    this.events.forEach(e => e.dateString = this.getDateString(e));
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

  showMeeting(event: Event) {
    const dialogRef = this.dialog.open(EventModalComponent, {
      width: '450px',
      data: event
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  private getDateString(data: Event) {
    let dateString = `${data.startDate.toLocaleDateString('de')}`;
    if (data.startDate.getDay() === data.endDate.getDay()) {
      dateString += ` - ${data.startDate.toLocaleDateString('de')}`;
    }
    const startTime = data.startDate.toLocaleTimeString('de', {hour: '2-digit', minute: '2-digit'});
    const endTime = data.endDate.toLocaleTimeString('de', {hour: '2-digit', minute: '2-digit'});
    dateString += ` (${startTime} - ${endTime})`;
    return dateString;
  }
}
