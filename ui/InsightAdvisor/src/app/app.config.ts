import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { AppConsts } from './shared/AppConsts';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { API_BASE_URL } from './shared/advisors.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideClientHydration(),
    provideHttpClient(withFetch()),
    { provide: API_BASE_URL, useFactory: () => AppConsts.remoteServiceBaseUrl },
  ]
};
