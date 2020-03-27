import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatCalendar, MatDialog } from '@angular/material';
import { EventModalComponent } from '../event-modal/event-modal.component';
import { Event } from '../models/event';
import { PlanningModalComponent } from '../planning-modal/planning-modal.component';
import { Absence } from '../models/absence';
import { EventService } from '../shared/event.service';
import { AbsenceService } from '../shared/absence.service';
import { AbsenceModalComponent } from '../absence-modal/absence-modal.component';

const CLASS_AVAILABLE = 'available';
const CLASS_NOT_AVAILABLE = 'not-available';
const CLASS_PLANNED = 'planned';
const CLASS_DISABLED = 'disabled';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements AfterViewInit {

  @ViewChild('calendar') calendar: MatCalendar<Date>;
  events: Event[] = [];
  upcomingEvents: Event[] = [];
  absences: Absence[] = [];
  upcomingAbsences: Absence[] = [];
  upcomingCount = 5;

  constructor(private readonly dialog: MatDialog,
              private readonly eventService: EventService,
              private readonly absenceService: AbsenceService) {
  }

  private static getDateString(data: Event | Absence) {
    data.startDate = new Date(data.startDate);
    data.endDate = new Date(data.endDate);
    let dateString = `${data.startDate.toLocaleDateString('de', {year: 'numeric', month: 'long', day: 'numeric'})}`;
    const startDateWithoutTime = new Date(data.startDate.getFullYear(), data.startDate.getMonth(), data.startDate.getDate());
    const endDateWithoutTime = new Date(data.endDate.getFullYear(), data.endDate.getMonth(), data.endDate.getDate());
    if (startDateWithoutTime < endDateWithoutTime) {
      dateString += ` - ${data.endDate.toLocaleDateString('de', {year: 'numeric', month: 'long', day: 'numeric'})}`;
    }
    const startTime = data.startDate.toLocaleTimeString('de', {hour: '2-digit', minute: '2-digit'});
    const endTime = data.endDate.toLocaleTimeString('de', {hour: '2-digit', minute: '2-digit'});
    dateString += ` (${startTime} - ${endTime})`;
    return dateString;
  }

  ngAfterViewInit(): void {
    this.refresh();
    this.calendar.selectedChange.subscribe(s => {
      this.planEventOrAbsence(s);
    });
    this.calendar.monthSelected.subscribe(() => {
      this.updateCalendar();
    });
    this.calendar.stateChanges.subscribe(() => {
      this.updateCalendar();
    });
  }

  refresh() {
    this.refreshEvents();
    this.refreshAbsences();
  }

  refreshEvents() {
    this.eventService.getUpcoming(this.upcomingCount).subscribe(upcomingEvents => {
      this.upcomingEvents = upcomingEvents;
      this.upcomingEvents.forEach(e => {
        e.startDate = new Date(e.startDate.toLocaleString());
        e.endDate = new Date(e.endDate.toLocaleString());
      });
      this.upcomingEvents.forEach(e => e.dateString = HomeComponent.getDateString(e));
      this.upcomingEvents.sort((a, b) => a.startDate > b.startDate ? 1 : a.startDate === b.startDate ? 0 : -1);
    });

    this.eventService.getUpcoming(0).subscribe((events) => {
      this.events = events;
      this.events.forEach(e => {
        e.startDate = new Date(e.startDate.toLocaleString());
        e.endDate = new Date(e.endDate.toLocaleString());
      });
      this.updateCalendar();
    });
  }

  refreshAbsences() {
    this.absenceService.getUpcoming(this.upcomingCount).subscribe(upcomingAbsences => {
      this.upcomingAbsences = upcomingAbsences;
      this.upcomingAbsences.forEach(a => {
        a.startDate = new Date(a.startDate.toLocaleString());
        a.endDate = new Date(a.endDate.toLocaleString());
      });
      this.upcomingAbsences.forEach(a => a.dateString = HomeComponent.getDateString(a));
      this.upcomingAbsences.sort((a, b) => a.startDate > b.startDate ? 1 : a.startDate === b.startDate ? 0 : -1);
    });

    this.absenceService.getUpcoming(0).subscribe((absences) => {
      this.absences = absences;
      this.absences.forEach(a => {
        a.startDate = new Date(a.startDate.toLocaleString());
        a.endDate = new Date(a.endDate.toLocaleString());
      });
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
      if (result) {
        this.eventService.delete(result as string).subscribe(() => {
          this.refreshEvents();
        }, err => {
          console.log(err);
        });
      }
    });
  }

  showAbsence(absence: Absence) {
    const dialogRef = this.dialog.open(AbsenceModalComponent, {
      width: absence.dateString.length > 50 ? '600px' : absence.dateString.length > 35 ? '550px' :
        absence.dateString.length > 20 ? '400px' : '300px',
      data: absence
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.absenceService.delete(result as string).subscribe(() => {
          this.refreshAbsences();
        }, err => {
          console.log(err);
        });
      }
    });
  }

  private planEventOrAbsence(date: Date) {
    const dialogRef = this.dialog.open(PlanningModalComponent, {
      width: '400px',
      data: {date}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result instanceof Event) {
        this.addEvent(result as Event);
      } else if (result instanceof Absence) {
        this.addAbsence(result as Absence);
      }
    });
  }

  private updateCalendar() {
    setTimeout(() => {
      const dateElements = document.querySelectorAll('.mat-calendar-body-cell-content');
      const month = this.calendar.activeDate.getMonth();
      const year = this.calendar.activeDate.getFullYear();
      const currentDay = new Date().getDate();
      const currentMonth = new Date().getMonth();
      const currentYear = new Date().getFullYear();
      const currentDate = new Date(currentYear, currentMonth, currentDay);
      dateElements.forEach(d => {
        const calendarDate = new Date(year, month, (d.innerHTML as unknown) as number);
        const isDateUnavailable = this.absences.filter(a => (a.startDate <= calendarDate &&
          a.endDate >= calendarDate) || a.startDate.toDateString() === calendarDate.toDateString() ||
          a.endDate.toDateString() === calendarDate.toDateString()).length > 0;
        const isDatePlanned = this.events.filter(e => (e.startDate <= calendarDate &&
          e.endDate >= calendarDate) || e.startDate.toDateString() === calendarDate.toDateString() ||
          e.endDate.toDateString() === calendarDate.toDateString()).length > 0;
        const dateElement = dateElements[(d.innerHTML as unknown) as number - 1];
        dateElement.classList.remove(CLASS_AVAILABLE, CLASS_NOT_AVAILABLE, CLASS_PLANNED);
        if (calendarDate >= currentDate) {
          if (isDateUnavailable) {
            dateElement.classList.add(CLASS_NOT_AVAILABLE);
          } else {
            dateElement.classList.add(CLASS_AVAILABLE);
            if (isDatePlanned) {
              dateElement.classList.add(CLASS_PLANNED);
            }
          }
        } else {
          dateElement.classList.add(CLASS_DISABLED);
        }
      });
    }, 1);
  }

  private addEvent(event: Event) {
    this.eventService.add(event).subscribe(() => {
      this.refreshEvents();
    }, err => {
      console.log(err);
    });
  }

  private addAbsence(absence: Absence) {
    this.absenceService.add(absence).subscribe(() => {
      this.refreshAbsences();
    }, err => {
      console.log(err);
    });
  }
}
