import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable()
export class ActionsService {

  private messageSource = new Subject<Actions>();

  constructor() { }
  
  handlerAction(action: Actions) {
    this.messageSource.next(action)
  }

  currentAction() : Observable<Actions> {
    return this.messageSource.asObservable();
  }
}

export enum Actions{
  RefreshTable,
  DeleteAll
}
