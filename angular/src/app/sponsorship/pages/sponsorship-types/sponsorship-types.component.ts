import { Component, OnInit } from '@angular/core';
import { SponsorshipTypeService } from '../../services/sponsorship-type.service';

@Component({
  selector: 'app-sponsorship-types',
  standalone: false,
  templateUrl: './sponsorship-types.component.html',
  styleUrls: ['./sponsorship-types.component.scss'],
})
export class SponsorshipTypesPageComponent implements OnInit {
  items: any[] = [];
  model: any = { name: '', description: '', isActive: true };
  editing: any = null;

  constructor(private svc: SponsorshipTypeService) {}

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.svc.getAll().subscribe((r) => (this.items = r));
  }

  edit(item: any) { this.editing = { ...item }; }
  create() { this.svc.create(this.model).subscribe(() => { this.model = { name: '', description: '', isActive: true }; this.load(); }); }
  save() { this.svc.update(this.editing.id, this.editing).subscribe(() => { this.editing = null; this.load(); }); }
  remove(id: string) { if (confirm('Delete type?')) this.svc.delete(id).subscribe(() => this.load()); }
}
