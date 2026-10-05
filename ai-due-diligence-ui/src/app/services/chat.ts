import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  private apiUrl = 'https://localhost:7070/api/chat/ask';

  constructor(private http: HttpClient) {}

  askQuestion(question: string): Observable<any> {
    return this.http.post<any>(
      this.apiUrl,
      {
        question: question
      }
    );
  }
}