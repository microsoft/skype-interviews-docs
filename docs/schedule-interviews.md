In many cases defining the date and time for interview suitable for all of the candidates is a cumbersome task. Skype Interviews can help with that by automatically sending emails to the participants and guiding them in picking the right date & time.

# List of capabilities for scheduling interviews
|Property |	Description	| Data type |	Default value |
|---------|-------------|---|---|
|**start** |	Date and time when interview is scheduled to start | Date | `undefined` |
|**duration** | Duration of the interview in minutes | Integer | `undefined` |
|**mode** | What is the scheduling mode - manual entry or automatic scheduling? | String - `"manual"` or `"automatic"` | `"manual"` |
|**dateproposing** | Which one of the meeting participants picks the set of dates to be offered to other side? | String - `"candidate"` or `"interviewer"` | `"interviewer"`|

# Schedule an interview at an agreed date and time

Normally, the Skype Interviews don't have a fixed date or duration. If you know the interview's date and time, you can optionally set it to make sure that the interview link is only accessible 10 minutes before and after the actual interview.

You can setup scheduling related values by providing the `scheduling` data object. Provide `date` as a JavaScript Date object and `duration` in minutes as an integer.

**Request**
```
POST /api/interviews HTTP/1.1
Host: interviews.skype.com
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
	"scheduling": {
        "start": "2019-12-23 14:00",
        "duration": 45
	}
}
```

**Response**
```
{
    ...,
    "scheduling": {
        "start": "2019-12-23T14:00:00",
        "duration": 45,
        "mode": null,
        "dateProposing": null
    },
    ...
}
```

# Leverage Skype Interviews' automatic scheduling capabilities

For automatic scheduling, we give you the ability to determine who proposes the time: the interviewer or the candidate. By providing the `participants` information, you are also protecting the interview link from being accessed by others. The interview links are now unique to the participants's email. 

*Note: You can leverage the access protection also without using our scheduling solution.*

Here is an example of automatic scheduling of a 90-minute interview where the candidate proposes one or many time slots and then interviewers gets to pick a suitable slot.

**Request**
```
POST /api/interviews HTTP/1.1
Host: interviews.skype.com
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
   "participants" : [
       { "name": "Harvey Pitman", "email": "harveypitman@outlook.com", "role": "candidate" },
       { "name": "Louis Dingle", "email": "louis@contoso.com", "role": "interviewer" }
   ],
   "scheduling" : {
      "duration": 90,
      "mode": "automatic",
      "dateproposing": "candidate" 
   }
}
```

**Response**
```
{
    ...,
    "participants": [
        {
            "name": "Harvey Pitman",
            "email": "harveypitman@outlook.com",
            "role": "Candidate"
        },
        {
            "name": "Louis Dingle",
            "email": "louis@contoso.com",
            "role": "Interviewer"
        }
    ],
    "scheduling": {
        "start": null,
        "duration": 90,
        "mode": "automatic",
        "dateProposing": "Candidate"
    },
    ...,
    "urls": [
        {
            "url": "https://interviews.skype.com/interviews?code=649b56ce-fb4e-4eeb-a558-7fae286b45d5-26527&accessCode=67847",
            "type": "Interview",
            "participantType": "Recruiter",
            "participant": "linda@contoso.com"
        },
        {
            "url": "https://interviews.skype.com/interviews?code=649b56ce-fb4e-4eeb-a558-7fae286b45d5-26527&accessCode=50854",
            "type": "Interview",
            "participantType": "Candidate",
            "participant": "harveypitman@outlook.com"
        },
        {
            "url": "https://interviews.skype.com/scheduler?code=649b56ce-fb4e-4eeb-a558-7fae286b45d5-26527&accessCode=04188",
            "type": "Scheduling",
            "participantType": "Candidate",
            "participant": "harveypitman@outlook.com"
        },
        {
            "url": "https://interviews.skype.com/interviews?code=649b56ce-fb4e-4eeb-a558-7fae286b45d5-26527&accessCode=60103",
            "type": "Interview",
            "participantType": "Interviewer",
            "participant": "louis@contoso.com"
        },
        {
            "url": "https://interviews.skype.com/feedback?code=649b56ce-fb4e-4eeb-a558-7fae286b45d5-26527&accessCode=60132",
            "type": "Feedback",
            "participantType": "Interviewer",
            "participant": "louis@contoso.com"
        },
        {
            "url": "https://interviews.skype.com/scheduler?code=649b56ce-fb4e-4eeb-a558-7fae286b45d5-26527&accessCode=47100",
            "type": "Scheduling",
            "participantType": "Interviewer",
            "participant": "louis@contoso.com"
        }
    ],
    ...,
}
```
Notice that you have a list of different URLs now in the response. There are different types of URLs. 

