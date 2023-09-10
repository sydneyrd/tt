import { Component } from '@angular/core';
import { ColorService } from './services/color.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  getColor() {
    return this.colorService.getColor();
  }
  constructor(private colorService: ColorService) {}
  ngOnInit() {
  this.colorService.currentColor.subscribe(color => {
    document.documentElement.style.setProperty('--text-color', color);
    document.body.style.setProperty('--text-color', color);
    console.log(color);
  });
  this.colorService.currentBackgroundColor.subscribe(color => {
    document.documentElement.style.setProperty('--background-color', color);
  });
}
}
