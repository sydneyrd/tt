import { Component, OnInit, OnDestroy } from '@angular/core';
import { ChatService } from "../services/chat-service.service";
import { GameService } from "../services/game-service-service";

@Component({
  selector: 'game-screen',
  templateUrl: './game-screen.component.html',
  styleUrls: ['./game-screen.component.css'],
})
export class GameScreenComponent implements OnInit, OnDestroy {

  constructor(private chatService: ChatService, private gameService: GameService) {}

  ngOnInit(): void {
  }
  ngOnDestroy(): void {
    // Stop or destroy the services here
    this.chatService.stopConnection();
    this.gameService.stopGame();
  }

}
