# Modify interviews

In our previous section we explored how to `GET` interview data. The object that is returned can be freely modified. To update the interview object, like changing its `stage` or changing the participants, it is as simple as using a `PUT` request on the `/interviews/YOUR_INTERVIEW_CODE` endpoint.

## Change the interview stage
Here's an example request to archive an interview. You can archive or cancel interviews by changing the interview stage. Please refer to our `webhooks` sections for the full list of interview stages.

**Request**
```http
PUT /api/interviews/YOUR_INTERVIEW_CODE HTTP/1.1 
Host: interviews.skype.com 
Authorization: Bearer <YOUR_TOKEN> 
Content-Type: application/json 

{
  "stage": {
    "value": "InterviewArchived"
  }
}
```

**Response**
```json
{
  "urls": [
    {
      "url": "https://interviews.skype.com/interviews?code=YOUR_INTERVIEW_CODE",
      "type": "Interview",
      "participantType": null,
      "participant": "all"
    }
  ],
  ...,
  "stage": {
    "value": "InterviewArchived",
    "metadata": "10000 - Interview was archived"
  },
  ...
}
```
