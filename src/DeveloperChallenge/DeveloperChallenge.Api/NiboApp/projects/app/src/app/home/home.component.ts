import { Component, OnInit, ViewChild } from '@angular/core';
import { BankTransactionService } from "projects/app/src/app/services/bank-transaction.service";
import { BankTransaction } from "projects/app/src/app/models/bank-transaction";
import { UploadComponent } from "projects/app/src/app/upload/upload.component";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers:  [ 
    BankTransactionService
   ]
})
export class HomeComponent implements OnInit {

  @ViewChild('upload', {static: true}) upload: UploadComponent;

  transactions: BankTransaction[];
  constructor(private bankTransactionService: BankTransactionService) {
    this.transactions = [];

   }

  ngOnInit(): void {
    this.loadBankTransactions();
  }

  public loadBankTransactions(){
    this.bankTransactionService.getAll().subscribe((result: BankTransaction[])=>{
      this.transactions = result;
    });
  }
}
