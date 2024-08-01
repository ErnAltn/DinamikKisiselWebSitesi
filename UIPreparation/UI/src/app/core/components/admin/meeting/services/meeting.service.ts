import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Meeting } from '../models/Meeting';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class MeetingService {

  constructor(private httpClient: HttpClient) { }


  getMeetingList(): Observable<Meeting[]> {

    return this.httpClient.get<Meeting[]>(environment.getApiUrl + '/meetings/getall')
  }
  
  generateCaptchaImage(): Observable<any> {
    return this.httpClient.get<any>(environment.getApiUrl + '/meetings/generateCaptcha')
  }

  getMeetingById(id: number): Observable<Meeting> {
    return this.httpClient.get<Meeting>(environment.getApiUrl + '/meetings/getbyid?id='+id)
  }

  addMeeting(meeting: Meeting): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/meetings/', meeting, { responseType: 'text' });
  }

  updateMeeting(meeting: Meeting): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/meetings/', meeting, { responseType: 'text' });

  }

  deleteMeeting(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/meetings/', { body: { id: id } });
  }


}