# Manage tenants

If you want to manage a group of different positions and their respective interviews, the best way to do it is using our "tenants" capabilities.
Basically tenants function like a seperate company. They have their own API keys with which they can create their own positions and interviews. 

**Request**
```
POST /api/tenants HTTP/1.1
Host: interviews.skype.com
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
  "company": {
    "name": "Contoso Tenant Ltd"
  }
}
```

**Response**
```
{
  "dashboardUrl": "YOUR_DASHBOARD_URL",
  "company": {
    "name": "Contoso Tenant Ltd",
    "apiKey": "API_KEY",
    "apiSecret": "API_SECRET"
  },
  ...
}
```

Using the new `API_KEY` and `API_SECRET` you can generate a new JWT to create interviews and manage positions like every other company.

## Dashboard for tenant administrators

If you want to have an admin for the company, you can also provide the admin's email and he can then access the dashboard with his email address, after authentication.

**Request**
```
POST /api/tenants HTTP/1.1
Host: interviews.skype.com
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
  "company": {
    "name": "Contoso Tenant Ltd",
  },
  "admin": {
    "name": "Rene Brandel",
    "email": "rene@contoso.com"
  }
}
```

**Response**
```
{
  "dashboardUrl": "YOUR_DASHBOARD_URL",
  "company": {
    "name": "Contoso Ltd",
    "apiKey": "API_KEY",
    "apiSecret": "API_SECRET"
  },
  "admin": {
    "name": "Rene Brandel",
    "email": "rene@contoso.com"
  },
  "webHooks": {
    "endpoint": "",
    "events": "*"
  }
}
```

## Webhooks for tenants

Setup webhooks for a tenant can also be done by supplying the endpoint in the body

**Request**
```
POST /api/tenants HTTP/1.1
Host: interviews.skype.com
Authorization: Bearer <YOUR_TOKEN>
Content-Type: application/json

{
  "company": {
    "name": "Contoso Tenant Ltd",
  },
  "webHooks": {
    "endpoint": "https://your.webhook.endpoint.com/interviews",
  }
}
```

**Response**
```
{
  "dashboardUrl": "YOUR_DASHBOARD_URL",
  "company": {
    "name": "Contoso Ltd",
    "apiKey": "API_KEY",
    "apiSecret": "API_SECRET"
  },
  ...,
  "webHooks": {
    "endpoint": "https://your.webhook.endpoint.com/interviews",
    "events": "*"
  }
}
```