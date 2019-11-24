import { Component, OnInit } from '@angular/core';
import { EventoService } from 'src/app/_Services/Evento.service';
import { BsLocaleService } from 'ngx-bootstrap';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Evento } from 'src/app/_models/Evento';
import { ActivatedRoute } from '@angular/router';

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
  file: File;

  get lotes(): FormArray{
    return <FormArray>this.registerForm.get('lotes');
  }
  get redesociais(): FormArray{
    return <FormArray>this.registerForm.get('redesociais');
  }

  constructor(
    private _eventoServive: EventoService,
    private fb: FormBuilder,
    private localService: BsLocaleService,
    private toastr: ToastrService,
    private router: ActivatedRoute
    ) { this.localService.use('pt-br'); }

    ngOnInit() {
      this.validation();
      this.carregarevento();
    }

    carregarevento(){

      const idEvento = +this.router.snapshot.paramMap.get('id');
      this._eventoServive.getEventoById(idEvento)
        .subscribe(
          (evento: Evento) => {
            console.log('evento observable: ', evento);
            this.evento = Object.assign({}, evento);
            this.imagemURL = `https://localhost:44376/Resources/Images/${ this.evento.imagemURL }`;
            this.evento.imagemURL = ``;
            this.registerForm.patchValue(this.evento);
            this.evento.lotes.forEach(lote =>
              {
                this.lotes.push(this.criaLote(lote));
              });
            this.evento.redesociais.forEach(redesociais =>
             {
              this.redesociais.push(this.criaRedeSociais(redesociais));
             });
          },
          error => {
            console.log('error ao tentar carregar: ', error);
          }
        );
    }

    validation() {
      this.registerForm = this.fb.group(
        {
          id: [],
          tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
          local: ['', Validators.required],
          dataEvento: ['', [Validators.required]],
          qtdPessoas: ['', [Validators.required, Validators.max(1200000)]],
          imagemURL: ['', Validators.nullValidator],
          telefone: ['', Validators.required],
          email: ['', [Validators.required, Validators.email]],
          lotes : this.fb.array([]) ,
          redesociais : this.fb.array([])
        }
      );
    }

    criaLote(lote: any): FormGroup {
      return this.fb.group({
        id: [lote.id],
        nome : [lote.nome , Validators.required],
        quantidade: [lote.quantidade, Validators.required],
        preco : [lote.preco , Validators.required],
        dataInicio: [lote.dataInicio, Validators.nullValidator],
        dataFim: [lote.dataFim, Validators.nullValidator],
      });
    }

    criaRedeSociais(redeSociais: any): FormGroup {
      return this.fb.group({
        id: [redeSociais.id],
        nome: [redeSociais.nome, Validators.required],
        url: [redeSociais.url, Validators.nullValidator],
      });
    }

    onFileChange(file: FileList) {
      const reader = new FileReader();
      reader.onload = (event: any) => this.imagemURL = event.target.result;
      this.file = event.target.files;
      reader.readAsDataURL(file[0]);
    }

    addLotes(){
      this.lotes.push(this.criaLote({id: 0}));
    }
    addRedesSocias(){
      this.redesociais.push(this.criaRedeSociais({id: 0}));
    }
    removeLote(id: number){
      this.lotes.removeAt(id);
    }
    removeRedesSocial(id: number){
      this.redesociais.removeAt(id);
    }

    salvarEvento(){

      this.evento = Object.assign({ id: this.evento.id }, this.registerForm.value);

      this.uploadImage();

      console.log(this.evento);
      this._eventoServive.EditEvento(this.evento).subscribe(
        () => {
          this.toastr.success('Editado com Sucesso');
        },
        error => { 
          this.toastr.error(`Ocorreu um erro ao Editar : ${error}`);
          console.log(error);
        }
      );
    }


    uploadImage(){
      if (this.registerForm.get('imagemURL').value !== '')
      {
        this._eventoServive.postUpload(this.file).subscribe();
        const nomeArquivo = this.evento.imagemURL.split('\\', 3);
        this.evento.imagemURL = nomeArquivo[2];
        console.log(this.evento);
      }

    }
}
