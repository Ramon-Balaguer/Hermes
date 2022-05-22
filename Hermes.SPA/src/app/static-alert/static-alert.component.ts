import { Component, OnDestroy, OnInit } from '@angular/core';
import {Message} from 'primeng/api';
import { Subscription } from 'rxjs';
import { ServiceInformation } from '../messages-grid/model/serviceInformation';
import { HermesApiService } from '../messages-grid/services/hermesApi.service';


@Component({
  selector: 'app-static-alert',
  templateUrl: './static-alert.component.html',
  styleUrls: ['./static-alert.component.css']
})
export class StaticAlertComponent implements OnInit, OnDestroy {

  alerts: Message[] = [];
  private subscriptionHermesApiService: Subscription = new Subscription;

  constructor(private readonly hermesApiService: HermesApiService) { }

  ngOnInit(): void {

    this.subscriptionHermesApiService = this.hermesApiService.getServiceInformation()
      .subscribe(
        (serviceInformation: ServiceInformation) =>{
         this.alerts = [
            {severity:'info', summary:'Server Info', detail:`Name: ${serviceInformation.name} \n Ports: ${serviceInformation.ports}` },
          ]
        }
      );
  }
  
  ngOnDestroy(): void {
    this.subscriptionHermesApiService.unsubscribe();
  }

}
