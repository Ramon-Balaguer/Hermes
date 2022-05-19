import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MessagesGridComponent } from "./messages-grid/messages-grid.component"

const routes: Routes = [
  { path: "", component: MessagesGridComponent }
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
