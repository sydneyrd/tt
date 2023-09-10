import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';   // install @microsoft/signalr
import { Observable } from 'rxjs';
import { IPlayer } from '../Interfaces/IPlayer';

@Injectable({
  providedIn: 'root'
})
export class LobbyService {
  private hubConnection!: HubConnection;
    public startConnection(): void {
    this.hubConnection = new HubConnectionBuilder( )
                            .withUrl('https://localhost:7201/lobby')
                            .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  };
 public createGame(): void {
    this.hubConnection.invoke('CreateGame');
  };
  public joinGame(gameCode: string): void {
    this.hubConnection.invoke('JoinGame', gameCode);
  };
  public findGame(): void {
    this.hubConnection.invoke('JoinOpenGame');
  };
  public gameStart(): Observable<string> {
    return new Observable<string>(observer => {
      this.hubConnection.on('GameStart', (serverMessage: string) => {
        observer.next(serverMessage);
        console.log(serverMessage + "this is the message i got back on the client side")
      });
    });
  };
  public PlayerInfo(): Observable<IPlayer> {
    return new Observable<IPlayer>(observer => {
      this.hubConnection.on('Player', (player: IPlayer) => {
        observer.next(player);
      });
    });
  };

public getGameCode(): Observable<string> {
    return new Observable<string>(observer => {
      this.hubConnection.on('Roomcode', (roomcode: string) => {
        observer.next(roomcode);
        console.log(roomcode + "this is the code i got back on the client side");
      });
    });
  };
}


