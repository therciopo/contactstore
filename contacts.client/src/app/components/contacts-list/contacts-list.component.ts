import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Contact } from '../../models/contact';
import { ContactService } from '../../services/contact.service';

@Component({
  selector: 'app-contacts-list',
  templateUrl: './contacts-list.component.html',
  styleUrl: './contacts-list.component.css'  
})
export class ContactsListComponent implements OnInit {

  constructor(
    private toastr: ToastrService,
    private contactService: ContactService,
    private router: Router) { }
  
  contacts: Contact[] = [];
  searchTerm = '';
  pageSize = 10;
  page = 1;
  count = 0;

  pageSizes = [10, 20, 30];

  currentIndex = -1;  

  ngOnInit(): void {
    this.getContacts();
  }

  getContacts(): void {

    const params = this.getRequestParams(this.searchTerm, this.page, this.pageSize);

    this.contactService.getAll(params)
      .subscribe({
        next: (response) => {

          const { contacts, totalItems } = response;
          this.contacts = contacts;
          this.count = totalItems;          
        },
        error: (e) => this.toastr.error('Failed to get the contacts.')
      });
  }

  public addContact() {
    this.router.navigate(['/add']);
  }
  search(): void {
    this.getContacts();
  }


  deleteContact(contactId: number): void {
    // improve: ask for a confirmation before delete
    this.contactService.delete(contactId)
      .subscribe({
        next: (response) => {
          this.toastr.success('The contact has been deleted');
          this.getContacts();          
        },
        error: (e) => this.toastr.error('Failed to delete the contact.')
      });
  }

  handlePageChange(event: number): void {
    this.page = event;
    this.getContacts();
  }
  handlePageSizeChange(event: any): void {
    this.pageSize = event.target.value;
    this.page = 1;
    this.getContacts();
  }

  public editContact(contactId: number) {
    this.router.navigate(['/contacts/' + contactId]);
  }
  setActiveContact(contact: Contact, index: number): void {    
    this.currentIndex = index;
  }
  
  refreshList(): void {
    this.getContacts();    
    this.currentIndex = -1;
  }

  getRequestParams(searchTerm: string, page: number, pageSize: number): any {
    let params: any = {};

    if (searchTerm) {
      params[`searchTerm`] = searchTerm;
    }

    if (page) {
      params[`pageNum`] = page;
    }

    if (pageSize) {
      params[`pageSize`] = pageSize;
    }
    return params;
  }
}
