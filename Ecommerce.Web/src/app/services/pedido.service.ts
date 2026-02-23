import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Pedido, StatusPedido } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class PedidoService {
    private apiUrl = 'https://localhost:7091/api/pedidos';

    constructor(private http: HttpClient) { }

    getPedidos(status?: StatusPedido): Observable<Pedido[]> {
        const url = status !== undefined ? ${this.apiUrl}?status=${status} : this.apiUrl;
        return this.http.get<Pedido[]>(url);
    }

    getPedido(id: number): Observable<Pedido> {
        return this.http.get<Pedido>(${this.apiUrl}/${id});
    }

    createPedido(pedido: Partial<Pedido>): Observable<Pedido> {
        return this.http.post<Pedido>(this.apiUrl, pedido);
    }

    pagarPedido(id: number): Observable<any> {
        return this.http.put(${this.apiUrl}/${id}/pagar, {});
    }

    cancelarPedido(id: number): Observable<any> {
        return this.http.put(${this.apiUrl}/${id}/cancelar, {});
    }
}