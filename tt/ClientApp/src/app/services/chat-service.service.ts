import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';   // install @microsoft/signalr
import { Observable } from 'rxjs';
import { Message } from '../classes/Message';
import { IMessage } from '../Interfaces/IMessage';


@Injectable({
  providedIn: 'root'
})

export class ChatService {
  private hubConnection!: HubConnection;
  constructor() {  //game info in constructor?
   }
  public startConnection(gameId: string, playerId: string): void {
    this.hubConnection = new HubConnectionBuilder( )
                            .withUrl('https://localhost:7201/chatHub?gameId=' + gameId + '&playerId=' + playerId) 
                            .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))

  };

 public sendMessageToGroup(message: IMessage): void {
    this.hubConnection
    .invoke('SendMessage', message)
    .catch(err => console.error(err));
  };

public  getMessage(): Observable<IMessage> {
    return new Observable<IMessage>(observer => {
      this.hubConnection
      .on('ReceiveMessage', (message: IMessage) => {
        observer.next(message);
        console.log(message);
      });
    });
  };
  public stopConnection(): void {
    this.hubConnection.stop();
  }

}


