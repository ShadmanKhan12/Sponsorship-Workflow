import { Environment } from '@abp/ng.core';

const baseUrl = 'https://sponsorship-workflow.vercel.app';

const apiUrl = 'https://sponsorship-workflow-l8d2.onrender.com';

const oAuthConfig = {
  issuer: apiUrl,
  redirectUri: baseUrl,
  clientId: 'SponsorshipWorkflow_App',
  responseType: 'code',
  scope: 'offline_access openid profile email roles SponsorshipWorkflow',
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
      url: apiUrl,
      rootNamespace: 'SponsorshipWorkflow',
    },
    AbpAccountPublic: {
      url: apiUrl,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: `${apiUrl}/getEnvConfig`,
    mergeStrategy: 'deepmerge'
  }
} as Environment;