import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PedidoService } from '../../services/pedido.service';
import { ClienteService } from '../../services/cliente.service';
import { ProdutoService } from '../../services/produto.service';
import { Pedido, Cliente, Produto, ItemPedido, StatusPedido } from '../../models/models';

@Component({
    selector: 'app-pedidos',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './pedidos.component.html'
})
export class PedidosComponent implements OnInit {
    pedidos: Pedido[] = [];
    clientes: Cliente[] = [];
    produtos: Produto[] = [];

    // Form states
    novoPedidoClienteId: number | null = null;
    itensAtuais: { produtoId: number, quantidade: number, produto: Produto }[] = [];
    produtoSelecionadoId: number | null = null;
    quantidadeSelecionada: number = 1;
    statusFiltro: string = ''; // '' means all

    // Enums for template
    statusPedidoEnum = StatusPedido;

    constructor(
        private pedidoService: PedidoService,
        private clienteService: ClienteService,
        private produtoService: ProdutoService
    ) { }

    ngOnInit() {
        this.carregarDadosBase();
        this.carregarPedidos();
    }

    carregarDadosBase() {
        this.clienteService.getClientes().subscribe(c => this.clientes = c);
        this.produtoService.getProdutos().subscribe(p => this.produtos = p);
    }

    carregarPedidos() {
        let status: StatusPedido | undefined = undefined;
        if (this.statusFiltro) {
            status = parseInt(this.statusFiltro) as StatusPedido;
        }
        this.pedidoService.getPedidos(status).subscribe(p => this.pedidos = p);
    }

    adicionarItem() {
        if (!this.produtoSelecionadoId || this.quantidadeSelecionada <= 0) return;

        const produtoBase = this.produtos.find(p => p.id == this.produtoSelecionadoId);
        if (!produtoBase) return;

        // Check if already in list
        const existente = this.itensAtuais.find(i => i.produtoId == this.produtoSelecionadoId);
        if (existente) {
            existente.quantidade += this.quantidadeSelecionada;
        } else {
            this.itensAtuais.push({
                produtoId: produtoBase.id,
                quantidade: this.quantidadeSelecionada,
                produto: produtoBase
            });
        }

        // Reset fields
        this.produtoSelecionadoId = null;
        this.quantidadeSelecionada = 1;
    }

    removerItem(produtoId: number) {
        this.itensAtuais = this.itensAtuais.filter(i => i.produtoId !== produtoId);
    }

    get totalNovoPedido(): number {
        return this.itensAtuais.reduce((total, item) => total + (item.quantidade * item.produto.preco), 0);
    }

    criarPedido() {
        if (!this.novoPedidoClienteId || this.itensAtuais.length === 0) return;

        const itens: ItemPedido[] = this.itensAtuais.map(i => ({
            produtoId: i.produtoId,
            quantidade: i.quantidade,
            precoUnitario: i.produto.preco
        }));

        const pedido: Partial<Pedido> = {
            clienteId: this.novoPedidoClienteId,
            itens: itens
        };

        this.pedidoService.createPedido(pedido).subscribe(novo => {
            this.carregarPedidos(); // reload
            // Clear form
            this.novoPedidoClienteId = null;
            this.itensAtuais = [];
        });
    }

    pagar(id: number) {
        this.pedidoService.pagarPedido(id).subscribe(() => this.carregarPedidos());
    }

    cancelar(id: number) {
        if (confirm('Tem certeza que deseja cancelar este pedido?')) {
            this.pedidoService.cancelarPedido(id).subscribe(() => this.carregarPedidos());
        }
    }

    getStatusName(status: number): string {
        switch (status) {
            case StatusPedido.Criado: return 'Criado';
            case StatusPedido.Pago: return 'Pago';
            case StatusPedido.Cancelado: return 'Cancelado';
            default: return 'Desconhecido';
        }
    }

    getClienteNome(id: number): string {
        const cliente = this.clientes.find(c => c.id === id);
        return cliente ? cliente.nome : `ID: ${id}`;
    }
}