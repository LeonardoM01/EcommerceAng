export interface Usuario {
  id: number;
  nome: string;
}

export interface Cliente extends Usuario {
  cpf: string;
}

export interface Produto {
  id: number;
  nome: string;
  preco: number;
}

export interface ItemPedido {
  produtoId: number;
  quantidade: number;
  precoUnitario: number;
}

export enum StatusPedido {
  Criado = 0,
  Pago = 1,
  Cancelado = 2
}

export interface Pedido {
  id: number;
  clienteId: number;
  itens: ItemPedido[];
  dataPedido: string;
  status: StatusPedido;
  valorTotal: number;
}

export enum AcaoHistorico {
  Criacao = 0,
  Alteracao = 1,
  Remocao = 2
}

export interface Historico {
  id: number;
  entidade: string;
  entidadeId: number;
  acao: AcaoHistorico;
  dataAlteracao: string;
  detalhes: string;
}