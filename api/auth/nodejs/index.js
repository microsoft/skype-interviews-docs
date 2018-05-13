var express = require('express');
var app = express();
var Guid = require('guid');
var bodyParser = require('body-parser');
var sha256 = require('sha256');
var jwt = require('jsonwebtoken');
var fetch = require('node-fetch')

// Setup your API variables
const url = 'https://interviews.skype.com/api/interviews'
const API_KEY = 'YOUR_API_KEY'
const API_SECRET = 'YOUR_API_SECRET'

function generateSignature(content) {
  var payload = {
    jti: Guid.raw(),
    iss: API_KEY,
    sub: sha256(content),
    exp: Math.floor(Date.now() / 1000) + 10
  }

  return jwt.sign(payload, API_SECRET)
}

// customize your payload here
const payload = {
}

console.log(JSON.stringify(payload));

fetch(url, {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
    'Authorization': 'Bearer ' + generateSignature(JSON.stringify(payload))
  },
  body: JSON.stringify(payload)
})
  .then(res => res.text())
  .then(console.log)
