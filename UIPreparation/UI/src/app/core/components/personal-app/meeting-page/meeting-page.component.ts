import { Component, OnInit } from '@angular/core';
import { Meeting } from '../../admin/meeting/models/Meeting';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MeetingService } from '../../admin/meeting/services/Meeting.service';

@Component({
  selector: 'app-meeting-page',
  templateUrl: './meeting-page.component.html',
  styleUrls: ['./meeting-page.component.css']
})
export class MeetingPageComponent implements OnInit {

  meetingList: Meeting[];
	meeting: Meeting = new Meeting();

	meetingAddForm: FormGroup;
	capthaMessage: string = '';
	captchaInput: string = '';
	nameSurnameInput: string = '';
	emailInput: string = '';
	dateInput: string = '';
	timeInput: string = '';
  	phoneInput: string = '';
	messageInput: string = '';
	nameSurnameBool: boolean = false;
	emailBool: boolean = false;
	dateBool: boolean = false;
	timeBool: boolean = false;
  	phoneBool: boolean = false;
	messageBool: boolean = false;
	message: string = '';
	showMessage: boolean = false;
	showSuccessfullMessage: boolean = false;


	meetingId: number;

	constructor(private router: Router, private meetingService: MeetingService, private formBuilder: FormBuilder ) { }

	ngOnInit() {

		this.createMeetingAddForm();
		this.meetingService.generateCaptchaImage().subscribe(data => {
			this.capthaMessage = data.message;
			console.log(this.capthaMessage)
		});
		this.setMinDate();
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
		if(inputType=="date"){
			this.dateInput=event
		this.dateBool = false;
		}
		if(inputType=="time"){
			this.timeInput=event
		this.timeBool = false;
		}
   		 if(inputType=="phone"){
			this.phoneInput=event
		this.phoneBool = false;
		}
		if(inputType=="shortInfo"){
			this.messageInput=event
		this.messageBool = false;
		}
		
	}




	save() {
		console.log(this.meetingAddForm);
		if (this.nameSurnameInput == '') {
			this.nameSurnameBool = true;
		}
		if (this.emailInput == '') {
			this.emailBool = true;
		}
		if (this.dateInput == '') {
			this.dateBool = true;
		}
		if (this.timeInput == '') {
			this.timeBool = true;
		}
    	if (this.phoneInput == '') {
			this.phoneBool = true;
		}
		if (this.messageInput == '') {
			this.messageBool = true;
		}

		if (this.captchaInput === this.capthaMessage) {
			if (this.meetingAddForm.valid) {
				this.meeting = Object.assign({}, this.meetingAddForm.value)

				if (this.meeting.id == 0)
					this.addMeeting();
				this.nameSurnameBool= this.emailBool= this.dateBool = this.timeBool = this.phoneBool = this.messageBool = false;
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

	addMeeting() {

		this.meetingService.addMeeting(this.meeting).subscribe(data => {

			this.meeting = new Meeting();
			this.message = '';
			this.showMessage = false;
			this.clearFormGroup(this.meetingAddForm);

		})

	}




	createMeetingAddForm() {
		this.meetingAddForm = this.formBuilder.group({
			id: [0],
			nameSurname: ["", Validators.required],
      		phoneNumber: ["",Validators.required],
			email: ["", [Validators.required, Validators.email]],
			shortInfo: ["", Validators.required],
			date: ["", Validators.required],
			time: ["", Validators.required],
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

	minDate: string; // minimum tarih
    selectedDate: string; // seçilen tarih

    setMinDate() {
        const today = new Date();
        this.minDate = today.toISOString().split('T')[0]; // bugünün tarihi
        this.selectedDate = this.minDate; // varsayılan olarak bugünün tarihini seç
    }

    onDateChange(selectedDate: string) {
        const today = new Date();
        const selected = new Date(selectedDate);
        
        if (selected < today) {
            // Geçmiş tarih seçilmeye çalışıldığında seçimi iptal et
            this.selectedDate = this.minDate; // son geçerli tarihi koru
        } else {
            this.selectedDate = selectedDate; // geçerli tarihi güncelle
        }
    }
	  setFullHour(event: any): void {
		let timeValue = event.target.value;
		if (timeValue) {
		  let [hours, minutes] = timeValue.split(':');
		  if (minutes !== '00') {
			event.target.value = `${hours}:00`;
		  }
		}
		}


}
