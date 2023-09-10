import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, NgModelGroup, NgModel } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { JoinComponent } from './join/join.component';
import { ChatBoxComponent } from './chatbox/chatbox.component';
import { ChatService } from './services/chat-service.service';
import { HubConnection } from '@microsoft/signalr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ClipboardModule } from '@angular/cdk/clipboard';
import { GameService } from './services/game-service-service';
import { LoadingScreenComponent } from './loading-screen/loading-screen.component';
import { CommonModule } from '@angular/common';
import { ColorService } from './services/color.service';
import { ColorPickerComponent } from './color-picker/color-picker.component';
import { VotingComponent } from './voting/voting.component';
import { GameStatusAreaComponent } from './game-status-area/game-status-area.component';
import { GameScreenComponent } from './gamescreen/game-screen.component';
import { SharedTerminalComponent } from './shared-terminal/shared-terminal.component';
import { GameInputAreaComponent } from './game-input/game-input.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ColorPickerComponent,
    HomeComponent,
    JoinComponent,
    ChatBoxComponent,
    LoadingScreenComponent,
    VotingComponent,
    GameStatusAreaComponent,
    GameScreenComponent,
    SharedTerminalComponent,
    GameInputAreaComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CommonModule,
    ClipboardModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
        { path: 'join', component: JoinComponent },
        { path: 'chat', component: GameScreenComponent},
        {path: 'game', component: GameScreenComponent},
        { path: 'voting', component: VotingComponent}
        // { path: 'chat', loadChildren: () => import('./chatbox/chatbox.module').then(m => m.ChatBoxModule) } haven't made children yet
    ]),
    BrowserAnimationsModule,

  ],
  providers: [
    ChatService,
    NgModelGroup,
    NgModel,
    FormsModule,
    HttpClientModule,
    CommonModule,
    GameService,
    ColorService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
