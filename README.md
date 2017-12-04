# Sitecore-Flow
<img src="https://github.com/adoprog/Sitecore-Flow/raw/master/wiki/images/connectors.png" width="300">

## About
Sitecore Flow is a free, open source Sitecore / Microsoft Flow connector. 
Connect Sitecore to hundreds of services including:

* Salesforce
* Dynamics
* SharePoint
* Docusign
* and [more](https://flow.microsoft.com/en-us/connectors/)

Let the service providers take care of updating their connectors in the cloud and start integrating new systems with Sitecore without developers help.

Initial release includes custom save action for Web Forms for Marketers module, which allows you to easily map your web form fields to the actions in Microsoft Flow.

Feel free to request additional features as well  ass add your own recipes via pull requests.

## Installation

Download and install the latest version of Sitecore module [here](https://github.com/adoprog/Sitecore-Flow/releases). 

## Usage

1. First create the web form (or use a sample one) and add a "Send to Microsoft Flow" action to it. Copy the "Request Body JSON Schema" field value to the clipboard.
![form actions](https://github.com/adoprog/Sitecore-Flow/raw/master/wiki/images/form%20actions.png)
![form actions](https://github.com/adoprog/Sitecore-Flow/raw/master/wiki/images/wffm_dialog.png)

2. Then create blank Microsoft Flow and a trigger called "When a HTTP request is received" to it. Paste the text from clipboard to the "Request Body JSON Schema" field
![flow trigger](https://github.com/adoprog/Sitecore-Flow/raw/master/wiki/images/flow%20trigger%20empty.png)

3. Now add any action (required to get the post URL), save the Flow and copy the "HTTP POST URL" to Sitecore field.
![flow trigger complete](https://github.com/adoprog/Sitecore-Flow/raw/master/wiki/images/flow%20trigger%20complete.png)

4. That's it! Now all Web Form submits will be posted to Microsoft Flow. With a few clicks you can build the flow for your form.
![dynamics](https://github.com/adoprog/Sitecore-Flow/raw/master/wiki/images/crm%20connector.png)
![form fields](https://github.com/adoprog/Sitecore-Flow/raw/master/wiki/images/form_fields.png)

## Features

Send Sitecore [Web Forms for Marketers form](https://doc.sitecore.net/web_forms_for_marketers) submit data to [Microsoft Flow](https://flow.microsoft.com/)
Supports Sitecore 7, 8

## Coming features
TODO

## License
MIT License
