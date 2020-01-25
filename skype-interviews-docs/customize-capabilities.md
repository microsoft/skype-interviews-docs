# Customize interviews

The Skype Interviews API offers a wide range of capabilities which you can configure before the interview. By default almost all capabilities are enabled to provide the most extensive out-of-the-box experience to our users. You can selective disable one or more capabilities to adapt to your user scenario.

## List of available capabilities

|Property |	Description	| Data type |	Default value |
|:---------:|-------------|:---:|:---:|
|**codeEditor** | Enable real-time code editor? | Boolean | *True* |
|**skype** | Enable Skype chat, call and lobby? | Boolean | *True* |
|**feedbacks** | Send email to request feedback from the interviewer after the interview? | Boolean | *True* for interview with `participants`. *False* for interview without `participants` |
|**notes** | Enable note-taking feature? | Boolean | *True* |
|**emails** | Shoud we send participants email confirmations about their interview? | Boolean | *True* |

## Configure capabilities

You can set capabilities by providing the `capabilities` object in your request. For instance, if we want to have an interview without the code editor and note-taking functionality, then we can just use this request.

**Request**
```http
POST /api/interviews HTTP/1.1
Host: interviews.skype.com
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
	"capabilities": {
		"codeEditor": false,
		"notes": false
	}
}
```

**Response**
```json
{
    ...,
    "capabilities": {
        "codeEditor": false,
        "skype": true,
        "feedbacks": false,
        "notes": false
    },
    "urls": [
        {
            "url": "https://interviews.skype.com/interviews?code=da9b6ab2-fb36-4e26-b26c-911144f7da95",
            "type": "Interview",
            "participantType": null,
            "participant": "all"
        }
    ],
    ...,
}
```

You can see your settings reflected in the `capabilities` object in the response of our API.

## Create a Skype Interview without requesting feedback from interviewer
In case you have your own feedback collection solution, you can surpress our feedback request email.

**Request**
```http
POST /api/interviews HTTP/1.1
Host: interviews.skype.com
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
	"capabilities": {
		"feedbacks": false
	}
}
```

**Response**
```json
{
    ...,
    "capabilities": {
        "codeEditor": true,
        "skype": true,
        "feedbacks": false,
        "notes": true
    },
    "urls": [
        {
            "url": "https://interviews.skype.com/interviews?code=6604a6d0-e73b-4825-af51-c2e3567a6694",
            "type": "Interview",
            "participantType": null,
            "participant": "all"
        }
    ],
    ...,
}
```

## Create a Skype Interview without Skype calling
In case you're using a different calling solution and want to only use the other features of Skype Interviews.

**Request**
```http
POST /api/interviews HTTP/1.1
Host: interviews.skype.com
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
	"capabilities": {
		"skype": false
	}
}
```

**Response**
```json
{
    ...,
    "capabilities": {
        "codeEditor": true,
        "skype": false,
        "feedbacks": false,
        "notes": true
    },
    "urls": [
        {
            "url": "https://interviews.skype.com/interviews?code=8f404305-2380-4cc3-be5b-cf237ad98b85",
            "type": "Interview",
            "participantType": null,
            "participant": "all"
        }
    ],
    ...,
}
```
