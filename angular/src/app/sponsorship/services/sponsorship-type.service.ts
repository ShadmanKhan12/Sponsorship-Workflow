import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { SponsorshipTypeService as SponsorshipTypeProxy } from 'src/app/proxy/services/sponsorship-type.service';

@Injectable({ providedIn: 'root' })
export class SponsorshipTypeService {
  constructor(private proxy: SponsorshipTypeProxy) {}

  getAll(): Observable<any[]> {
    return this.proxy.getList().pipe(map((r: any) => r || []), catchError(() => of([])));
  }

  create(dto: any) {
    return this.proxy.create(dto);
  }

  update(id: string, dto: any) {
    return this.proxy.update(id, dto);
  }

  delete(id: string) {
    return this.proxy.delete(id);
  }
}
