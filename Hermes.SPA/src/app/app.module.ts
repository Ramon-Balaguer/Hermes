import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TableModule } from 'primeng/table';
import { MessagesGridComponent } from './messages-grid/messages-grid.component';
import { HttpClientModule } from '@angular/common/http';
import { HermesApiService } from './messages-grid/api/hermesApi.service';


@NgModule({
  declarations: [
    AppComponent,
    MessagesGridComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    TableModule,
    HttpClientModule
  ],
  providers: [HermesApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
