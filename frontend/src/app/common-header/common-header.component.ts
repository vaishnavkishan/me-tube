import { Component } from '@angular/core';

@Component({
  selector: 'app-common-header',
  templateUrl: './common-header.component.html',
  styleUrls: ['./common-header.component.scss']
})
export class CommonHeaderComponent {
  title = 'MyTube'; // Static title
  description = 'Welcome to MyTube, a YouTube clone!'; // Static description
}
