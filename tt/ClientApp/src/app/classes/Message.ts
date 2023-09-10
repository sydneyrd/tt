import { IMessage } from '../Interfaces/IMessage';


export class Message implements IMessage {
  text: string;
  user: string;
  constructor(text: string, user: string) {
    this.text = text;
    this.user = user;
  }
}
