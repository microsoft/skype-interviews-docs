# Retrieve interview data

If your application wants to leverage all the information that Skype Interviews can provide after an interview like the interviewer `notes`, `feedback` and the candidate's written code `snapshots`, then you can request the data with a simple HTTP request.

To retrieve information such as "notes", "feedback" and "code snapshots", we just need to issue a HTTP `GET` request to `https://interviews.skype.com/api/interviews/YOUR_INTERVIEW_CODE`. 

**note:** for the `GET` request please omit the `sub` property in the `JWT` token.

Here's an example request to retrieve the information.

**Request**
```
GET /api/interviews/YOUR_INTERVIEW_CODE HTTP/1.1 
Host: interviews.skype.com 
Authorization: Bearer <YOUR_TOKEN_WITHOUT_SUB> 
Content-Type: application/json 
```

In the response you'll see everything from the interview. Some data might have a delayed for up to 30 seconds.