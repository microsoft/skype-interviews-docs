# Create interviews link

With your API key set up, we can start creating interview links. The simplest "out-of-the-box" experience of Skype Interviews can be created with this HTTP POST request.

**Request**
```http
POST /api/interviews HTTP/1.1 
Host: interviews.skype.com 
Authorization: Bearer <YOUR_TOKEN> 
Content-Type: application/json 

{}
```
**Response**
```json
{
    ...,
    "urls": [
        {
            "url": "https://interviews.skype.com/interviews?code=7afac1df-4c1d-4e5c-b201-6cdb67c9e513",
            "type": "Interview",
            "participantType": null,
            "participant": "all"
        }
    ],
    ...,
}
```
You can retrieve the `url` from the `urls` list and send it out to your candidate and interviewer.

## Set custom Interview name and position code
Every interview can have it's own name, which will be the title of the Skype group conversation. We also provide you a "position code" variable that can help you associate an interview to a given job position posting.

HTTP Request example:
```http
POST /api/interviews HTTP/1.1
Host: interviews.skype.com
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
  "code": "RENE2010",
  "title": ".Net Developer Interview"
}
```
**Response**
```json
{
    "code": "RENE2010",
    "title": ".Net Developer Interview",
    ...,
    "urls": [
        {
            "url": "https://interviews.skype.com/interviews?code=RENE2010",
            "type": "Interview",
            "participantType": null,
            "participant": "all"
        }
    ],
    ...,
}
```
