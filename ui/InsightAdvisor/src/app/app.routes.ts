import { Routes } from '@angular/router';

export const routes: Routes = [

  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    loadChildren: () => import('./views/home/routes').then((m) => m.routes)
  },
  {
    path: 'advisors',
    loadChildren: () => import('./views/advisors/routes').then((m) => m.routes)
  },
];
