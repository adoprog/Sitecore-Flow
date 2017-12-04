# Sitecore-Flow

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

Download and install the latest version of Sitecore module here. 

## Usage

1. First create the web form (or use a sample one) and add a "Send to Microsoft Flow" action to it. Copy the "Request Body JSON Schema" field value to the clipboard.

2. Then create blank Microsoft Flow and a trigger called "When a HTTP request is received" to it. Paste the text from clipboard to the "Request Body JSON Schema" field

3. Now add any action (required to get the post URL), save the Flow and copy the "HTTP POST URL" to Sitecore field.

4. That's it! Now all Web Form submits will be posted to Microsoft Flow. With a few clicks you can build the flow for your form.

## Features

Send Web Forms for Marketers form submit data to Microsoft Flow
Supports Sitecore 7, 8

## Coming features
TODO

## License
MIT License
