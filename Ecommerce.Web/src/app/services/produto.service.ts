import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Produto } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class ProdutoService {
    private apiUrl = 'https://localhost:7150/api/produtos';

    constructor(private http: HttpClient) { }

    getProdutos(): Observable<Produto[]> {
        return this.http.get<Produto[]>(this.apiUrl);
    }

    getProduto(id: number): Observable<Produto> {
        return this.http.get<Produto>(`${this.apiUrl}/${id}`);
    }

    createProduto(produto: Partial<Produto>): Observable<Produto> {
        return this.http.post<Produto>(this.apiUrl, produto);
    }
}