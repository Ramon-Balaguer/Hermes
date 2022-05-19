import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MessagesGridComponent } from './messages-grid.component';

describe('MessagesGridComponent', () => {
  let component: MessagesGridComponent;
  let fixture: ComponentFixture<MessagesGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MessagesGridComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MessagesGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
