import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChatService } from '../../services/chat';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent {

  question = '';
  response = '';
  loading = false;

  constructor(private chatService: ChatService) { }

  askQuestion() {

    if (!this.question.trim()) {
      return;
    }

    this.loading = true;

    this.chatService
      .askQuestion(this.question)
      .subscribe({
        next: (result) => {
          this.response = result.response;
          this.loading = false;
        },
        error: (error) => {
          console.error(error);
          this.loading = false;
        }
      });
  }
}