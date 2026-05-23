import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SponsorshipRequest } from '../../models/request.model';

@Component({
  selector: 'app-request-form',
  standalone: false,
  templateUrl: './request-form.component.html',
  styleUrls: ['./request-form.component.scss'],
})
export class RequestFormComponent implements OnInit, OnChanges {
  @Input() request?: SponsorshipRequest | null;
  @Input() sponsorshipTypes: any[] = [];
  @Output() save = new EventEmitter<{ dto: any; submit?: boolean }>();

  form!: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.buildForm();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['request'] && this.form) {
      this.patchForm(this.request);
    }
  }

  private buildForm(): void {
    this.form = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(200)]],
      requestorName: ['', [Validators.required, Validators.maxLength(100)]],
      department: ['', [Validators.required, Validators.maxLength(100)]],
      sponsorshipTypeId: [null as string | null, [Validators.required]],
      eventName: ['', [Validators.maxLength(200)]],
      eventDate: [''],
      requestedAmount: [0, [Validators.required, Validators.min(0)]],
      purpose: ['', [Validators.maxLength(1000)]],
      expectedBusinessBenefit: ['', [Validators.maxLength(1000)]],
      remarks: ['', [Validators.maxLength(1000)]],
    });
    this.patchForm(this.request);
  }

  private patchForm(request?: SponsorshipRequest | null): void {
    if (!request) return;
    this.form.patchValue({
      title: request.title || '',
      requestorName: request.requestorName || '',
      department: request.department || '',
      sponsorshipTypeId: request.sponsorshipTypeId || null,
      eventName: request.eventName || '',
      eventDate: request.eventDate ? String(request.eventDate).substring(0, 10) : '',
      requestedAmount: request.requestedAmount ?? 0,
      purpose: request.purpose || '',
      expectedBusinessBenefit: request.expectedBusinessBenefit || '',
      remarks: request.remarks || '',
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
