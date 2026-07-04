import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-league-tabs',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './league-tabs.component.html',
  styleUrl: './league-tabs.component.css'
})
export class LeagueTabsComponent {
  @Input() tabs: { id: string, name: string }[] = [];
  @Input() activeTabId: string = '';
  @Output() tabChange = new EventEmitter<string>();

  selectTab(id: string) {
    this.tabChange.emit(id);
  }
}