The `type` of the URL tells you what it's used for. 

|URL `type`|Description|
|-|-|
|`Interview`| Url for conducting the actual interview itself|
|`Scheduling`| Url for candidate / interviewer to propose or select scheduling time slots|
|`Feedback`| Url for interviewer to submit feedback which will be visible in the administrator dashboard|

# Setup time zones for scheduling

If you want to pre-define the `timezone` of the user, then you can supply it in the `participants` object as well. All timezone values are in the IANA format which is listed in [this Wikipedia article](https://en.wikipedia.org/wiki/List_of_tz_database_time_zones) under the "TZ*" column.

**Request**
```
POST /api/interviews HTTP/1.1
Host: interviews.skype.com
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
    "participants" : [{
       "name": "Harvey Pitman",
       "email": "harveypitman@outlook.com",
       "role": "candidate",
       "timezone": "Asia/Shanghai"
    }, {
       "name": "Louis Dingle",
       "email": "louis@contoso.com",
       "role": "interviewer",
       "timezone": "US/Pacific"
    }],
    "scheduling" : {
      "duration": 90,
      "mode": "automatic",
      "dateproposing": "candidate" 
    }
}
```


# Schedule interviews without Skype sending the emails
By default if you schedule an interview, we send an email to the participants to notify them of their interview. If you want to send out the URLs on your own, you can just disable the `emails` capabilitiy in the `capabilities` object.

Let's assume that I want to schedule a 60-minute interview on the 5th Decemeber 2017 at 14:00 and want to manage the URLs on my own. 

**Request**
```
POST /api/interviews HTTP/1.1
Host: interviews.skype.com
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
    "capabilities": {
        "emails": false
    },
    "scheduling" : {
        "start": "2017-12-05 14:00", 
        "duration": 60 
    },
    "participants" : [
        { 
            "name": "Harvey Pitman", 
            "email": "harveypitman@outlook.com", 
            "role": "Candidate" 
        },
        { 
            "name": "Louis Dingle",
            "email": "louis@contoso.com",
            "role": "Interviewer" 
        }
    ]
}
```

**Response**
```
{
    "capabilities": {
        ...,
        "emails": false
        ...,
    }
    ...,
    "participants": [
        {
            "name": "Harvey Pitman",
            "email": "harveypitman@outlook.com",
            "role": "Candidate"
        },
        {
            "name": "Louis Dingle",
            "email": "louis@contoso.com",
            "role": "Interviewer"
        }
    ],
    "scheduling": {
        "start": "2017-12-05T14:00:00",
        "duration": 60,
        ...,
    },
    ...,
    "urls": [
        {
            "url": "https://interviews.skype.com/interviews?code=649b56ce-fb4e-4eeb-a558-7fae286b45d5-26527&accessCode=67847",
            "type": "Interview",
            "participantType": "Recruiter",
            "participant": "linda@contoso.com"
        },
        {
            "url": "https://interviews.skype.com/interviews?code=649b56ce-fb4e-4eeb-a558-7fae286b45d5-26527&accessCode=50854",
            "type": "Interview",
            "participantType": "Candidate",
            "participant": "harveypitman@outlook.com"
        },
        {
            "url": "https://interviews.skype.com/interviews?code=649b56ce-fb4e-4eeb-a558-7fae286b45d5-26527&accessCode=60103",
            "type": "Interview",
            "participantType": "Interviewer",
            "participant": "louis@contoso.com"
        },
        {
            "url": "https://interviews.skype.com/feedback?code=649b56ce-fb4e-4eeb-a558-7fae286b45d5-26527&accessCode=60132",
            "type": "Feedback",
            "participantType": "Interviewer",
            "participant": "louis@contoso.com"
        }
    ],
    ...,
}
```
