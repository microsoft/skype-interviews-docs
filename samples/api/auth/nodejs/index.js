var Guid = require('guid');
var sha256 = require('sha256');
var jwt = require('jsonwebtoken');
var fetch = require('node-fetch')

// Setup your API variables
const url = 'https://interviews.skype.com/api/interviews'
const API_KEY = '40b55bb6-fd2d-7e05-c276-7d1ee4f8b54d'
const API_SECRET = '2d46ef9d-97c4-a691-4125-da407f7fa366'

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
