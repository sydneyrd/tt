import { Component, OnInit, OnDestroy } from '@angular/core';
import { GameService } from '../services/game-service-service';
import { IPlayer } from '../Interfaces/IPlayer';

enum GamePhase {
  Discussion,
  Task,
  Voting
}

@Component({
  selector: 'game-status-area',
  templateUrl: './game-status-area.component.html',
  styleUrls: ['./game-status-area.component.css']
})
export class GameStatusAreaComponent implements OnInit, OnDestroy {
  gamePhase: GamePhase = GamePhase.Discussion;
  timeLeft: number = 600;  // Start with 10 minutes (10 * 60 seconds)
  playerCount: number = 0;
  timerId?: number;
  player: IPlayer = this.gameService.player;
  constructor(private gameService: GameService) { }

  ngOnInit(): void {
    // TODO: Fetch game status information.
    this.startTimer();
  }

  ngOnDestroy(): void {
    if (this.timerId) {
      clearInterval(this.timerId);
    }
  }

  startTimer(): void {
    this.timerId = window.setInterval(() => {
      if (this.timeLeft > 0) {
        this.timeLeft--;
      } else {
        clearInterval(this.timerId);
      }
    }, 1000);
  }

  getMinutes(): number {
    return Math.floor(this.timeLeft / 60);
  }

  getSeconds(): number {
    return this.timeLeft % 60;
  }
}
