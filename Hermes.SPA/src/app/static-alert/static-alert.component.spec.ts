import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaticAlertComponent } from './static-alert.component';

describe('StaticAlertComponent', () => {
  let component: StaticAlertComponent;
  let fixture: ComponentFixture<StaticAlertComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StaticAlertComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StaticAlertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
