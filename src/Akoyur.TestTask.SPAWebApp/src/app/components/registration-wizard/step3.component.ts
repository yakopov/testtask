import { Component, Input, OnInit } from '@angular/core';

@Component({
  standalone: false,
  selector: 'app-step3',
  templateUrl: './step3.component.html',
  styleUrls: ['./step3.component.css']
})

export class Step3Component implements OnInit {
  @Input() userId!: number;

  ngOnInit(): void {
  }
}
