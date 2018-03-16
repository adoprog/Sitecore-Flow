import { Plugin } from '@sitecore/ma-core';
import { SitecoreFlowActivity } from './sitecore-flow/sitecore-flow-activity';
import { SitecoreFlowModuleNgFactory } from '../codegen/sitecore-flow/sitecore-flow-module.ngfactory';
import { ReadonlyEditorComponent } from '../codegen/sitecore-flow/editor/readonly-editor.component';

// Use the @Plugin decorator to define all the activities the module contains.
@Plugin({
    activityDefinitions: [
        {
            // The ID must match the ID of the activity type description definition item in the CMS.
            id: '5017a646-29ee-4859-bbe8-8b8cdb62fa94', 
            activity: SitecoreFlowActivity,
            editorComponenet: ReadonlyEditorComponent,
            editorModuleFactory: SitecoreFlowModuleNgFactory
        }
    ]
})
export default class SitecoreFlowPlugin {}
