import { Component, OnInit } from '@angular/core';
import { EventoService } from 'src/app/_Services/Evento.service';
import { BsModalService, BsLocaleService } from 'ngx-bootstrap';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Evento } from 'src/app/_models/Evento';

@Component({
  selector: 'app-evento-edit',
  templateUrl: './eventoEdit.component.html',
  styleUrls: ['./eventoEdit.component.css']
})
export class EventoEditComponent implements OnInit {

  titulo = 'Evento Edit';
  evento: Evento = new Evento();
  imagemURL = 'assets/img/upload-1.png';
  registerForm: FormGroup;

  get lotes(): FormArray{
    return <FormArray>this.registerForm.get('lotes');
  }
  get redesociais(): FormArray{
    return <FormArray>this.registerForm.get('redesociais');
  }

  constructor(
    private _eventoServive: EventoService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private localService: BsLocaleService,
    private toastr: ToastrService
    ) { this.localService.use('pt-br'); }

    ngOnInit() {
      this.validation();
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
          lotes : this.fb.array([this.criaLote()]) ,
          redesociais : this.fb.array([this.criaRedeSociais()])
        }
      );
    }

    criaLote(): FormGroup {
      return this.fb.group({
        nome : ['', Validators.required],
        quantidade: ['', Validators.required],
        preco : ['', Validators.nullValidator],
        dataInicio: ['', Validators.nullValidator],
        dataFim: ['', Validators.nullValidator],
      });
    }

    criaRedeSociais(): FormGroup {
      return this.fb.group({
        nome: ['', Validators.required],
        url: ['', Validators.nullValidator],
      });
    }

    onFileChange(file: FileList) {
      const reader = new FileReader();
      reader.onload = (event: any) => this.imagemURL = event.target.result;
      reader.readAsDataURL(file[0]);
    }

    addLotes(){
      this.lotes.push(this.criaLote());
    }
    addRedesSocias(){
      this.redesociais.push(this.criaRedeSociais());
    }
    removeLote(id: number){
      this.lotes.removeAt(id);
    }
    removeRedesSocial(id: number){
      this.redesociais.removeAt(id);
    }
}
