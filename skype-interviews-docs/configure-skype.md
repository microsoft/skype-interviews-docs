# Configure Skype call and chat

By default Skype Interviews enables calling, chatting and a lobby experience. If you don't need some of those features, you can turn them off.

## List of available Skype configurations

|Property |	Description	| Data type |	Default value |
|:---------:|-------------|:---:|:---:|
|**call** |	Enable Skype audio + video calling? | Boolean | *True* |
|**chat** | Enable Skype chat? | Boolean | *True* |
|**lobby** | Enable the lobby / preparation experience? | Boolean | *True* |

## Set Skype configurations

If you don't want to use the lobby and the calling experience, you can just selective turn them off by passing the `skypeConfig` object in your request.

**Request**
```http
POST /api/interviews HTTP/1.1
Host: interviews.skype.com
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
	"skypeConfig": {
        "call": false,
        "lobby": false
	}
}
```

**Response**
```json
{
    ...,
    "urls": [
        {
            "url": "https://interviews.skype.com/interviews?code=6f93f3a5-5c90-4ba7-8e05-242bc012277f",
            "type": "Interview",
            "participantType": null,
            "participant": "all"
        }
    ],
    ...,
    "skypeConfig": {
        "call": false,
        "chat": true,
        "lobby": false
    }
}
```

As always, you can see your configuration reflected in the response if it was successful.
