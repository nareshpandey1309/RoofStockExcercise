import { Component, OnInit } from '@angular/core';
import { Console } from 'console';
import {ApiService} from '../api.service';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.css']
})
export class PropertyListComponent implements OnInit {

  propertiesData = null;

  constructor(private api:ApiService) { }

  ngOnInit(): void {
    this.api.getPropertiesData().subscribe((data)=>{
      this.propertiesData = data;
    });
  }

  SaveRecord(Id:number, index:number) {
    var selectedProperty = this.propertiesData.find(x => x.id === Id);
    let payload = {};
    
    payload = {
         'Id': selectedProperty.id,
        'address': {
          'address1': selectedProperty.address.address1,
          'address2': selectedProperty.address.address2,
          'city': selectedProperty.address.city,
          'country': selectedProperty.address.country,
          'county': selectedProperty.address.county,
          'district': selectedProperty.address.district,
          'state': selectedProperty.address.state,
          'zip': selectedProperty.address.zip,
          'zipPlus4': selectedProperty.address.zipplus4
              },
          'physicalData': {
                'yearBuilt': selectedProperty.physicalData?.yearBuilt
            },
            'financialData': {
                'listPrice': selectedProperty.financialData?.listPrice,
                'monthlyRent': selectedProperty.financialData?.monthlyRent
            },
            'grossYield': selectedProperty.grossYield
      }

    this.api.savePropertyData(payload).subscribe(
        (res: any) => {
            let redirctUrl = '';
            if ((res.body)) {
              if (res.isSuccess == 'true') {
                console.log('record saved');
              }
              else {
                console.log('record already exist, unable to save');
              }
          }
        }, err => {
    });

    alert("Record saved successfully.!");

  }
 
}