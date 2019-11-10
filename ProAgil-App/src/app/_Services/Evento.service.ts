import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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

postUpload(file: File){

  const fileToUpload = <File>file[0];
  const formData = new FormData();
  formData.append('file', fileToUpload, fileToUpload.name);
  console.log(fileToUpload);
  return this.http.post(`${this.baseURL}/upload`, formData);
}

}
