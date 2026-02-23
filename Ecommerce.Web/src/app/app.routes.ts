import { Routes } from '@angular/router';

export const routes: Routes = [
    { path: 'clientes', loadComponent: () => import('./components/clientes/clientes.component').then(c => c.ClientesComponent) },
    { path: 'produtos', loadComponent: () => import('./components/produtos/produtos.component').then(c => c.ProdutosComponent) },
    { path: 'pedidos', loadComponent: () => import('./components/pedidos/pedidos.component').then(c => c.PedidosComponent) },
    { path: '', redirectTo: '/pedidos', pathMatch: 'full' }
];