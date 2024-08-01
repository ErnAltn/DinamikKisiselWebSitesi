import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Service } from '../models/Service';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class ServiceService {

  constructor(private httpClient: HttpClient) { }


  getServiceList(): Observable<Service[]> {

    return this.httpClient.get<Service[]>(environment.getApiUrl + '/services/getall')
  }

  getServiceById(id: number): Observable<Service> {
    return this.httpClient.get<Service>(environment.getApiUrl + '/services/getbyid?id='+id)
  }

  addService(service: Service): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/services/', service, { responseType: 'text' });
  }

  updateService(service: Service): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/services/', service, { responseType: 'text' });

  }

  deleteService(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/services/', { body: { id: id } });
  }


}