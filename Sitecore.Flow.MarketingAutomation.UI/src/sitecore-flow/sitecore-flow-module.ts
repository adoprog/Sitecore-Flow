import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReadonlyEditorComponent } from './editor/readonly-editor.component';

@NgModule({
    imports: [
        CommonModule, FormsModule
    ],
    declarations: [ReadonlyEditorComponent],
    entryComponents: [ReadonlyEditorComponent]
})
export class SitecoreFlowModule { }
