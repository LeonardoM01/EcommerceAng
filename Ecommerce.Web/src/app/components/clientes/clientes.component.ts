import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ClienteService } from '../../services/cliente.service';
import { Cliente } from '../../models/models';

@Component({
    selector: 'app-clientes',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './clientes.component.html'
})
export class ClientesComponent implements OnInit {
    clientes: Cliente[] = [];
    novoCliente: Partial<Cliente> = { nome: '', cpf: '' };

    constructor(private clienteService: ClienteService) { }

    ngOnInit() {
        this.carregarClientes();
    }

    carregarClientes() {
        this.clienteService.getClientes().subscribe(clientes => {
            this.clientes = clientes;
        });
    }

    salvarCliente() {
        if (!this.novoCliente.nome || !this.novoCliente.cpf) return;
        this.clienteService.createCliente(this.novoCliente).subscribe(cliente => {
            this.clientes.push(cliente);
            this.novoCliente = { nome: '', cpf: '' };
        });
    }
}