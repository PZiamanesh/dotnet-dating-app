import {Component, inject, Inject, OnInit} from '@angular/core';
import {MemberService} from '../../_services/member.service';
import {Member} from '../../_models/member';
import {MemberCardComponent} from '../member-card/member-card.component';
import {PaginationComponent} from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [
    MemberCardComponent,
    PaginationComponent
  ],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {
  protected memberSrv = inject(MemberService);
  members: Member[] = [];
  pageNumber = 1;
  pageSize = 10;
  
  ngOnInit(): void {
    if (!this.memberSrv.paginatedResult()) {
      this.loadMembers();
    }
  }

  loadMembers() {
    this.memberSrv.getMembers(this.pageNumber, this.pageSize);
  }

  pageChanged(event: any) {
    if (this.pageNumber !== event.page) {
      this.pageNumber = event.page;
      this.loadMembers();
    }
  }
}
