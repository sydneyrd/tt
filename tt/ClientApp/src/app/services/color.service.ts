import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ColorService {
  private colorSource = new BehaviorSubject('#FFFFFF');

  // Expose the color as an Observable for components to subscribe to
  currentColor = this.colorSource.asObservable();
 

  constructor() { }
  public getColor(): string {
    return this.colorSource.getValue();
  }
  public changeColor(color: string) {
    this.colorSource.next(color);
  }
  private backgroundColorSource = new BehaviorSubject('black');
currentBackgroundColor = this.backgroundColorSource.asObservable();

public changeBackgroundColor(color: string) {
  this.backgroundColorSource.next(color);
}

}
