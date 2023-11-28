import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConvertService {
  private apiUrl = 'https://localhost:7063'; // Replace with your actual API endpoint

  constructor(private http: HttpClient) { }

  convertNumberToWords(number: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/convert`, { number });
  }
}