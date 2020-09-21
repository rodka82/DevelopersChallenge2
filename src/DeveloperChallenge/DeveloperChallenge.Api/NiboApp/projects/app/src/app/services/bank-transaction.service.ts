import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { saveAs } from 'file-saver';
import { Subscription } from 'rxjs';
import { BankTransaction } from '../models/bank-transaction';

const URL_BANKTRANSACTIONS = "http://localhost:51373/api/banktransactions";

@Injectable({
  providedIn: 'root'
})
export class BankTransactionService {

  constructor(private httpClient: HttpClient){}

  getAll(){
    return this.httpClient.get(URL_BANKTRANSACTIONS);
  }
}
