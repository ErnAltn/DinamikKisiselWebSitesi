import { Component, OnInit } from '@angular/core';
import { About } from '../../admin/about/models/About';
import { AboutService } from '../../admin/about/services/About.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {
aboutList:About[]=[];
  constructor(private aboutService:AboutService) { }

  ngOnInit(): void {
    this.aboutService.getAboutList().subscribe(data=>{
      this.aboutList=data;
      console.log(this.aboutList)
    })
  }

}
