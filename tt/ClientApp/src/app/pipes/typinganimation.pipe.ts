import { Pipe, PipeTransform } from '@angular/core';
import { Observable, concatMap, from, map, timer } from 'rxjs';

@Pipe({name: 'typingAnimation'})
export class TypingAnimationPipe implements PipeTransform {
  transform(value: string): Observable<string> {
    return from(value.split('')).pipe(concatMap((char, i) => timer(i * 150).pipe(map(() => char))));
  }
}
