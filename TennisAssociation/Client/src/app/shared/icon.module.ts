import { MatIconModule } from '@angular/material/icon';
import { ChangeDetectorRef, Directive, HostBinding, Input, NgModule } from "@angular/core";
import { MatIconRegistry } from "@angular/material";
import { HttpClientModule } from '@angular/common/http';
import { DomSanitizer } from '@angular/platform-browser';

const ICONS = {
  arrowBack: '../../assets/icons/chevron.svg',
}

@Directive({
  selector: 'mat-icon, mat-icon[appearance], [appearance], app-icon',
})
export class MatIconComponent {
  @HostBinding('class.mat-appearance-filled') public filled: boolean;
  @HostBinding('class.mat-appearance-outline') public outline: boolean;
  @HostBinding('class.mat-appearance-solid') public solid: boolean;
  @HostBinding('attr.icon') public icon: string;

  @HostBinding('attr.size') @Input() size: number;
  @HostBinding('class.mat-icon-disabled') @Input() disabled: boolean = false;

  @Input() set appearance(appearance) {
    if (['filled', 'outline', 'solid'].includes(appearance)) {
      this[appearance] = true;
    }
  }

  @HostBinding('style.color') _color: string;

  @Input() set color(color: string) {
    this._color = color;
  }

  @Input() set svgIcon(icon) {
    this.icon = icon;
    this.matIconRegistry.getNamedSvgIcon(icon).subscribe(svg => {
      const defaultColor = svg.getAttribute('color');
      if (defaultColor) {
        svg.setAttribute('color', 'currentColor');
        this.color = defaultColor;
        this.cdr.markForCheck();
      }
    });
  }

  constructor(
    private matIconRegistry: MatIconRegistry,
    private cdr: ChangeDetectorRef
  ) {}
}

@NgModule({
  declarations: [MatIconComponent],
  imports: [MatIconModule, HttpClientModule],
  exports: [MatIconModule, HttpClientModule, MatIconComponent],
})
export class IconModule {
  constructor(matIconRegistry: MatIconRegistry, domSanitizer: DomSanitizer) {
    Object.keys(ICONS).forEach(name =>
      matIconRegistry.addSvgIcon(
        name,
        domSanitizer.bypassSecurityTrustResourceUrl(ICONS[name])
      )
    );
  }
}