import { Component, OnInit } from "@angular/core";
import { TableModule } from "primeng/table";
import { IMessage } from "./IMessage";


@Component({
  selector: "app-messages-grid",
  templateUrl: "./messages-grid.component.html",
  styleUrls: ["./messages-grid.component.css"]
})
export class MessagesGridComponent implements OnInit {

  messages = new Array<IMessage>();

  ngOnInit(): void {
  }

}
