import { Environment } from '@abp/ng.core';

const baseUrl = 'https://sponsorship-workflow.vercel.app';
const apiUrl = 'https://sponsorship-workflow-l8d2.onrender.com/';

export const environment: Environment = {
  production: true,

  application: {
    baseUrl,
    name: 'SponsorshipWorkflow',
  },

  oAuthConfig: {
    issuer: apiUrl,
    redirectUri: baseUrl,
    clientId: 'SponsorshipWorkflow_App',
    responseType: 'code',
    scope: 'openid profile email roles offline_access SponsorshipWorkflow',
    // API is served over HTTPS on Render; metadata discovery uses https.
    requireHttps: true,
    strictDiscoveryDocumentValidation: false,
  },

  apis: {
    default: {
      url: apiUrl,
      rootNamespace: 'SponsorshipWorkflow',
    },
    AbpAccountPublic: {
      url: apiUrl,
      rootNamespace: 'AbpAccountPublic',
    },
  }
};