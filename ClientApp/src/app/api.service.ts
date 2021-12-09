import {Observable} from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private httpClient: HttpClient) { }

  getPropertiesData(): Observable<any> {
  const routePath = 'http://localhost:60417/api/Property/getproperties';
  return this.httpClient.get(routePath);    
  }

  savePropertyData(payload?:any): Observable<any> {
    const routePath = 'http://localhost:60417/api/Property/saveproperty';
    return this.httpClient.post<any>(routePath, payload);    
    }
}
