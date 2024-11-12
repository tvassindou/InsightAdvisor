import { Component, inject, OnInit } from '@angular/core';
import { HeaderComponent } from '../../../shared/header/header.component';
import { AdvisorsService, UpdateAdvisorDto } from '../../../shared/advisors.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-advisor-edit',
  standalone: true,
  templateUrl: './advisor-edit.component.html',
  styleUrl: './advisor-edit.component.scss',
  imports: [HeaderComponent, FormsModule],
})
export class AdvisorEditComponent implements OnInit {
  advisorId: number | any = {};
  updateAdvisor: UpdateAdvisorDto | any = {};

  readonly advisorsService: AdvisorsService = inject(AdvisorsService);
  constructor(private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.advisorId = Number(this.route.snapshot.paramMap.get('id'));
    this.advisorsService.getAdvisorById(this.advisorId).subscribe({
      next: (data) => {
        this.updateAdvisor = <UpdateAdvisorDto>{
          address: data.address,
          fullName: data.fullName
        }
      },
      error: (error) => {
        console.error('Error while get details:', error);
      }
    })
  }


  onSave(): void {
    this.advisorsService.updateAdvisor(this.advisorId, this.updateAdvisor).subscribe({
      next: (data) => {
        this.router.navigate(['/advisors/details', this.advisorId]);
      },
      error: (error) => {
        console.error('Error while update Advisor:', error);
      }
    })
  }

  onCancel(): void {
    this.router.navigate(['/advisors/details', this.advisorId]);
  }
}