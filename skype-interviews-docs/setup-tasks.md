# Setup tasks

If you have a few task that the candidate should tackle during the interview, you can set it up through the API as well. 

## Preload a task in the code editor
A `task` object contains meta data about the task and every task has a list of definitions for the task. The definition "defines" the task in various different coding languages.

### `task` object schema
|Property |	Description	| Data type | Default value |
|:---------:|-------------|:---:|:---:|
|**title** | The title of the task | String | `undefined` |
|**description** | A description of the task | String | `undefined`|
|**definitions** | List of definitions of the task | Object Array | `[]` |

### `definition` object schema
|Property |	Description	| Data type | Default value |
|:---------:|-------------|:---:|:---:|
|**language** | Which programming language is this definition for? *Possible values:* `c`, `cpp`, `csharp`, `fsharp`, `go`, `java`, `javascript`, `php`, `python`, `ruby`, `rust`, `typescript` or `all` | String | `["all"]` |
|**content** | The content of the task that'll show up in the code editor for the given `language` | String | `""`|

__Note:__ Even though you have to pass a list of `tasks` to the API, we currently only support 1 task. We will also automatically limit the code editor's list of languages to the one for which you have a `definition` for.

### Example task: Find the error in this n-th fibonacci number function
Let's assume we want to setup a task that's targeted towards Javascript and Python developers. The task is to identify the error in the recursive fibonacci function.

The "wrong" Javascript code would look like this:
```js
function fib(n) {
  if (n < 2) {
    return n;
  } else {
    return fib(n-1) + fib(n);
  }
}
```

On the other hand the "wrong" Python function looks like this:
```py
def fib(n):
  if n < 2:
    return n
  return fib(n-1) + fib(n)
```

Now to setup this task for the user simply put it into the body of the `POST` request again.
**Request**
```http
POST /api/interviews HTTP/1.1
Host: interviews.skype.com
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
  "tasks": [{
    "title": "Ouch! Fibonacci is broken!",
    "description": "Find what is wrong with the given 'n-th fibonacci number' function. The devil is in the details!",
    "definitions": [
        {
            "language": "javascript",
            "content": "function fib(n) {\n if (n < 2) {\n    return n;\n  } else {\n    return fib(n-1) + fib(n);\n  }\n}"
        },
        {
            "language": "python",
            "content": "def fib(n):\n  if n < 2:\n    return n\n  return fib(n-1) + fib(n)"
        }
    ]
  }]
}
```

**Response**
```json
{
    ...,
    "tasks": [
        {
            "id": 2,
            "title": "Ouch! Fibonacci is broken!",
            "description": "Find what is wrong with the given 'n-th fibonacci number' function. The devil is in the details!",
            "duration": 0,
            "definitions": [
                {
                    "languageDescription": null,
                    "language": "javascript",
                    "content": "function fib(n) {\n  if (n < 2) {\n    return n;\n  } else {\n    return fib(n-1) + fib(n);\n  }\n}"
                },
                {
                    "languageDescription": null,
                    "language": "python",
                    "content": "def fib(n):\n  if n < 2:\n    return n\n  return fib(n-1) + fib(n)"
                }
            ]
        }
    ],
    ...,
}
```

If the task was setup successfully, then the change is reflected in the response as usual. Now, when you go to the returned URL, you will see that our Python / Javascript task is already setup and ready to be tackled by the candidate.
