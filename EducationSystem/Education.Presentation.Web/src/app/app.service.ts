import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class AppService {
  constructor(private http: HttpClient) { }

  private hostUrl = "http://localhost:8081/api/SchoolService/Register";

  registerEmployee(formData: any) {
    return this.http.post(this.hostUrl, formData);
  }
}
