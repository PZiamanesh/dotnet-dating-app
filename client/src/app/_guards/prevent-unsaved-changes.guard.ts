import { CanDeactivateFn } from '@angular/router';
import {MemberEditComponent} from '../members/member-edit/member-edit.component';

export const preventUnsavedChangesGuard: CanDeactivateFn<MemberEditComponent> = (component, currentRoute, currentState, nextState) => {
  if (component.memberEditForm?.dirty) {
    return confirm('Any unsaved changes will be lost. Are you sure?');
  }
  return true;
};
