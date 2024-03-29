# API Samples and Documentation

This repository contains code samples and notes for our public API. The [API reference](#rest-api-reference) should be considered the primary reference.

## URLs

- <https://api.uk.casefunnel.io> - UK (London)
- <https://api.au.casefunnel.io> - Australia (Sydney)
- <https://api.us.casefunnel.io> - USA (Ohio)
- <https://api.casefunnel.co> - deprecated.  Do Not Use.  Alternate URL for London.
- <https://api.casefunnel.io> - deprecated.  Do Not Use.  Alternate URL for London.

## Getting Started

Refer to our [walk-through guide for common API use cases](use-cases-walkthrough-v2.md) to get started. [Concepts and data](concepts-and-data-v2.md) provides guidance on the resources exposed by the API.

(Deprecated) For an overview of the resources used by API V1, [follow this guide](concepts-and-data-v1.md).

## Rest API Reference

The REST API reference documentation describes the HTTP method, path, and parameters for every operation. It also displays example requests and responses for each operation. We use the Swagger framework to generate this documentation.

- [V2 (Current)](https://api.uk.casefunnel.io/docs/index.html?urls.primaryName=CaseFunnel%20Case%20API%20V2)
- [V1 (Deprecated)](https://api.uk.casefunnel.io/docs/index.html?urls.primaryName=CaseFunnel%20Case%20API%20V1)

## Webhooks

Webhooks are HTTP-callbacks that can be used to notify you of a change to the data we hold. [Follow this guide](outgoing-webhooks.md) for details on what & when webhooks can be configured and the payload that is sent.

Typically customers configure webhooks to notify them of new or updated data then use the payload to query our API for further information.

## Testing with Postman

### Importing an API specification

You can import a specification of our API directly into Postman. Follow this guide on [Importing an API](https://learning.postman.com/docs/designing-and-developing-your-api/importing-an-api/#importing-api-definitions). The link to the API specification you will require are listed below.

- V2 - https://api.casefunnel.io/docs/funnel-v2.json
- V1 - https://api.casefunnel.io/docs/funnel-v1.json

### Importing a Postman collection

Alternatively you can import one of the following JSON Postman collection files. Follow this guide on [Importing a collection](https://learning.postman.com/docs/getting-started/importing-and-exporting-data/#importing-postman-data). Please be aware that these files may become stale, using the method above is a sure fire way to import the latest specification of our APIs.

- V2 - [CaseFunnel-V2.postman_collection.json](CaseFunnel-V2.postman_collection.json) - generated 24/07/2023
- V1 - [CaseFunnel-V1.postman_collection.json](CaseFunnel-V1.postman_collection.json) - generated 24/07/2023
