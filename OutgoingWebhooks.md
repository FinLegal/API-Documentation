# Outgoing Webhooks

These are outgoing HTTP requests to an endpoint of a Case Administrator's choosing.  This is configured under Case Settings

## Version 1

This first version of Webhook events fire every time an Activity is created or updated.

### Options

- HTTP URL to send HTTP POSTS
- optional payload for `Authorization` header for the HTTP URL
- enable or disable the hook

## Notes

- Your URL should be unique per case.  There is no case identifier in the payloads.
- A 200-level response is expected.  Otherwise, it is considered an error and will be retried.
- A event POST is only retried once.
- Types:
- ActivityChange
  - ClaimAttributeChange
  - CaseAttributeChange
  - ContactAttributeChange
  - ActivityAttributeChange
  - ClaimStatusChange
        
## Activity Change
- Activities have two types: Claim and Case with slightly different payloads.  Both need to be handled.
- `timestamp` is always GMT and in ISO-8601 format.
- Valid values for `status`
  - Unset
  - Open,
  - Submitted,
  - UnderReview,
  - Accepted,
  - Rejected,
  - Deleted,
  - PartiallyAccepted

### Claim Activity Sample payload

``` json
{
    "timestamp": "2021-04-13T08:58:50.232224Z",    
    "caseId": "8d30cc21-8e21-45df-96a1-47578d7de26c",
    "claimId": "ee10a4b1-97a2-4d0c-a128-674201044eaf",
    "activityId": "1315f728-6753-479d-8436-7212931ae9be",
    "activityTemplateId": "0fe30be6-6ef4-4736-93e1-ac51da3010fc",
    "status": "Submitted",
    "type": "Claim"
}
```

### Case Activity Sample payload

``` json
{
    "timestamp": "2021-04-13T08:58:50.232224Z",
    "caseId": "8d30cc21-8e21-45df-96a1-47578d7de26c",
    "contactId":"8d30cc21-8e21-45df-96a1-47578d7de26c",
    "activityId": "1315f728-6753-479d-8436-7212931ae9be",
    "activityTemplateId": "0fe30be6-6ef4-4736-93e1-ac51da3010fc",
    "status": "Open",
    "type": "Case"
}
```

## Attribute Changes

### Claim Attribute Change Sample payload

``` json
{
    "timestamp": "2021-04-13T08:58:50.232224Z",
    "caseId": "8d30cc21-8e21-45df-96a1-47578d7de26c",
    "claimId": "5bd3c355-4221-41a3-a0bf-564a5944a374",
    "attributeId": "a7167f2d-6ae5-4417-b9d4-a42c21d3b951",
    "type": "ClaimAttribute"
}
```

### Contact Attribute Updated Sample payload

``` json
{
    "timestamp": "2021-04-13T08:58:50.232224Z",
    "contactId":"8d30cc21-8e21-45df-96a1-47578d7de26c",
    "companyId": "886d87ff-eedb-4f40-8d05-e50bfc8ae346",
    "attributeId": "d80b61a9-2066-498a-9803-2b6ab77a5f77",
    "type": "ContactAttribute"
}
```

### Case Attribute Updated Sample payload

``` json
{
    "timestamp": "2021-04-13T08:58:50.232224Z",
    "caseId": "8d30cc21-8e21-45df-96a1-47578d7de26c",
    "contactId": "8d30cc21-8e21-45df-96a1-47578d7de26c",
    "attributeId": "d80b61a9-2066-498a-9803-2b6ab77a5f77",
    "type": "CaseAttribute"
}
```

### Activity Attribute Updated Sample payload

``` json
{
    "timestamp": "2021-04-13T08:58:50.232224Z",
    "caseId": "8d30cc21-8e21-45df-96a1-47578d7de26c",
    "activityId": "d80b61a9-2066-498a-9803-2b6ab77a5f77",
    "attributeId": "fd54fc38-8703-4640-902a-c98b8c3c6712",
    "type": "ActivityAttribute"
}
```

## Claim Status Changes

### Claim Status Changed Sample payload

``` json
{
    "timestamp":"2021-09-08T14:22:20.361415Z",
    "caseId": "8d30cc21-8e21-45df-96a1-47578d7de26c",
    "claimId":"73b5c8d9-2810-4799-a405-1c99d3cd658e",
    "claimStatusId":"61c813df-071f-430f-9d8c-37c75abdc0fd",
    "claimStatusName":"Provided technical fix"
}
```
