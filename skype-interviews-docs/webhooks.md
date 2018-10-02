# Webhooks

You can configure the webhook URL in our [API Settings](https://interviews.skype.com/api/get-started). Your webhook URL must be using HTTPS and has to respond within 30 seconds.

## Sample event

When you first create a Skype Interview your webhook should receive a request with the following body:

```json
{
  "previousStage": {
    "value": "InterviewCreated",
    "metadata": "1 - Interview was created"
  },
  "currentStage": {
    "value": "SchedulingTime",
    "metadata": "2000 - Pending interviewer time selection"
  },
  "event": "State",
  "timestamp": "2018-02-26T17:50:17.4862382Z",
  "interviewCode": "1c519c56-1712-1e89-1c3e-743ecbedd117"
}
```

Now using the `interviewCode` identifier you can fetch interview data such as the scheduled time or the notes as described in the previous section.

### Interview Stage property table
An interview goes through multiple stages and with every request we send you the `previousStage` and the `currentStage` to allow you to track an interview in a chronological order.

Here's the list of all stages an interview could go through.

|Stage name|Description|
|:-:|-|
|`InterviewCreated`|When a recruiter creates a new interview.|
|`InterviewCanceled`|When the recruiter cancels the interview.|
|`InterviewArchived`|When the recruiter archives the interview.|
|`SchedulingTime`|When either the candidate or interviewer is asked to propose or select a time slot. You can identify the role and action through the `metadata` variable.|
|`SchedulingFailed`|When an error occurs during the interview scheduling process.|
|`SchedulingCompleted`|When the interview has been scheduled and a time has been set.|
|`InterviewStarted`|When the interview has started and at least two participants joined the call.|
|`InterviewFailed`|When an error occured during the interview.|
|`InterviewCompleted`|When the a participant has clicked on the "End interview" button after `InterviewStarted` fired.|
|`FeedbackCollecting`|When the interview has ended and is awaiting the interviewer to submit feedback.|
|`FeedbackFailed`|When an error occured while submitting the feedback.|
|`FeedbackCompleted`|When the interviewer submitted feedback.|

## Verify the webhook request

Every webhook request also contains an `Authorization` header which you can use to verify that the call actually came from Skype Interviews. 

We recommend using a library from [jwt.io](https://jwt.io) to verify the token. If you choose to verify it manually, here are the 4 things to check before you can fully trust the request.

1. Verify the signature
```js
var received_token = REQUEST.headers['Authorization'].replace("Bearer ", "")
var splits = received_token.split('.')

var signature = HMACSHA256( splits[0] + "." + splits[1],   API_SECRET)
var new_token = splits[0] + "." + splits[1] + "." signature

assert(received_token === new_token);
```

2. Verify that the token is issued from your company
```js
var payload = Base64decode(splits[1])
assert(API_KEY === payload.iss)
```

3. Verify that the token didn't expire
```js
var payload = Base64decode(splits[1])
assert(Date.now() > payload.iat)
assert(Date.now() < payload.exp)
```

4. Verify that event payload (the HTTPS request's body) is not manipulated
```js
var payload = Base64decode(splits[1])
assert(sha256(REQUEST.body) === payload.sub)
```

## NodeJS sample
[![Remix on Glitch](assets/remix_button.svg)](https://glitch.com/edit/#!/skype-interviews-webhook-sample?path=README.md:1:0)

Try our sample that's hosted on Glitch. Just remix the code and follow the instructions in the Readme. All you need to do is to copy and paste the API Key and Secret and supply us the Glitch project URL. Then you'll see event changes of your Skype Interviews live on the Glitch URL.
