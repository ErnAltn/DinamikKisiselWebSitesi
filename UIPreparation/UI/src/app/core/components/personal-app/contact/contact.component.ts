import { Component, OnInit } from '@angular/core';
import { Contact } from '../../admin/contact/models/Contact';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ContactService } from '../../admin/contact/services/Contact.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {

  contactList: Contact[];
	contact: Contact = new Contact();

	contactAddForm: FormGroup;
	capthaMessage: string = '';
	captchaInput: string = '';
	nameSurnameInput: string = '';
	emailInput: string = '';
    phoneInput: string = '';
	messageInput: string = '';
	nameSurnameBool: boolean = false;
	emailBool: boolean = false;
    phoneBool: boolean = false;
	messageBool: boolean = false;
	message: string = '';
	showMessage: boolean = false;
	showSuccessfullMessage: boolean = false;


	contactId: number;

	constructor(private router: Router, private contactService: ContactService, private formBuilder: FormBuilder) { }

	ngOnInit() {

		this.createContactAddForm();
		this.contactService.generateCaptchaImage().subscribe(data => {
			this.capthaMessage = data.message;
			console.log(this.capthaMessage)
		})
	}


	changeFunction(event:string,inputType){
		if(inputType=="nameSurname"){
		this.nameSurnameInput=event
		this.nameSurnameBool = false;
		}
		if(inputType=="email"){
			this.emailInput=event
		this.emailBool = false;
		}
   		 if(inputType=="phone"){
			this.phoneInput=event
		this.phoneBool = false;
		}
		if(inputType=="message"){
			this.messageInput=event
		this.messageBool = false;
		}
		
	}




	save() {
		console.log(this.contactAddForm);
		if (this.nameSurnameInput == '') {
			this.nameSurnameBool = true;
		}
		if (this.emailInput == '') {
			this.emailBool = true;
		}
    	if (this.phoneInput == '') {
			this.phoneBool = true;
		}
		if (this.messageInput == '') {
			this.messageBool = true;
		}

		if (this.captchaInput === this.capthaMessage) {
			if (this.contactAddForm.valid) {
				this.contact = Object.assign({}, this.contactAddForm.value)

				if (this.contact.id == 0)
					this.addContact();
				this.nameSurnameBool= this.emailBool= this.phoneBool = this.messageBool = false;
				this.showSuccessfullMessage = true;
				setTimeout(() => {
					this.router.navigate(['/home']);
				}, 5000);
			}
		} else {
			if (this.captchaInput == '')
				this.message = 'Lütfen doğrulama kodunu giriniz'
			else
				this.message = 'Kod yanlış, tekrar deneyin';
		}
		this.showMessage = true;



	}

	addContact() {

		this.contactService.addContact(this.contact).subscribe(data => {

			this.contact = new Contact();
			this.message = '';
			this.showMessage = false;
			this.clearFormGroup(this.contactAddForm);

		})

	}




	createContactAddForm() {
		this.contactAddForm = this.formBuilder.group({
			id: [0],
			nameSurname: ["", Validators.required],
      		phoneNumber: ["",Validators.required],
			email: ["", [Validators.required, Validators.email]],
			message: ["", Validators.required],
			captchaInput: ["", Validators.required],
		});
	}






	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'id')
				group.get(key).setValue(0);
		});
	}


}
