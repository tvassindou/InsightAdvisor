import { Component, inject, OnInit } from '@angular/core';
import { NgbDropdownModule, NgbPaginationModule, NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { HeaderComponent } from '../../../shared/header/header.component';
import { AdvisorDto, AdvisorDtoPagedResult, AdvisorsService } from '../../../shared/advisors.service';
import { FormsModule } from '@angular/forms';
import { HealthStatusComponent } from '../../../shared/health-status/health-status.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-advisor-list',
  standalone: true,
  templateUrl: './advisor-list.component.html',
  styleUrl: './advisor-list.component.scss',
  imports: [FormsModule, RouterLink, NgbTypeaheadModule, NgbPaginationModule, NgbDropdownModule, HeaderComponent, HealthStatusComponent],
})
export class AdvisorListComponent implements OnInit {

  advisors: AdvisorDto[] = [];
  filter: string | undefined = '';
  page: number = 0;
  pageSize: number = 10;
  collectionSize = 0;

  readonly advisorsService: AdvisorsService = inject(AdvisorsService);

  ngOnInit(): void {
  }


  // Méthode pour confirmer et supprimer l'advisor
  confirmDelete(advisor: AdvisorDto): void {
    const confirmDeletion = confirm(`Are you sure you want to delete ${advisor.fullName} ?`);
    if (confirmDeletion) {
      this.advisorsService.deleteAdvisor(advisor.id).subscribe({
        next: () => {
          this.refreshAdvisors();
        },
        error: (error) => {
          console.error('Error while deleting:', error);
        }
      });
    }
  }
  refreshAdvisors() {
    this.advisorsService.getAllAdvisors(this.filter, (this.page - 1) * this.pageSize, this.pageSize)
      .subscribe(
        {
          next: (data: AdvisorDtoPagedResult) => {
            this.advisors = data.items ?? [];
            this.collectionSize = data.totalItems;
          },
          error(error) {
            console.error('Error loading advisors:', error);
          }
        }
      );
  }
}
