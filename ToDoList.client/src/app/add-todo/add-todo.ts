import { Component, output } from '@angular/core';

@Component({
  selector: 'app-add-todo',
  templateUrl: './add-todo.html',
  styleUrl: './add-todo.css',
})
export class AddTodo {
  readonly add = output<string>();

  protected onSubmit(form: HTMLFormElement, titleInput: HTMLInputElement): void {
    const title = titleInput.value.trim();
    if (!title) {
      return;
    }

    this.add.emit(title);
    form.reset();
  }
}
