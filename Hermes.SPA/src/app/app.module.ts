import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TableModule } from 'primeng/table';
import { MenubarModule } from 'primeng/menubar';
import { MessagesGridComponent } from './messages-grid/messages-grid.component';
import { HttpClientModule } from '@angular/common/http';
import { HermesApiService } from './messages-grid/services/hermesApi.service';
import { MenuComponent } from './menu/menu.component';
import { ActionsService } from './actions.service';


@NgModule({
  declarations: [
    AppComponent,
    MessagesGridComponent,
    MenuComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    TableModule,
    MenubarModule,
    HttpClientModule
  ],
  providers: [HermesApiService, ActionsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
