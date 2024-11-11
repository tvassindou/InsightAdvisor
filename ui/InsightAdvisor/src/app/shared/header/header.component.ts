// header.component.ts
import { Component, Input } from '@angular/core';
import { Location } from '@angular/common';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    standalone: true,
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
    @Input() title: string = ''; 
    @Input() backLink: string | undefined;
    constructor(private location: Location) { }

    goBack(): void {
        if (this.backLink) {
            window.location.href = this.backLink; 
        } else {
            this.location.back(); 
        }
    }
}
