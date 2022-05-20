import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Actions, ActionsService } from '../actions.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  items: MenuItem[] = [];

  constructor(private readonly actionService: ActionsService) { }

  ngOnInit(): void {
    this.items = [
      {
        label: 'Hermes'
      },
      {
        label: 'Delete All',
        icon: 'pi pi-fw pi-trash',
        command: () => this.actionService.handlerAction(Actions.DeleteAll)
      },
      {
        label: 'Refresh',
        icon: 'pi pi-fw pi-refresh',
        command: () => this.actionService.handlerAction(Actions.RefreshTable)
      }
    ];
  }

}
