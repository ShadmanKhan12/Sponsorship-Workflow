import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-approval-dialog',
  standalone: false,
  templateUrl: './approval-dialog.component.html',
  styleUrls: ['./approval-dialog.component.scss'],
})
export class ApprovalDialogComponent {
  @Input() visible = false;
  @Input() title = 'Approval';
  @Output() confirm = new EventEmitter<{ approved: boolean; remarks?: string }>();
  @Output() close = new EventEmitter<void>();

  remarks = '';

  onConfirm(approved: boolean) {
    this.confirm.emit({ approved, remarks: this.remarks });
    this.remarks = '';
  }
}
