import {Component, OnInit} from '@angular/core';
//import {ApiService} from './api.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'Property App ';
  sampleContent = null;

  constructor() {}

  ngOnInit() {
    // this.api.getTestData().subscribe((data)=>{
    //   this.sampleContent = data;
    // });
  }

  // SaveRecord() {

  //   alert("Record saved successfully.!");

  // }

}


