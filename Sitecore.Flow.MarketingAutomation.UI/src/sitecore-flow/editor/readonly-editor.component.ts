import { Component, OnInit, Injector } from '@angular/core';
import {
		EditorBase
    } from '@sitecore/ma-core';

@Component({
    selector: 'readonly-editor',
	template: `
        <section class="content">
            <div class="form-group">
                <div class="row readonly-editor">
                    <label class="col-6 title">HTTP POST URL</label>
                    <div class="col-6">
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <input type="text" class="form-control" [(ngModel)]="triggerAddress"/>
                    </div>
                </div>
                <div class="row">
                    <p></p>
                </div>
                <div class="row readonly-editor">
                    <label class="col-12 title">Request Body JSON Schema</label>
                </div>
                <div class="row">
                    <div class="col-12">
                        <textarea style="width:100%;height:200px;" readonly>
{
    "type": "object",
    "properties": {
        "Email": {
            "type": "string"
        },
        "FirstName": {
            "type": "string"
        },
        "MiddleName": {
            "type": "string"
        },
        "LastName": {
            "type": "string"
        },
        "PreferredLanguage": {
            "type": "string"
        },
        "Title": {
            "type": "string"
        },
        "JobTitle": {
            "type": "string"
        }
    }
}                        
                        </textarea>                    
                    </div>
                </div>
            </div>
        </section>
    `,
    //CSS Styles are ommitted for brevity
    styles: ['']
})

export class ReadonlyEditorComponent extends EditorBase implements OnInit {


    constructor(private injector: Injector) {
        super();
    }

    triggerAddress: Text;
   
    ngOnInit(): void { 
        this.triggerAddress = this.model.triggerAddress;
    }
 
    serialize(): any {
        return {    
            triggerAddress : this.triggerAddress
        };
    }
}