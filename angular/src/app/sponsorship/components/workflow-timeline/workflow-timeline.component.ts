import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-workflow-timeline',
  standalone: false,
  templateUrl: './workflow-timeline.component.html',
  styleUrls: ['./workflow-timeline.component.scss'],
})
export class WorkflowTimelineComponent {
  @Input() items: Array<any> = [];
}
