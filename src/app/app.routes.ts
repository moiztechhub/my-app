import { Routes } from '@angular/router';
import { Home } from './home/home';
import { Employees } from './employess/employess';
import { NotFound } from './not-found/not-found';


export const routes: Routes = [
  {path: '', component: Home},
  {path: 'employee', component: Employees},
  {path: '**', component: NotFound}
];
