import { Component, ChangeDetectionStrategy, ChangeDetectorRef, NgZone } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AppService } from './app.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent {
  employeeForm = new FormGroup({
    name: new FormControl(''),
    address: new FormControl(''),
    image: new FormControl(''),
  });

  public message: any;
  /*
  * Default constructor
  * @param cd
  * @param appService
  */
  constructor(private zone: NgZone, private cd: ChangeDetectorRef, private appService: AppService) {
    this.initDefaultMessage();
  }


  /**
   * method initDefaultMessage
   */
  initDefaultMessage() {
    this.message = { message: '', isSuccess: true };
  }

  /*
  * Method submitForm
  *
  */

  submitForm() {
    this.appService.registerEmployee(this.employeeForm.value).
      subscribe((response: any) => {
        if (response) {
          this.message = { text: 'Form successfully submited.', isSuccess: true };
          setTimeout(this.initDefaultMessage);
          this.cd.detectChanges();
        }
      }, (response: any) => {
        if (response) {
          this.message = { text: 'Form submission failed.', isSuccess: false };
          setTimeout(this.initDefaultMessage);
          this.cd.detectChanges();
        }
      });
  }


  /*
  * Method onFIleChange
  * @param event
  */
  onFileChange(event) {
    let reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.employeeForm.patchValue({
          image: reader.result
        });
        this.cd.markForCheck();
      };
    }
  }
}
