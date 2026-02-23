import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cliente } from '../models/models';

@Injectable({
    providedIn: 'root'
})
export class ClienteService {
    private apiUrl = 'https://localhost:7150/api/clientes';

    constructor(private http: HttpClient) { }

    getClientes(): Observable<Cliente[]> {
        return this.http.get<Cliente[]>(this.apiUrl);
    }

    getCliente(id: number): Observable<Cliente> {
        return this.http.get<Cliente>(`${this.apiUrl}/${id}`);
    }

    createCliente(cliente: Partial<Cliente>): Observable<Cliente> {
        return this.http.post<Cliente>(this.apiUrl, cliente);
    }
}