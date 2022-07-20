# Use Case Walkthrough

Note: The below use case examples are relevant for API v2 ONLY.

Here are a few common scenarios that consumers of our API typically implement. The aim here is to provide examples of the order in which API calls are to  made to satisfy basic use cases. As an integrator you will work with a FinLegal customer to define the use case(s) before implementing any API calls.

For the purposes of this walkthrough we will use the following fictitious claim:

* Our claim contains four activities
* Activities are configured in a workflow where: Activity 1 -> Activity 2 -> Activity 3
* Activity 4 is assigned, by a case handler, as and when it is required and is not assigned through workflow

**Note:** All id's (uuid's) shown in the examples below are fictitious. And also be aware id's are environment & claim specific.

## Use Case: Capturing basic claimant information & redirecting them to step 1 of the claims process

*Expectation:* You have captured some basic information about the client (email & name) & now wish to refer them to a claims site so they complete the remaining three activities of the claim.

1. Create a Contact: POST request to `/funnel/v2/contacts?isAdministrator=true` **Note:** A Contact should by default be an administrator. 
2. Create a Claim: POST request to `/funnel/v2/claims`
3. Create Activity 1: POST request to: `/funnel/v2/activities?claimId={claimId}&contactId={contactId}` **Note:** Either create a Contact OR a Claim activity.
4. Create Magic Link for redirect: POST request to: `/funnel/v2/contacts/{contactId}/magic-link`
You will receive a response containing a redirect url which, when followed, will enable the client to begin the claims process at the first Open activity

### Additional items to consider

- In the response to the first POST (1.) you will receive a `contactID`. This `contactID` must be used to create a claim (2.). 
- In the response to the second POST (2.) you will receive a `claimID`. 
- You may want to save both `contactID` and `claimID`s to your record system if you wish to push more data into FinLegal or if you need to track activity completion by the claimant (via e.g. webhooks or GTM). 
- The `claimId` can also be identified using the following API endpoints:

* `GET /funnel/v2/contacts/by-email/{email}`
* `GET /funnel/v2/contacts/by-phone/{phone}`

## Use Case: Creating a client & redirecting them to step 3 of the claims process

*Expectation:* You have captured personal details about a claimant and details about the claim to satisfy activities 1 & 2 of the claim. You have agreed with a FinLegal customer that the client will resume at activity 3.

1. Create a Contact: POST request to `/funnel/v2/contacts`
2. Create a Claim: POST request to `/funnel/v2/claims`, keep hold of the response and do NOT redirect the client at this point.
3. Create Magic Link for redirect: POST request to: `/funnel/v2/contacts/{contactId}/magic-link`. Keep hold of this URL - you will not need it quite yet.
4. Capture Activity template IDs: GET request to `/funnel/v2/activity-templates`. Keep hold of this response as you will need to refer to it several times in the following steps. **Note:** Some data properties have been removed for brevity.

        [
          {
            "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "name": "Activity 1",
            "activityAttributeTemplates": [
              {
                "id": "8c31c71a-0959-4926-9e0e-2cbcbf1cb574",
                "name": "Attribute 1-1"
              },
              {
                "id": "c9634a59-a14c-4554-b1c4-a1569b740739",
                "name": "Attribute 1-2"
              }
            ]
          },  
          {
            "id": "f24044b5-3ba5-4356-a3c0-301bdd4f9379",
            "name": "Activity 2",
            "activityAttributeTemplates": [
              {
                "id": "22697147-5af4-43a8-8719-fecbc5eb083e",
                "name": "Attribute 2-1"
              },
              {
                "id": "f6322b5b-387a-4203-af32-09eac9a92859",
                "name": "Attribute 2-2"
              }
            ]
          },  
          {
            "id": "04f907e7-07b7-4c13-92f9-d7501b9936c4",
            "name": "Activity 3",
            "activityAttributeTemplates": [
              {
                "id": "bbf96bc0-2d2e-476e-99f6-56667e89881a",
                "name": "Attribute 3-1"
              },
              {
                "id": "571432b8-4016-42f3-a337-4017d6dd0e87",
                "name": "Attribute 3-2"
              }
            ]
          },  
          {
            "id": "a0dfc61e-ace2-4f30-9547-642aaf1a9e48",
            "name": "Activity 4",
            "activityAttributeTemplates": [
              {
                "id": "2e9855d0-ea3f-4fbe-b5f4-b9030e4a90ef",
                "name": "Attribute 4-1"
              },
              {
                "id": "cfa1882a-4577-4666-aab3-a9e210f156ec",
                "name": "Attribute 4-2"
              }
            ]
          }
        ]

4. Create and/or populate activities: POST request to `/funnel/v2/activities`. To enable the client to begin at activity 3 we require you to create an activity where the status is set to Open. Example request body:

        {
          "activityTemplateId": "04f907e7-07b7-4c13-92f9-d7501b9936c4",
          "status": "Open"
        }
    **Note**:  `activityTemplateId` is set to the `id` for activity 3 received in the previous step.

5. Redirect the client using the redirect url you received in step 3. Now we require to you "back-fill" activities 1 & 2 so that case handlers can refer to this data in the Case Funnel dashboard. We recommend making the following requests after you have redirected the client for the best client experience as this means you are not keeping the client waiting unnecessarily.

6. POST request to `/funnel/v2/activities`. Making this request will enable you to create activity 1 in CaseFunnel. As you are back-filling an activity you will need to also include any attributes as per the example below. CaseFunnel requires that you use the Submitted status to indicate this is a completed activity. Example request body:

        {
          "activityTemplateId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          "status": "Submitted",
          "attributes": [
            {
              "activityAttributeTemplateId": "8c31c71a-0959-4926-9e0e-2cbcbf1cb574",
              "stringValue": "Lorem ipsum dolor"
            },
            {
              "activityAttributeTemplateId": "c9634a59-a14c-4554-b1c4-a1569b740739",
              "stringValue": "Dolor ipsum"
            }
          ],
        }

    **Note**: `activityAttributeTemplateId` is set to the `id` for the attribute you wish to set. The attribute is listed in the `activityAttributeTemplates` section of the response received in step 2. See the attributes: Attribute 1-1 & Attribute 1-2.

7. POST request to `/funnel/v2/activities`. Now you will need to back-fill activity 2 in the same way as you did activity 1 in the previous step. Example request body:

        {
          "activityTemplateId": "f24044b5-3ba5-4356-a3c0-301bdd4f9379",
          "status": "Submitted",
          "attributes": [
            {
              "activityAttributeTemplateId": "22697147-5af4-43a8-8719-fecbc5eb083e",
              "dateTimeValue": "2021-04-13T06:44:42.322Z",
            },
            {
              "activityAttributeTemplateId": "f6322b5b-387a-4203-af32-09eac9a92859",
              "stringValue": "2017-09-08T19:01:55.0+03:00"
            }
          ],
        }
