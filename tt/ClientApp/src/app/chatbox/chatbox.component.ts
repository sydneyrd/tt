import { Component, OnInit } from "@angular/core";
import { ChatService } from "../services/chat-service.service";
import { IMessage } from '../Interfaces/IMessage';
import { Message } from "../classes/Message";
import { IPlayer } from "../Interfaces/IPlayer";
import { Player } from "../classes/Player";

import { trigger, transition, style, animate } from '@angular/animations';
import { ViewChild, ElementRef, AfterViewChecked } from '@angular/core';
import { GameService } from "../services/game-service-service";

@Component({
    selector: 'chatbox',
    templateUrl: './chatbox.component.html',
    styleUrls: ['./chatbox.component.css'],
    animations: [
      trigger('type', [
        transition(':enter', [
          style({ opacity: 0, transform: 'translateY(-100%)' }),
          animate('2s ease', style({ opacity: 1, transform: 'translateY(0)' })),
        ]),
      ]),
    ],
  })
export class ChatBoxComponent implements AfterViewChecked {
    player: IPlayer = this.gameService.player;
    messages: IMessage[] = [];
    newMessage: string = '';
 
    constructor(private chatService: ChatService, private gameService: GameService) { }
    @ViewChild('chatbox')
    private chatbox!: ElementRef;
    ngAfterViewChecked() {
        this.scrollToBottom();
      }
    
      scrollToBottom(): void {
        try {
          this.chatbox.nativeElement.scrollTop = this.chatbox.nativeElement.scrollHeight;
        } catch (err) { }
      }
    

    ngOnInit() {
        this.chatService.startConnection(this.gameService.gameId, this.player.playerId);
        this.chatService.getMessage().subscribe((message: IMessage) => {
            console.log(message);
            console.log(`Received message from ${message.user}: ${message.text}`)
            this.messages.push(message);
        }
        );        
    }
     sendMessage(event: Event): void {
        event.preventDefault();
        if (this.newMessage) {
    let  message: IMessage = new Message(this.newMessage, this.player.user)
        this.chatService.sendMessageToGroup(message);
        this.newMessage = '';
      }
}
}