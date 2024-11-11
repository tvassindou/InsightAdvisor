import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'list',
    loadComponent: () => import('./advisor-list/advisor-list.component').then(m => m.AdvisorListComponent),
  },

  {
    path: 'edit/:id',
    loadComponent: () => import('./advisor-edit/advisor-edit.component').then(m => m.AdvisorEditComponent),
  },
  {
    path: 'details/:id',
    loadComponent: () => import('./advisor-details/advisor-details.component').then(m => m.AdvisorDetailsComponent),
  }
];