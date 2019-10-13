import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Evento } from '../_models/Evento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {
  baseURL = 'https://localhost:44376/api/evento';
constructor(private http: HttpClient) { }

getEvento(): Observable<Evento[]> {
  return this.http.get<Evento[]>(this.baseURL);
}

getEventoByTema(tema: string): Observable<Evento[]> {
  return this.http.get<Evento[]>(`${this.baseURL}/getByTema/${tema}`);
}

getEventoById(id: number): Observable<Evento> {
  return this.http.get<Evento>(`${this.baseURL}/${id}`);
}

postEvento(evento: Evento): Observable<Evento> {
  return this.http.post<Evento>(this.baseURL, evento);
}
deleteEvento(id: number): Observable<Evento> {
  return this.http.delete<Evento>(`${this.baseURL}/${id}`);
}

  EditEvento(model: Evento): Observable<Evento> {
    return this.http.put<Evento>(`${this.baseURL}/${model.id}`, model);
  }

}
