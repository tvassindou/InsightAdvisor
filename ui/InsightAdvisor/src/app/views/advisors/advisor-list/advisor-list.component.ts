import { Component, inject, OnInit } from '@angular/core';
import { AdvisorsServiceProxy } from '../../../shared/service-proxies/service-proxies';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { HeaderComponent } from '../../../shared/header/header.component';

@Component({
  selector: 'app-advisor-list',
  standalone: true,
  templateUrl: './advisor-list.component.html',
  styleUrl: './advisor-list.component.scss',
  imports: [NgbAlertModule, HeaderComponent],
})
export class AdvisorListComponent implements OnInit {

  advisors: any[] = [];
  filter: string | undefined = '';
  page: number = 0;
  pageSize: number = 10;
  readonly advisorsService: AdvisorsServiceProxy = inject(AdvisorsServiceProxy);




  ngOnInit(): void {
    this.advisorsService.getAll(this.filter, this.page, this.pageSize)
      .subscribe(
        {
          next(data) {
            console.log('Advisors loaded : ', data);
            if(data!=null){

              console.log('Advisors loaded : ', data);
            }
          },
          error(error) {
            console.error('Error loading advisors:', error);
          }
        }
      );
  }
}
