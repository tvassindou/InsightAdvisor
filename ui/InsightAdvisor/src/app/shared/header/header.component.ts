// header.component.ts
import { Component, Input } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    standalone: true,
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
    @Input() title: string = '';
    @Input() backLink: string | undefined;
    constructor(private location: Location, private router: Router) { }

    goBack(): void {
        if (this.backLink) {
            this.router.navigate([this.backLink]);
            window.location.href = this.backLink;
        } else {
            this.location.back();
        }
    }
}
