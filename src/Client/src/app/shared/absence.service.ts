import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Absence } from '../models/absence';

@Injectable({
  providedIn: 'root'
})
export class AbsenceService {

  constructor(private readonly http: HttpClient) {
  }

  getAll(): Observable<Absence[]> {
    return this.http.get<Absence[]>('/api/absences');
  }

  add(absence: Absence): Observable<string> {
    return this.http.post<string>('/api/absences', absence);
  }

  delete(id: string): Observable<any> {
    return this.http.delete(`/api/absences/${id}`);
  }
}
