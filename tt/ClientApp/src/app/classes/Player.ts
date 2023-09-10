import { IPlayer} from "../Interfaces/IPlayer";

export class Player implements IPlayer {
    user: string;
    currentGameId: string;
    playerId: string;
    isAi: boolean;
    constructor(user: string, currentGameId: string, playerId: string) {
        this.user = user;
        this.currentGameId = currentGameId;
        this.playerId = playerId;
        this.isAi = false;
        
    }
}