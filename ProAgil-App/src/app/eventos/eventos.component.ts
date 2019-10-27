import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_Services/Evento.service';
import { Evento } from '../_models/Evento';
import { Location } from '@angular/common';
import {  BsModalService, defineLocale, BsLocaleService, ptBrLocale } from 'ngx-bootstrap';
import { FormGroup,  Validators, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';


defineLocale('pt-br', ptBrLocale);
@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  titulo = 'Eventos';

  eventos: Evento[];
  evento: Evento;
  imagemLargura = 70;
  imagemMargem = 2;
  mostrarImagem = false;
  // tslint:disable-next-line: variable-name
  _filtroLista: string;
  eventosFiltrados: Evento[];

  registerForm: FormGroup;
  modoSalvar = '';

  bodyDeletarEvento = '';


  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }







  // tslint:disable-next-line: variable-name
  constructor(private _eventoServive: EventoService,
              private modalService: BsModalService,
              private fb: FormBuilder,
              private localService: BsLocaleService,
              private location: Location,
              private toastr: ToastrService
              ) { this.localService.use('pt-br'); }

  ngOnInit() {
    this.getEventos();
    this.validation();
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show(template);
  }

  salvarAlteracao(template: any) {
    if (this.registerForm.valid) {
      if (this.modoSalvar === 'post') {
        this.evento = Object.assign({}, this.registerForm.value);
        this._eventoServive.postEvento(this.evento).subscribe(
          (novoEvento: Evento) => {
            console.log(novoEvento);
            template.hide();
            this.toastr.success('Criado evento  com Sucesso');
            this.getEventos();
          },
          error => { 
            this.toastr.error(`Ocorreu um erro ao Inserir : ${error}`);
            console.log(error);
          
          }
        );
      }
      else {
        this.evento = Object.assign({ id: this.evento.id }, this.registerForm.value);
        this._eventoServive.EditEvento(this.evento).subscribe(
          () => {
            console.log('Editando');
            template.hide();
            this.toastr.success('Editado com Sucesso');
            this.getEventos();
          },
          error => { 
            this.toastr.error(`Ocorreu um erro ao Editar : ${error}`);
            console.log(error); 
          }
        );
      }
   }
  }

  validation() {
    this.registerForm = this.fb.group(
      {
        tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
        local: ['', Validators.required],
        dataEvento: ['', [Validators.required]],
        qtdPessoas: ['', [Validators.required, Validators.max(1200000)]],
        imagemURL: ['', Validators.nullValidator],
        telefone: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
      }
    );
  }

  alterarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  getEventos() {
    this._eventoServive.getEvento().subscribe(
      // tslint:disable-next-line: variable-name
      (_eventos: Evento[]) =>
       { this.eventos = _eventos; console.log(_eventos); this.eventosFiltrados = this.eventos; },
    error => { 
      this.toastr.error(`Ocorreu ao tentar carregar : ${error}`);
      console.log(error);
     });
  }

  filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }


  getEventoById(id: number): any {

    return this._eventoServive.getEventoById(id).subscribe(
      // tslint:disable-next-line: variable-name
      (_eventos: Evento) =>
      { this.evento = _eventos;
        console.log(_eventos);
      },
      error => { console.log(error); });
  }

  editarEvento(template: any, evento: Evento) {
    this.modoSalvar = 'put';
    this.openModal(template);
    this.evento = evento;
    this.registerForm.patchValue(this.evento);
  }

  novoEvento(template: any) {
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  excluirEvento(template: any, evento: Evento) {
    this.openModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.tema}`;

  }

  confirmeDelete(template: any) {
    this._eventoServive.deleteEvento(this.evento.id).subscribe(
      () => {
        template.hide();
        this.toastr.success('Deletado com Sucesso');
        this.getEventos();
      }, error => {
        this.toastr.error(`Ocorreu um erro ao Deletar : ${error}`);
        console.log(error);
      }
    );
  }
}
