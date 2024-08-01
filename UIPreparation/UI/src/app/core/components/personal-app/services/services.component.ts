import { Component, OnInit } from '@angular/core';
import { Service } from '../../admin/service/models/Service';
import { ServiceService } from '../../admin/service/services/Service.service';

@Component({
  selector: 'app-services',
  templateUrl: './services.component.html',
  styleUrls: ['./services.component.css']
})
export class ServicesComponent implements OnInit {
  serviceList:Service[]=[];
  constructor(private serviceService:ServiceService) { }

  ngOnInit(): void {
    this.serviceService.getServiceList().subscribe(data=>{
      this.serviceList=data;
      console.log(this.serviceList)
    })
  }
}
