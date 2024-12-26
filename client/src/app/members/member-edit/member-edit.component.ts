import {Component, inject, OnInit, ViewChild} from '@angular/core';
import {Member} from '../../_models/member';
import {AccountService} from '../../_services/account.service';
import {MemberService} from '../../_services/member.service';
import {GalleryComponent} from 'ng-gallery';
import {TabDirective, TabsetComponent} from 'ngx-bootstrap/tabs';
import {FormsModule, NgForm} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [
    GalleryComponent,
    TabDirective,
    TabsetComponent,
    FormsModule
  ],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit {
  member?: Member;
  private accountSrv = inject(AccountService);
  private memberSrv = inject(MemberService);
  private toastrSrv = inject(ToastrService);
  @ViewChild('profileForm') memberEditForm?: NgForm;

  ngOnInit() {
    this.loadMember();
  }

  loadMember() {
    const username = this.accountSrv.currentUser()?.username;
    if (!username) return;

    this.memberSrv.getMember(username).subscribe({
      next: (result) => {
        this.member = result;
      }
    });
  }

  updateMember() {
    this.memberSrv.updateMember(this.memberEditForm?.value).subscribe({
      next: _ => {
        this.toastrSrv.success('profile updated successfully');
        this.memberEditForm?.reset(this.member);
      }
    })
  }
}
