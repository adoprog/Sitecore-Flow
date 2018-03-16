import { SingleItem} from '@sitecore/ma-core';

export class SitecoreFlowActivity extends SingleItem {
	
    getVisual(): string {
        const subTitle = 'Send to Microsoft Flow';
        const cssClass = this.isDefined ? '' : 'undefined';
        
        return `
            <div class="viewport-readonly-editor marketing-action ${cssClass}">
                <span class="icon">
                    <img src="/~/icon/Apps/32x32/dna.png" />
                </span>
                <p class="text with-subtitle" title="Sitecore Flow">
                    Sitecore Flow
                    <small class="subtitle" title="${subTitle}">${subTitle}</small>
                </p>
            </div>
        `;
    }

    get isDefined(): boolean {
        return true;
    }
}