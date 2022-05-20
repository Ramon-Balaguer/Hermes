import { Component, OnDestroy, OnInit } from "@angular/core";
import { TableModule } from "primeng/table";
import { IMessage } from "./IMessage";
import { HermesApiService } from "./services/hermesApi.service"
import { Message } from "./model/message"
import { Subscription } from "rxjs";
import { Actions, ActionsService } from "../actions.service";

@Component({
  selector: "app-messages-grid",
  templateUrl: "./messages-grid.component.html",
  styleUrls: ["./messages-grid.component.css"]
})
export class MessagesGridComponent implements OnInit, OnDestroy {

  messages = new Array<Message>();
  private subscriptionHermesApiService: Subscription = new Subscription;
  private subscriptionActionService: Subscription = new Subscription;

  constructor(private readonly hermesApiService: HermesApiService, private readonly actionService: ActionsService) {}

  ngOnInit(): void {
    this.RefreshMessages();
    this.actionService.currentAction()
    .subscribe(
      (action: Actions) => {
        switch (action) {
          case Actions.RefreshTable:
            this.RefreshMessages();
            break;
          case Actions.DeleteAll:
            this.hermesApiService.deleteMessages().subscribe(() => this.RefreshMessages());
            break;
        }
      }
    );
  }

  ngOnDestroy(): void {
    this.subscriptionHermesApiService.unsubscribe();
    this.subscriptionActionService.unsubscribe();
  }

  public RefreshMessages() {
    this.subscriptionHermesApiService = this.hermesApiService.getMessages()
      .subscribe(
        (messages: Message[]) => this.messages = messages
      );
  }
}
