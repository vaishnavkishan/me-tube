import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-thumbnails',
  templateUrl: './thumbnails.component.html',
  styleUrls: ['./thumbnails.component.scss']
})
export class ThumbnailsComponent implements OnInit {
  thumbnailData: ThumbnailData[] = [];

  constructor(private http: HttpClient, private route: ActivatedRoute) { }


  ngOnInit(): void {
    this.http.get<string[]>('http://localhost:5213/api/Thumbnails')
      .subscribe(data => {
        this.thumbnailData = data.map(url => {
          const videoId = this.extractVideoIdFromUrl(url);
          return {
            thumbnailUrl: url,
            title: 'Video Title', // You can set dynamic title if available
            channel: 'Channel Name', // You can set dynamic channel if available
            description: 'Video description goes here.', // You can set dynamic description if available
            videoId: videoId
          };
        });
      });
  }
  private extractVideoIdFromUrl(url: string): string {
    // Extract the last segment of the URL as videoId
    const segments = url.split('/');
    return segments[segments.length - 1];
  }
}

interface ThumbnailData {
  thumbnailUrl: string;
  title: string;
  channel: string;
  description: string;
  videoId: string;
}