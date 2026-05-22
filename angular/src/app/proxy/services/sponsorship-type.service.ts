import { RestService, Rest } from '@abp/ng.core';
import { Injectable, inject } from '@angular/core';
import type { CreateUpdateSponsorshipTypeDto, SponsorshipTypeDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class SponsorshipTypeService {
  private restService = inject(RestService);
  apiName = 'Default';
  

  create = (input: CreateUpdateSponsorshipTypeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SponsorshipTypeDto>({
      method: 'POST',
      url: '/api/app/sponsorship-type',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/sponsorship-type/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, SponsorshipTypeDto[]>({
      method: 'GET',
      url: '/api/app/sponsorship-type',
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateSponsorshipTypeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SponsorshipTypeDto>({
      method: 'PUT',
      url: `/api/app/sponsorship-type/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
}