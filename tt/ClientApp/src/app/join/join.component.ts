import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { LobbyService } from "./lobby-service";
import {Clipboard} from '@angular/cdk/clipboard';
import { ViewChild } from '@angular/core';
import { ClipboardModule } from '@angular/cdk/clipboard';
import { GameService } from "../services/game-service-service";
import { Subscription, timeout } from "rxjs";
import { IPlayer } from "../Interfaces/IPlayer";


//when we click the join button i will be added to a game lobby.  when i enter the lobby i will receive my players username
@Component({
    selector: "join",
    templateUrl: "./join.component.html",

})
export class JoinComponent{
    constructor(private router: Router,
                private lobbyService: LobbyService,
                private clipboard: Clipboard,
                private gameService: GameService
                ) { }
private gameCodeSubscription: Subscription | undefined;
    gameId: string = "";
    showGameCodeInput: boolean = false;
    gameCode: string = "";
    showGameCode: boolean = false;
    showLoadingScreen: boolean = false;
    findGame(): void {
        this.lobbyService.findGame();
        this.showLoadingScreen = true;
        console.log("find game button clicked")


        // this.router.navigate(["/chat"]);
    }
    createGame(): void {
        this.lobbyService.createGame();
        this.showLoadingScreen = true;
    }
    joinGameButton(): void {
        this.showGameCodeInput = true;
    }
    joinGame(): void {
        this.lobbyService.joinGame(this.gameCode);
        this.gameService.setGameCode(this.gameCode);
        this.showLoadingScreen = true;
        
    }
   copyGameId(): void {
        this.clipboard.copy(this.gameId);
      }
ngOnInit() {
    this.lobbyService.startConnection();
   this.gameCodeSubscription = this.lobbyService.getGameCode().subscribe((roomcode: string) => {
        this.gameId = roomcode;
        this.clipboard.copy(this.gameId);
    }
    ); 
    this.lobbyService.PlayerInfo().subscribe(async (player: IPlayer) => {
        console.log(player);
       this.gameService.setPlayer(player);
        }
    );
    this.lobbyService.gameStart().subscribe(async (m : string) => {
        console.log(m);
        this.router.navigate(["/chat"]);
        }
    );
}
ngOnDestroy() {
    // Unsubscribe from the subscription to close the connection
    if (this.gameCodeSubscription !== undefined) {
      this.gameCodeSubscription.unsubscribe();
    }
  }
}

// Path: src\app\join\join.component.html