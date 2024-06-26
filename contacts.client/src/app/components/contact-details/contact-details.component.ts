import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Contact } from '../../models/contact';
import { ContactService } from '../../services/contact.service';

@Component({
  selector: 'app-contact-details',
  templateUrl: './contact-details.component.html',
  styleUrl: './contact-details.component.css'
})
export class ContactDetailsComponent {
  submitted = false;  
  currentContact: Contact = {};  
    
  constructor(    
    private toastr: ToastrService,
    private contactService: ContactService,
    private route: ActivatedRoute,
    private router: Router) { }


  ngOnInit(): void {  
    if (this.route.snapshot.params["id"])
      this.getContact(this.route.snapshot.params["id"]);
  }

  onSubmit(form: NgForm): void {

    this.submitted = true;

    if (form.value.id) {
      this.updateContact(form);
    }
    else {
      this.saveContact(form);
    }
  }

  public cancel() {
    this.router.navigate(['/contacts']);
  }

  onReset(form: NgForm): void {
    this.submitted = false;
    form.form.reset();    
  }

  getContact(id: number): void {
    this.contactService.getById(id)
      .subscribe({
        next: (response) => {
          this.currentContact = response;          
        },
        error: (e) => this.toastr.error('An error occurred on get the contact.')
      });
  }
  updateContact(form: NgForm): void {
    this.contactService.update(form.value.id, form.form.value)
      .subscribe({
        next: (response) => {
          this.toastr.success('Updated successful');
          this.router.navigateByUrl('/contacts');
        },
        error: (e) => this.toastr.error('An error occurred on update the contact.')
      });
  }

  saveContact(form: NgForm): void {
    this.contactService.create(form.form.value)
      .subscribe({
        next: (response) => {          
          this.router.navigateByUrl('/contacts');
          this.toastr.success('Registration successful');
        },
        error: (e) => {
          if (e.status == 409) {
            this.toastr.error(e.error)
          }          
        }
      });
  }

  //deleteContact(): void {
  //  this.contactService.delete(this.currentContact.id)
  //      .subscribe({
  //        next: (response) => {
  //          console.log(response);            
  //        },
  //        error: (e) => console.error(e)
  //      });
  //}  
}
