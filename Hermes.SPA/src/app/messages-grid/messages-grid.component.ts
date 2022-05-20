import { Component, OnInit } from "@angular/core";
import { TableModule } from "primeng/table";
import { IMessage } from "./IMessage";
import { HermesApiService } from "./api/hermesApi.service"
import { Message } from "./model/message"

@Component({
  selector: "app-messages-grid",
  templateUrl: "./messages-grid.component.html",
  styleUrls: ["./messages-grid.component.css"]
})
export class MessagesGridComponent implements OnInit {

  messages = new Array<Message>();

  constructor(private readonly hermesApiService: HermesApiService) {}

  ngOnInit(): void {
    this.hermesApiService.getMessages()
      .subscribe(
       (messages: Message[])=> this.messages = messages
      );
  }

}
