import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProdutoService } from '../../services/produto.service';
import { Produto } from '../../models/models';

@Component({
    selector: 'app-produtos',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './produtos.component.html',
    styleUrls: ['./produtos.component.css']
})
export class ProdutosComponent implements OnInit {
    produtos: Produto[] = [];
    novoProduto: Partial<Produto> = { nome: '', preco: 0 };

    constructor(private produtoService: ProdutoService) { }

    ngOnInit() {
        this.carregarProdutos();
    }

    carregarProdutos() {
        this.produtoService.getProdutos().subscribe(produtos => {
            this.produtos = produtos;
        });
    }

    salvarProduto() {
        if (!this.novoProduto.nome || this.novoProduto.preco! <= 0) return;
        this.produtoService.createProduto(this.novoProduto).subscribe(produto => {
            this.produtos.push(produto);
            this.novoProduto = { nome: '', preco: 0 };
        });
    }
}