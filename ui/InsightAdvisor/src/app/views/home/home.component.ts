import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
  imports: [RouterLink]
})
export class HomeComponent {
  title = 'Insight Advisor';
  logoUrl = 'assets/logo.svg';
}
