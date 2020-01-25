We know coding interviews can be tricky and requires a great setup. Therefore we provide a number of options to customize the interviewing experience to your liking.

# List of available code editor configurations

|Property |	Description	| Data type |	Default value |
|:---------:|-------------|:---:|:---:|
|**codeExecution** | Enable code execution? | Boolean | `true` |
|**codingLanguages** | Which coding languages should be available to choose? *Possible values:* `c`, `cpp`, `csharp`, `fsharp`, `go`, `java`, `javascript`, `php`, `python`, `ruby`, `rust`, `typescript` or `all` | String array | `["all"]` |
|**defaultCodingLanguage** | What is the default coding language? | String | `"javascript"` |

# Set code editor configurations

If you don't want to disable code execution, limit coding languages or change the default language, then you can configure these options by passing the `codingConfig` object in your request.

For instance, if you want to disable code execution and change default language to `Java`, then you can issue this request.

**Request**
```http
POST /api/interviews HTTP/1.1
Host: interviews.skype.com
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
	"codingConfig": {
        "codeExecution": false,
        "defaultCodingLanguage": "java"
	}
}
```

**Response**
```json
{
    ...,
    "urls": [
        {
            "url": "https://interviews.skype.com/interviews?code=653cb17e-7173-4746-b7f1-296ef895b1a7",
            "type": "Interview",
            "participantType": null,
            "participant": "all"
        }
    ],
    "codingConfig": {
        "codeExecution": false,
        "codingLanguages": [
            "all"
        ],
        "defaultCodingLanguage": "java"
    },
    ...,
}
```

As always, you can see your configuration reflected in the response if it was successful.

# Additional scenarios

## Change code editor languages
If you want to limit the code editor to certain languages, then you'll have to set the `codingLanguages` object in `codingConfig`. In this example, we're limiting it to Java, C# and C++.

**Request**
```http
POST /api/interviews HTTP/1.1
Host: interviews.skype.com
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
	"codingConfig": {
        "codingLanguages": [
        	"java",
        	"csharp",
        	"cpp"
        ]
	}
}
```

**Response**
```json
{
    ...,
    "urls": [
        {
            "url": "https://interviews.skype.com/interviews?code=763fc7d5-9e80-4f28-9d3c-df12a402d74a",
            "type": "Interview",
            "participantType": null,
            "participant": "all"
        }
    ],
    "codingConfig": {
        "codeExecution": true,
        "codingLanguages": [
            "java",
            "csharp",
            "cpp"
        ],
        "defaultCodingLanguage": "java"
    },
    ...,
}
```
