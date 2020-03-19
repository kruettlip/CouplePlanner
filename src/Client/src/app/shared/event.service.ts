import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Event } from '../models/event';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private readonly http: HttpClient) { }

  public getAll(): Observable<Event[]> {
    return this.http.get<Event[]>('/api/events');
  }
}
