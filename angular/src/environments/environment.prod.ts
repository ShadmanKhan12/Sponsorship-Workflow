import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44363/',
  redirectUri: baseUrl,
  clientId: 'SponsorshipWorkflow_App',
  responseType: 'code',
  scope: 'offline_access SponsorshipWorkflow',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'SponsorshipWorkflow',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44363',
      rootNamespace: 'SponsorshipWorkflow',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
