import { PopupButton } from "./popup-button";

export class PopupProperties {
  public title: string;
  public message: string;
  public actions: PopupButton[];
  public width?: string;
  public height?: string;
  public result?: any;
  public data?: any;

  constructor(title: string, message: string, width?: string, height?: string) {
    this.title = title;
    this.message = message;
    this.actions = [];
    this.width = width;
    this.height = height;
  }
}
