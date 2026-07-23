import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { ToDoItem } from '../models/todo-item.model';

@Injectable({ providedIn: 'root' })
export class TodoService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = '/api/ToDo';

  getAll(): Observable<ToDoItem[]> {
    return this.http.get<ToDoItem[]>(this.baseUrl);
  }

  create(title: string): Observable<ToDoItem> {
    return this.http.post<ToDoItem>(this.baseUrl, JSON.stringify(title), {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    });
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
