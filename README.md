# Sitecore-Flow <img src="https://ci.appveyor.com/api/projects/status/github/adoprog/Sitecore-Flow?svg=true">
<img src="https://github.com/adoprog/Sitecore-Flow/raw/master/wiki/images/connectors.png" width="300">

## About
Sitecore Flow is a free, open source Sitecore / Microsoft Flow connector. 
Connect Sitecore to hundreds of services including:

* Salesforce
* Dynamics
* SharePoint
* Office 365
* Docusign
* and [more](https://flow.microsoft.com/en-us/connectors/)

Let the service providers take care of updating their connectors in the cloud and start integrating new systems with Sitecore without developers help.

Initial release includes custom save action for [Web Forms for Marketers](https://doc.sitecore.net/web_forms_for_marketers) module, which allows you to easily map your web form fields to the actions in Microsoft Flow.

Feel free to request additional features as well  ass add your own recipes via pull requests.

## Installation

Download and install the latest version of Sitecore module [here](https://github.com/adoprog/Sitecore-Flow/releases). 

## Usage

First create the web form (or use a sample one) and add a "Send to Microsoft Flow" action to it. Copy the "Request Body JSON Schema" field value to the clipboard.

![form actions](https://github.com/adoprog/Sitecore-Flow/raw/master/wiki/images/form%20actions.png)

![form actions](https://github.com/adoprog/Sitecore-Flow/raw/master/wiki/images/wffm_dialog.png)

Then [create](https://emea.flow.microsoft.com/en-us/) a blank Microsoft Flow and choose a trigger called "When a HTTP request is received" to it. Paste the text from clipboard to the "Request Body JSON Schema" field

![flow trigger](https://github.com/adoprog/Sitecore-Flow/raw/master/wiki/images/flow%20trigger%20empty.png)

Now add any action (required to get the post URL), save the Flow and copy the "HTTP POST URL" to Sitecore field.

![flow trigger complete](https://github.com/adoprog/Sitecore-Flow/raw/master/wiki/images/flow%20trigger%20complete.png)

That's it! Now all Web Form submits will be posted to Microsoft Flow. With a few clicks you can build the flow for your form.

![dynamics](https://github.com/adoprog/Sitecore-Flow/raw/master/wiki/images/crm%20connector.png)   ![form fields](https://github.com/adoprog/Sitecore-Flow/raw/master/wiki/images/form_fields.png)

Here is the sample with Dynamics, Slack, and custom condition in on Flow:

![flow](https://github.com/adoprog/Sitecore-Flow/raw/master/wiki/images/sitecoreflow.png)

## Features

* Send Sitecore [Web Forms for Marketers](https://doc.sitecore.net/web_forms_for_marketers) form submit data to [Microsoft Flow](https://flow.microsoft.com/)
* Send items to Microsoft Flow using Workflow actions (available in v1.0.1)
* Supports Sitecore 7, 8, 9

## Planned Features

* Send Analytics session data to Microsoft Flow
* Sitecore 9: Forms
* Sitecore 9: Automation
* Rules Engine

## License
MIT License

Copyright (c) 2018 Alexander Doroshenko

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
