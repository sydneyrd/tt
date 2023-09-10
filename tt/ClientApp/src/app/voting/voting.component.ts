import { Component, OnInit } from '@angular/core';
import { IPlayer } from '../Interfaces/IPlayer';


@Component({
  selector: 'voting',
  templateUrl: './voting.component.html',
  styleUrls: ['./voting.component.css']
})
export class VotingComponent implements OnInit {
  players: IPlayer[] = [
    { playerId: '1', user: 'Player 1', currentGameId: "hiiiiiii", isAi: false},
    { playerId: '2', user: 'Player 2', currentGameId: "hiiiiiii", isAi: false},
    { playerId: '3', user: 'Player 3', currentGameId: "hiiiiiii", isAi: false},
    { playerId: '4', user: 'Player 4', currentGameId: "hiiiiiii", isAi: false},
    {playerId: '5', user: 'Player 5', currentGameId: "hiiiiiii", isAi: false},
    {playerId: '6', user: 'Player 6', currentGameId: "hiiiiiii", isAi: false},

  ];
  selectedPlayerId: string | null = null;
  isVoteIncognito: boolean = false;

  constructor() { }
//countdown timer for voting phase will need to reflect voting time and not available round time
  ngOnInit(): void {
    // TODO: Fetch the list of players.
  }

  selectPlayer(id: string): void {
    this.selectedPlayerId = id;
  }

  toggleIncognito(): void {
    this.isVoteIncognito = !this.isVoteIncognito;
  }

  castVote(): void {
    // TODO: Implement the vote casting logic.
    //send the player vote to the server, along with isVoteIncognito
  }
}
