import { Component, OnInit } from '@angular/core';
import { Service } from '../../admin/service/models/Service';
import { ActivatedRoute } from '@angular/router';
import { ServiceService } from '../../admin/service/services/Service.service';

@Component({
  selector: 'app-service',
  templateUrl: './service.component.html',
  styleUrls: ['./service.component.css']
})
export class ServiceComponent implements OnInit {
  serviceId?:number;
  service?:Service;
  constructor(private activeRoute:ActivatedRoute,private serviceService:ServiceService) { }

  ngOnInit(): void {
  this.activeRoute.params.subscribe(params=>{
  this.serviceId=params['serviceId'];
  if(params['serviceId'])
    this.getServiceById(this.serviceId);
})
  }


getServiceById(id:number){
  this.serviceService.getServiceById(id).subscribe(data=>{
    this.service=data;
    console.log(this.service)
  })
}

}
