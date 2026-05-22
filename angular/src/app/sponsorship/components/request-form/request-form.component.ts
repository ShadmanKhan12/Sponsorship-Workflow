import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SponsorshipRequest } from '../../models/request.model';

@Component({
  selector: 'app-request-form',
  standalone: false,
  templateUrl: './request-form.component.html',
  styleUrls: ['./request-form.component.scss'],
})
export class RequestFormComponent implements OnInit {
  @Input() request?: SponsorshipRequest | null;
  @Input() sponsorshipTypes: any[] = [];
  @Output() save = new EventEmitter<{ dto: any; submit?: boolean }>();

  form!: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      title: [this.request?.title || '', [Validators.required, Validators.maxLength(200)]],
      requestorName: [this.request?.requestorName || '', [Validators.required]],
      department: [this.request?.department || ''],
      sponsorshipTypeId: [this.request?.sponsorshipTypeId || '', [Validators.required]],
      eventName: [this.request?.eventName || ''],
      eventDate: [this.request?.eventDate || ''],
      requestedAmount: [this.request?.requestedAmount || 0, [Validators.min(0)]],
      purpose: [this.request?.purpose || '', [Validators.maxLength(1000)]],
      expectedBusinessBenefit: [this.request?.expectedBusinessBenefit || ''],
      remarks: [this.request?.remarks || ''],
    });
  }

  submitDraft() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.save.emit({ dto: this.form.value, submit: false });
  }

  submitFinal() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.save.emit({ dto: this.form.value, submit: true });
  }
}
