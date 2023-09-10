import { Injectable } from '@angular/core';
import { IPlayer } from '../Interfaces/IPlayer';
@Injectable({
  providedIn: 'root'
})
export class GameService {
  private gameCodeKey = 'gameCode';
  public player!: IPlayer;
  public gameId! : string;
  constructor() { }

  getGameCode(): string | null {
    return sessionStorage.getItem(this.gameCodeKey);
  }

  setGameCode(gameCode: string): void {
    sessionStorage.setItem(this.gameCodeKey, gameCode);
  }

  clearGameCode(): void {
    sessionStorage.removeItem(this.gameCodeKey);
  }
  setPlayer(player: IPlayer): void {
   localStorage.setItem("player", JSON.stringify(player));
   this.player = player;
   this.gameId = player.currentGameId;
  }
  getPlayer(): IPlayer {
     localStorage.getItem("player");
      return JSON.parse(localStorage.getItem("player") || '{}');
  }
  stopGame(): void {
    localStorage.removeItem("player");
  }
}
