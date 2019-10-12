import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_Services/Evento.service';
import { Evento } from '../_models/Evento';

import { BsModalRef, BsModalService } from 'ngx-bootstrap';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventos: Evento[];
  imagemLargura = 70;
  imagemMargem = 2;
  mostrarImagem = false;
  // tslint:disable-next-line: variable-name
  _filtroLista: string;
  eventosFiltrados: Evento[];

  modalRef: BsModalRef;






  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }



  // tslint:disable-next-line: variable-name
  constructor(private _eventoServive: EventoService,
              private modalService: BsModalService) { }

  ngOnInit() {
    this.getEventos();
  }

  openModal(template: TemplateRef<any>){
    this.modalRef = this.modalService.show(template);
  }




  alterarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  getEventos() {
    this._eventoServive.getEvento().subscribe(
      // tslint:disable-next-line: variable-name
      (_eventos: Evento[]) =>
      { this.eventos = _eventos; console.log(_eventos);this.eventosFiltrados = this.eventos; },
    error => { console.log(error); });
  }

  filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

}
