import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges } from '@angular/core';

@Component({
    selector: 'app-health-status',
    templateUrl: './health-status.component.html',
    styleUrls: ['./health-status.component.scss'],
    standalone: true,
    imports: [CommonModule],
})
export class HealthStatusComponent implements OnChanges {
    @Input() value: number = 0;
    colorClass: string = '';

    ngOnChanges(): void {
        this.updateColor();
    }

    private updateColor(): void {
        if (this.value >= 0.6) {
            this.colorClass = 'green';
        } else if (this.value >= 0.2) {
            this.colorClass = 'yellow';
        } else {
            this.colorClass = 'red';
        }
    }
}
