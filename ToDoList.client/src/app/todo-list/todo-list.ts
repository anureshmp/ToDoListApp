import { Component, OnInit, inject, signal } from '@angular/core';
import { DatePipe } from '@angular/common';

import { AddTodo } from '../add-todo/add-todo';
import { ToDoItem } from '../models/todo-item.model';
import { TodoService } from '../services/todo.service';

@Component({
  selector: 'app-todo-list',
  imports: [AddTodo, DatePipe],
  templateUrl: './todo-list.html',
  styleUrl: './todo-list.css',
})
export class TodoList implements OnInit {
  private readonly todoService = inject(TodoService);

  protected readonly items = signal<ToDoItem[]>([]);
  protected readonly loading = signal(true);
  protected readonly error = signal<string | null>(null);

  ngOnInit(): void {
    this.loadItems();
  }

  private loadItems(): void {
    this.loading.set(true);
    this.error.set(null);

    this.todoService.getAll().subscribe({
      next: (items) => {
        this.items.set(items);
        this.loading.set(false);
      },
      error: () => {
        this.error.set('Failed to load todo items.');
        this.loading.set(false);
      },
    });
  }

  protected onAdd(title: string): void {
    this.error.set(null);

    this.todoService.create(title).subscribe({
      next: (item) => this.items.update((items) => [...items, item]),
      error: () => this.error.set('Failed to add todo item.'),
    });
  }

  protected onDelete(id: string): void {
    this.error.set(null);

    this.todoService.delete(id).subscribe({
      next: () => this.items.update((items) => items.filter((i) => i.id !== id)),
      error: () => this.error.set('Failed to delete todo item.'),
    });
  }
}
