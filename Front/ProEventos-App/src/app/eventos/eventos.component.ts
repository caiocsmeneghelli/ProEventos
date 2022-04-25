import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public eventos: any = [
    {
      Tema: "Angular",
      Local: "Belo Horizonte"
    },
    {
      Tema: ".Net 5",
      Local: "São Paulo"
    },
    {
      Tema: "Angular e suas novidades",
      Local: "Rio de janeiro"
    }
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
