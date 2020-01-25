# Getting Started

Skype Interviews API is built on top of the premise of using authenticated *HTTP Post* requests to a single endpoint: `https://interviews.skype.com/api/interviews`

## Get your API key and secret
In order to use the Skype Interviews API, you need to [sign up for an API key and secret](https://interviews.skype.com/api/get-started). Using the `API Key` and the `API Secret` you can generate a [JSON Web Token (JWT)](https://jwt.io) to authenticate your request.

If you want to learn more about JWT, check out our [blog post](https://aka.ms/Fopdvn) and [jwt.io](https://jwt.io).

## Authenticate your request

You can generate an interview link by doing an authenticated HTTP POST request to `https://interviews.skype.com/api/interviews`. 

Skype Interviews only accepts requests that are authenticated through a JWT that was issued with the `API Key` and signed by with the `API Secret` using JWT.

A JWT is a string consisting of three Base64 encoded strings separated by periods "`.`".

The token breaks down into a **header**, **payload** and **signature**.

You can rely on one of the [numerous libraries](https://jwt.io/#libraries) to generate JWTs to help you create the JWT. You mostly have to configure the payload. 

The **payload** is a JSON object that has the following attributes:

|Key | Description | Data type|
|:----:|-------------|:----------:|
|`jti`| **JWT ID** - a unique identifier required for every JWT to prevent replay attacks. **Value must be random GUID.**| string |
|`iss`| **Issuer** - unique identifier of the request issuer. In our case, your `API Key`.| string |
|`iat`| **Issued At** - NumericDate¹ value indicating to us, when the request was issued. | number |
|`sub`| **Subject** - A SHA256 hash of the request's body. | string |
|`exp`| **Expiration Time** - NumericDate vaule indicating after which time we should classify this request as invalid. **Value must be  `current NumericDate + 10 seconds`**.| number |

**Example**

A token payload would look like this: 
```json5
{
    "jti": "d8661a14-4b7c-5fda-2227-9b055fcf5b10", // Random GUID
    "iss": "YOUR_API_KEY",
    "iat": 1519343714, // Current NumericDate
    "sub": "44136fa355b3678a1146ad16f7e8649e94fb4fc21fe77e8310c060f61caaff8a", // SHA256 hash request body
    "exp": 1519343724 // Current NumericDate + 10 seconds
}
```

Once you have signed this payload with your `API Secret`. You'll receive something like this:

```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJkODY2MWExNC00YjdjLTVmZGEtMjIyNy05YjA1NWZjZjViMTAiLCJpc3MiOiIwZWQwZTAxYi00ZjhmLTkzZWUtNDVkZS05Njk1OTU0YTQ0YTkiLCJzdWIiOiI0NDEzNmZhMzU1YjM2NzhhMTE0NmFkMTZmN2U4NjQ5ZTk0ZmI0ZmMyMWZlNzdlODMxMGMwNjBmNjFjYWFmZjhhIiwiZXhwIjoxNTE5MzQzNzI0LCJpYXQiOjE1MTkzNDM3MTR9.4KSookeDh2d_Vujy_bUiA2n0yKY39inaIc1laWjDt6Q
```


### JavaScript example

We are using the `jsonwebtoken` node module to sign the payload and `node-fetch` to issue the HTTP POST request.

1. Install the required node modules
```
npm install guid sha256 jsonwebtoken node-fetch
```

2. Create token generator function
```js
import Guid from 'guid';
import sha256 from 'sha256';
import jwt from 'jsonwebtoken';

function generateToken(content) {
  return jwt.sign({
    jti: Guid.raw(),
    iss: API_KEY,
    sub: sha256(content),
    exp: Math.floor(Date.now() / 1000) + 10
  }, API_SECRET);
}
```
Note: `jsonwebtoken` module auto fills the `iat - Issued At` value.

3. Issue the POST request to generate an interview link and console log the output
```js
import fetch from 'node-fetch';

const payload = {};

fetch('https://interviews.skype.com/api/interviews', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
    'Authorization': 'Bearer ' + generateToken(JSON.stringify(payload))
  },
  body: JSON.stringify(payload)
})
.then(res => res.json())
.then(console.log);
```

Now you'll see the output containing the URLs generated for the interview. 

-----------------------
¹NumericDate represents the number of seconds from 1970-01-01T00:00:00Z UTC until the specified UTC date/time, ignoring leap seconds.
