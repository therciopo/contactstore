import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Contact } from '../models/contact';

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  constructor(private http: HttpClient) { }

  getAll(params: any): Observable<any> {
    return this.http.get<Contact[]>(`/api/contacts`, {params});
  }

  getById(id: number): Observable<any> {
    return this.http.get<Contact>(`/api/contacts/${id}`);
  }

  update(id: number, contact: Contact): Observable<any> {
    return this.http.put(`/api/contacts/${id}`, contact);
  }

  create(contact: Contact): Observable<any> {
    return this.http.post(`/api/contacts`, contact);
  }
  delete(id: any): Observable<any> {
    return this.http.delete(`/api/contacts/${id}`);
  }
}
