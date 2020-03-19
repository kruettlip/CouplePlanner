import {AfterViewInit, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {MatCalendar, MatDialog} from '@angular/material';
import {EventModalComponent} from '../event-modal/event-modal.component';
import {Event} from '../models/event';
import {PlanningModalComponent} from '../planning-modal/planning-modal.component';
import {Absence} from '../models/absence';
import { EventService } from '../shared/event.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, AfterViewInit {

  constructor(private readonly dialog: MatDialog,
              private readonly eventService: EventService) {
  }

  @ViewChild('calendar') calendar: MatCalendar<Date>;
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
  absences: Absence[] = [];

  private static getDateString(data: Event) {
    data.startDate = new Date(data.startDate);
    data.endDate = new Date(data.endDate);
    let dateString = `${data.startDate.toLocaleDateString('de', {year: 'numeric', month: 'long', day: 'numeric'})}`;
    if (data.startDate.getDate() < data.endDate.getDate()) {
      dateString += ` - ${data.endDate.toLocaleDateString('de', {year: 'numeric', month: 'long', day: 'numeric'})}`;
    }
    const startTime = data.startDate.toLocaleTimeString('de', {hour: '2-digit', minute: '2-digit'});
    const endTime = data.endDate.toLocaleTimeString('de', {hour: '2-digit', minute: '2-digit'});
    dateString += ` (${startTime} - ${endTime})`;
    return dateString;
  }

  private addCorrespondingClass(date: Date, selectedDateElement: Element) {
    const dialogRef = this.dialog.open(PlanningModalComponent, {
      width: '400px',
      data: {date}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result instanceof Event) {
        this.events.push(result as Event);
        this.refreshEvents();
      } else if (result instanceof Absence) {
        this.absences.push(result as Absence);
      }
    });
    if (selectedDateElement.classList.contains('available')) {
      if (selectedDateElement.classList.contains('planned')) {
        selectedDateElement.classList.remove('available', 'planned');
        selectedDateElement.classList.add('not-available');
      } else {
        selectedDateElement.classList.add('planned');
      }
    } else {
      selectedDateElement.classList.remove('not-available');
      selectedDateElement.classList.add('available');
    }
  }

  ngOnInit() {
    this.refreshEvents();
  }

  ngAfterViewInit(): void {
    this.updateCalendar();
    this.calendar.selectedChange.subscribe(s => {
      const elements = document.querySelectorAll('.mat-calendar-body-cell-content');
      const selectedDate = elements[s.getDate() - 1];
      this.addCorrespondingClass(s, selectedDate);
    });
    this.calendar.monthSelected.subscribe(m => {
      this.updateCalendar();
    });
    this.calendar.stateChanges.subscribe(m => {
      this.updateCalendar();
    });
  }

  refreshEvents() {
    this.eventService.getAll().subscribe((events) => {
      this.events = events;
      this.events.forEach(e => e.dateString = HomeComponent.getDateString(e));
      this.events.sort((a, b) => a.startDate > b.startDate ? 1 : a.startDate === b.startDate ? 0 : -1);
      this.updateCalendar();
    });
  }

  showMeeting(event: Event) {
    const dialogRef = this.dialog.open(EventModalComponent, {
      width: event.dateString.length > 50 ? '600px' : event.dateString.length > 35 ? '550px' :
        event.dateString.length > 20 ? '400px' : '300px',
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
          const dateElement = dateElements[(d.innerHTML as unknown) as number - 1];
          dateElement.classList.remove('available', 'not-available', 'planned');
          dateElement.classList.add('available');
          if (isDatePlanned) {
            dateElement.classList.add('planned');
          }
        });
      }, 1);
  }
}
