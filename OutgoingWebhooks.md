# Outgoing Webhooks

These are outgoing HTTP requests to an endpoint of a Case Administrator's choosing.  This is configured under Case Settings

## Version 1
This first version of Webhook events fire everytime an Activity is created or updated.

### Options
- HTTP URL to send HTTP POSTS
- optional payload for `Authorization` header for the HTTP URL
- enable or disable the hook

## Notes
- Your URL should be unique per case.  There is no case identifier in the payloads.
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
```
{
    "timestamp": "2021-04-13T08:58:50.232224Z",
    "claimId": "ee10a4b1-97a2-4d0c-a128-674201044eaf",
    "activityId": "1315f728-6753-479d-8436-7212931ae9be",
    "activityTemplateId": "0fe30be6-6ef4-4736-93e1-ac51da3010fc",
    "status": "Submitted",
    "type": "Claim"
}
```

### Case Activity Sample payload
```
{
    "timestamp": "2021-04-13T08:58:50.232224Z",
    "contactId":"8d30cc21-8e21-45df-96a1-47578d7de26c"
    "activityId": "1315f728-6753-479d-8436-7212931ae9be",
    "activityTemplateId": "0fe30be6-6ef4-4736-93e1-ac51da3010fc",
    "status": "Open",
    "type": "Case"
}
```
