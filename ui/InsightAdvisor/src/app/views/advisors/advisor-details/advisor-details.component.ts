import { Component, inject, OnInit } from '@angular/core';
import { HeaderComponent } from '../../../shared/header/header.component';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { AdvisorDto, AdvisorsService } from '../../../shared/advisors.service';

@Component({
  selector: 'app-advisor-details',
  standalone: true,
  templateUrl: './advisor-details.component.html',
  styleUrl: './advisor-details.component.scss',
  imports: [HeaderComponent, RouterLink],
})
export class AdvisorDetailsComponent implements OnInit {
  advisorId: number = 0;
  advisorDetails: AdvisorDto | any = {};


  readonly advisorsService: AdvisorsService = inject(AdvisorsService);
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.advisorId = Number(this.route.snapshot.paramMap.get('id'));
    this.advisorsService.getAdvisorById(this.advisorId).subscribe({
      next: (data) => {
        this.advisorDetails = data
      },
      error: (error) => {
        console.error('Error while get details:', error);
      }
    })
  }
}
