import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactsListComponent } from './components/contacts-list/contacts-list.component';
import { ContactDetailsComponent } from './components/contact-details/contact-details.component';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [  
  { path: 'home', component: HomeComponent },
  { path: 'contacts', component: ContactsListComponent },
  { path: 'contacts/:id', component: ContactDetailsComponent },
  { path: 'add', component: ContactDetailsComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
