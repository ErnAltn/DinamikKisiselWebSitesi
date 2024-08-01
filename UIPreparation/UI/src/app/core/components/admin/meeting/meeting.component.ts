import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Meeting } from './models/Meeting';
import { MeetingService } from './services/Meeting.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-meeting',
	templateUrl: './meeting.component.html',
	styleUrls: ['./meeting.component.scss']
})
export class MeetingComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','createdDate','updatedDate','deletedDate','nameSurname','email','phoneNumber','date','time','shortInfo', 'update','delete'];

	meetingList:Meeting[];
	meeting:Meeting=new Meeting();

	meetingAddForm: FormGroup;


	meetingId:number;

	constructor(private meetingService:MeetingService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getMeetingList();
    }

	ngOnInit() {

		this.createMeetingAddForm();
	}


	getMeetingList() {
		this.meetingService.getMeetingList().subscribe(data => {
			this.meetingList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.meetingAddForm.valid) {
			this.meeting = Object.assign({}, this.meetingAddForm.value)

			if (this.meeting.id == 0)
				this.addMeeting();
			else
				this.updateMeeting();
		}

	}

	addMeeting(){

		this.meetingService.addMeeting(this.meeting).subscribe(data => {
			this.getMeetingList();
			this.meeting = new Meeting();
			jQuery('#meeting').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.meetingAddForm);

		})

	}

	updateMeeting(){

		this.meetingService.updateMeeting(this.meeting).subscribe(data => {

			var index=this.meetingList.findIndex(x=>x.id==this.meeting.id);
			this.meetingList[index]=this.meeting;
			this.dataSource = new MatTableDataSource(this.meetingList);
            this.configDataTable();
			this.meeting = new Meeting();
			jQuery('#meeting').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.meetingAddForm);

		})

	}

	createMeetingAddForm() {
		this.meetingAddForm = this.formBuilder.group({		
			id : [0],
nameSurname : ["", Validators.required],
email : ["", Validators.required],
phoneNumber : ["", Validators.required],
date : [null, Validators.required],
time : ["", Validators.required],
shortInfo : ["", Validators.required]
		})
	}

	deleteMeeting(meetingId:number){
		this.meetingService.deleteMeeting(meetingId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.meetingList=this.meetingList.filter(x=> x.id!=meetingId);
			this.dataSource = new MatTableDataSource(this.meetingList);
			this.configDataTable();
		})
	}

	getMeetingById(meetingId:number){
		this.clearFormGroup(this.meetingAddForm);
		this.meetingService.getMeetingById(meetingId).subscribe(data=>{
			this.meeting=data;
			this.meetingAddForm.patchValue(data);
		})
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

	checkClaim(claim:string):boolean{
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

  }
