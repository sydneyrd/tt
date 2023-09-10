import { Component, EventEmitter, Output } from '@angular/core';
import { ColorService } from '../services/color.service';
import { style } from '@angular/animations';

@Component({
  selector: 'app-color-picker',
  templateUrl: './color-picker.component.html',
  styleUrls: ['./color-picker.component.css']
})
export class ColorPickerComponent {
  colors = ['rgb(255, 0, 0)', 'rgb(0, 255, 0)', 'rgb(0, 0, 255)', 'rgb(255, 255, 0)', 'rgb(255, 0, 255)', 'rgb(0, 255, 255)', 'rgb(255, 255, 255)', 'rgb(128, 0, 128)', 'rgb(144, 238, 144)', 'rgb(255, 192, 203)', 'rgb(255, 165, 0)', 'rgb(255, 255, 0)', 'rgb(255, 0, 255)', 'rgb(0, 255, 255)'];
  currentColor = this.colors[0];
  backgroundColors = ['rgb(0, 0, 0)', 'rgb(255, 255, 255)', 'rgb(245, 245, 220)', 'rgb(173, 216, 230)', 'rgb(240, 128, 128)', 'rgb(144, 238, 144)', 'rgb(211, 211, 211)', 'rgb(255, 182, 193)', 'rgb(255, 160, 122)', 'rgb(32, 178, 170)'];
  currentBackgroundColor = this.backgroundColors[0];
  isFocused: boolean = false;
  onFocus() {
    this.isFocused = true;
  }
  onBlur() {
    this.isFocused = false;
  }


  constructor(private colorService: ColorService) {}
  changeColor(direction: string) {
    const currentIndex = this.colors.indexOf(this.currentColor);
    if (direction === 'next') {
      if (currentIndex < this.colors.length - 1) {
        this.currentColor = this.colors[currentIndex + 1];
      } else {
        this.currentColor = this.colors[0];  // Loop back to the start
      }
    } else if (direction === 'prev') {
      if (currentIndex > 0) {
        this.currentColor = this.colors[currentIndex - 1];
      } else {
        this.currentColor = this.colors[this.colors.length - 1];  // Loop back to the end
      }
    }
    this.colorService.changeColor(this.currentColor);
    this.colorChange.emit(this.currentColor);
  }
  changeBackgroundColor(direction: string) {
    const currentIndex = this.backgroundColors.indexOf(this.currentBackgroundColor);
    if (direction === 'up') {
      if (currentIndex < this.backgroundColors.length - 1) {
        this.currentBackgroundColor = this.backgroundColors[currentIndex + 1];
      } else {
        this.currentBackgroundColor = this.backgroundColors[0];
      }
    } else if (direction === 'down') {
      if (currentIndex > 0) {
        this.currentBackgroundColor = this.backgroundColors[currentIndex - 1];
      } else {
        this.currentBackgroundColor = this.backgroundColors[this.backgroundColors.length - 1];
      }
    }
    this.colorService.changeBackgroundColor(this.currentBackgroundColor);
  }
  
  onKeydown(event: KeyboardEvent) {
    if (event.key === 'ArrowRight') {
      this.changeColor('next');
    } else if (event.key === 'ArrowLeft') {
      this.changeColor('prev');
    } else if (event.key === 'ArrowUp') {
      this.changeBackgroundColor('up');
    } else if (event.key === 'ArrowDown') {
      this.changeBackgroundColor('down');
    }
  }
  
  @Output() colorChange = new EventEmitter<string>();
  onColorChange(color: string): void {
    this.colorService.changeColor(color);
    this.colorChange.emit(color);
  }
}
