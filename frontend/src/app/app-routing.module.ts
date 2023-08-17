import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ThumbnailsComponent } from './thumbnails/thumbnails.component';
import { VideoPlayerComponent } from './video-player/video-player.component'; // Import the VideoPlayerComponent
import { LayoutComponent } from './layout/layout.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent, // Use LayoutComponent as the base for all pages
    children: [
      { path: '', redirectTo: 'thumbnails', pathMatch: 'full' },
      { path: 'thumbnails', component: ThumbnailsComponent },
      { path: 'play/:videoId', component: VideoPlayerComponent }
    ]
  }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
