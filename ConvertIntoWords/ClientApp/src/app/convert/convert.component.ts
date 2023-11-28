import { Component } from '@angular/core';
import { ConvertService } from './convert.service';

@Component({
  selector: 'app-convert',
  templateUrl: './convert.component.html',
})
export class ConvertComponent {
  result: string;
  isSuccess: boolean;
  numberInput: string;

  constructor(private convertService: ConvertService) { 
    this.result = "";
    this.isSuccess = true;
    this.numberInput = "";
  }

  convertToWords() {
    console.log(this.numberInput);

    const numericValue = parseFloat(String(this.numberInput).replace(/,/g, '.').replace(/\s/g, ''));
    if (!isNaN(numericValue) && numericValue >= 0 && numericValue <= 999999999.99) {
      this.convertService.convertNumberToWords(numericValue)
      .subscribe({
        next: (response) => {
          this.result = response.result;
          this.isSuccess = true;
        },
        error: (error) => {
          this.result = "We apologize, but it seems that something went wrong";
          this.isSuccess = false;
          console.log("Error: " + error.message);
        }
      });
    } else {
      this.result = 'Please enter a valid number between 0 and 999999999,99';
      this.isSuccess = false;
    }
  }


  validateInput(event: any) {
    let input = event.target.value;

    input = input.replace(/[^0-9,]/g, '');

    input = input.replace(/,+/g, ',');

    if (input.charAt(0) === ',') {
      input = input.substr(1);
    }

    const parts = input.split(',');
    if (parts[1] && parts[1].length > 2) {
      parts[1] = parts[1].substring(0, 2);
      input = parts.join(',');
    }

    event.target.value = input;
    this.numberInput = input;
  }

}

