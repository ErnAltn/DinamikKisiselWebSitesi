import { Component, OnInit } from '@angular/core';
import { HomePage } from '../../admin/homePage/models/HomePage';
import { HomePageService } from '../../admin/homePage/services/HomePage.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
homepageList:HomePage[]=[];
  constructor(private homepageService:HomePageService) { }

  ngOnInit(): void {
    this.homepageService.getHomePageList().subscribe(data=>{
      this.homepageList=data;
      console.log(this.homepageList)
    })
  }

}
