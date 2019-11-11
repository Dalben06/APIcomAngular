import { Lote } from './Lote';
import { RedeSocial } from './RedeSocial';
import { Palestrante } from './Palestrante';

export class Evento {
        constructor() { }
        id: number ;
        dataEvento: Date;
        local: string;
        tema: string;
        qtdPessoas: number;
        telefone: string;
        email: string;
        lote: Lote[];
        redeSocial: RedeSocial[];
        palestrantesEvento: Palestrante[];
        imagemURL: string;
} 