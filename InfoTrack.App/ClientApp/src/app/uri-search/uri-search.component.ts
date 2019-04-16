import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormControl, FormGroup, Validators  } from '@angular/forms';

@Component({
  selector: 'app-uri-search',
  templateUrl: './uri-search.component.html'
})

export class UriSearchComponent {

  exampleForm: FormGroup = new FormGroup({
    keyword: new FormControl(),
    findUri: new FormControl(),
  });

  public searchHistory: SearchHistory[];

  private _baseUrl;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private formBuilder: FormBuilder
  ){
      this._baseUrl = baseUrl;

      this.createForm();

      http.get<SearchHistory[]>(baseUrl + 'api/Search/History').subscribe(result => {
        this.searchHistory = result;
      }, error => console.error(error));
  }

  createForm() {
    this.exampleForm = this.formBuilder.group({
      keyword: ['', Validators.required],
      findUri: ['', Validators.required]
    });
  }

  submit()
  {
    if (!this.exampleForm.valid) {
        //TODO show validation message
        return;
      }

    // TODO look for a better way to build the query
    let queryUrl = this._baseUrl + 'api/Search/FindUri?keyword=' + this.exampleForm.value.keyword + "&findUri=" + this.exampleForm.value.findUri;

    this.http.get<SearchHistory>(queryUrl).subscribe(result => {
      debugger;
      this.searchHistory.concat(result);
    }, error => console.error(error));
  }
}

interface SearchHistory {
  searchDate: string;
  url: number;
  keyword: string;
  matchEntries: string;
}
