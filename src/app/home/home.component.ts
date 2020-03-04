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

  constructor(private readonly dialog: MatDialog) {
  }

  @ViewChild('calendar', {static: false}) calendar: MatCalendar<Date>;
  events: Event[] = [
    {
      startDate: new Date(2020, 3, 27, 6, 30),
      endDate: new Date(2020, 3, 27, 7, 0),
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

  private static getDateString(data: Event) {
    let dateString = `${data.startDate.toLocaleDateString('de', {year: 'numeric', month: 'long', day: 'numeric'})}`;
    if (data.startDate.getDate() < data.endDate.getDate()) {
      dateString += ` - ${data.endDate.toLocaleDateString('de', {year: 'numeric', month: 'long', day: 'numeric'})}`;
    }
    const startTime = data.startDate.toLocaleTimeString('de', {hour: '2-digit', minute: '2-digit'});
    const endTime = data.endDate.toLocaleTimeString('de', {hour: '2-digit', minute: '2-digit'});
    dateString += ` (${startTime} - ${endTime})`;
    return dateString;
  }

  private static addCorrespondingClass(selectedDate: Element) {
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

  ngOnInit() {
    this.events.forEach(e => e.dateString = HomeComponent.getDateString(e));
    this.events.sort((a, b) => a.startDate > b.startDate ? 1 : a.startDate === b.startDate ? 0 : -1);
  }

  ngAfterViewInit(): void {
    this.updateCalendar();
    this.calendar.selectedChange.subscribe(s => {
      const elements = document.querySelectorAll('.mat-calendar-body-cell-content');
      const selectedDate = elements[s.getDate() - 1];
      HomeComponent.addCorrespondingClass(selectedDate);
    });
    this.calendar.monthSelected.subscribe(m => {
      this.updateCalendar();
    });
    this.calendar.stateChanges.subscribe(m => {
      this.updateCalendar();
    });
  }

  showMeeting(event: Event) {
    const dialogRef = this.dialog.open(EventModalComponent, {
      width: event.dateString.length > 50 ? '600px' : event.dateString.length > 35 ? '550px' :
        event.dateString.length > 30 ? '400px' : '300px',
      data: event
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  private updateCalendar() {
    setTimeout(() => {
        const dateElements = document.querySelectorAll('.mat-calendar-body-cell-content');
        const month = this.calendar.activeDate.getMonth();
        const year = this.calendar.activeDate.getFullYear();
        dateElements.forEach(d => {
          const calendarDate = new Date(year, month, (d.innerHTML as unknown) as number).toDateString();
          const isDatePlanned = this.events.filter(e => e.startDate.toDateString() === calendarDate ||
            e.endDate.toDateString() === calendarDate).length > 0;
          if (isDatePlanned) {
            dateElements[(d.innerHTML as unknown) as number - 1].classList.add('available', 'planned');
          }
        });
      }, 1);
  }
}
