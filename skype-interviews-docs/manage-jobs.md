# Manage job positions

Every Skype Interview is created under a position which is defined by a name, code and a small description. The Skype interview position is a simple way of connecting external ATS, job boards and other systems.

By default, Skype Interviews API does not require partners to use positions at all. However, if partner wants to group interviews under positions it is possible.

## List of capabilities for positions
|Property |	Description	| Data type |	Default value |
|:---------:|-------------|:---:|:---:|
|**title** | Job position title for the interview | String | `"Default position"` |
|**code** | Job position code used as an identifier | String | `"everest-default"` |
|**description** | Description of the job position | String | `""` |
|**url** | URL of your job posting | String | `""` |

## Create a new interview under a new position
You can create a new interview under a new position by passing us a `position` object. If the `code` doesn't already exist, we'll automatically create a new position and create a new interview under that position.

**Request**
```http
POST /api/interviews HTTP/1.1
Host: interviews.skype.com
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
	"position" : {
		"code": "SIN-12345",
		"title": "Software Engineer / Senior Software Engineer - Skype",
		"Description": "Are you passionate about creating premier user experiences which are used by millions of people every single day? Do you want to be a part of working on a new application being developed as we speak? Are you interested in seeing the results of your work featured on all the prominent web sites on the Internet?
		We are the team of developers working on a messaging part of completely new Skype client for Android, iOS, Windows and MacOS (check it out: https://www.skype.com/en/new/ ). We are doing it using OSS tools and frameworks such are React Native/JS, Typescript, Node and many others. ",
		"url": "https://careers.microsoft.com/jobdetails.aspx?ss=&pg=0&so=&rw=1&jid=305610&jlang=en&pp=ss"
	},
}
```

**Response**
```json
{
    ...,
    "position": {
        "title": "Software Engineer / Senior Software Engineer - Skype",
        "code": "SIN-12345",
        "description": "Are you passionate about creating premier user experiences which are used by millions of people every single day? Do you want to be a part of working on a new application being developed as we speak? Are you interested in seeing the results of your work featured on all the prominent web sites on the Internet?\n\t\tWe are the team of developers working on a messaging part of completely new Skype client for Android, iOS, Windows and MacOS (check it out: https://www.skype.com/en/new/ ). We are doing it using OSS tools and frameworks such are React Native/JS, Typescript, Node and many others. ",
        "url": null
    },
    ...,
    "urls": [
        {
            "url": "https://interviews.skype.com/interviews?code=27c5b1f3-d6de-47aa-bd72-27f73bef6acb-09888",
            "type": "Interview",
            "participantType": null,
            "participant": "all"
        }
    ],
    ...,
}
```

## Create a new interview under an existing position
To create a new interview under an existing solution, just pass in the `position` object with a `code` value that already exists and we'll automatically create a new interview under that position.

**Request**
```http
POST /api/interviews HTTP/1.1
Host: interviews.skype.com
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
	"position" : {
		"code": "SIN-12345",
	},
}
```

**Response**
```json
{
    "position": {
        "title": "Software Engineer / Senior Software Engineer - Skype",
        "code": "SIN-12345",
        "description": "Are you passionate about creating premier user experiences which are used by millions of people every single day? Do you want to be a part of working on a new application being developed as we speak? Are you interested in seeing the results of your work featured on all the prominent web sites on the Internet?\n\t\tWe are the team of developers working on a messaging part of completely new Skype client for Android, iOS, Windows and MacOS (check it out: https://www.skype.com/en/new/ ). We are doing it using OSS tools and frameworks such are React Native/JS, Typescript, Node and many others. ",
        "url": null
    },
    ...,
    "urls": [
        {
            "url": "https://interviews.skype.com/interviews?code=57af9c10-6bcb-4e72-b7b3-7d9f4687373a-06690",
            "type": "Interview",
            "participantType": null,
            "participant": "all"
        }
    ],
    ...,
}
```
