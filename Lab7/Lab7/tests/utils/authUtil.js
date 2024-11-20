// tests/utils/authUtil.js

const axios = require('axios');

const authDomain = 'https://dev-rvtsgimbavkuzus5.us.auth0.com'; 
const clientId = 'hAOfKgcT8zZtZKFkoJ9qodCjStXboWLF'; 
const clientSecret = '-DEEjJxhHYs79r0WJFRGLGZlBWD45ZPkb_Rcb4FgiS-OgSZsH-89v4iansfuwwvY'; 
const audience = 'https://dev-rvtsgimbavkuzus5.us.auth0.com/api/v2/';

async function getApiToken() {
    try {
        const response = await axios.post(`${authDomain}/oauth/token`, {
            client_id: clientId,
            client_secret: clientSecret,
            audience: audience,
            grant_type: 'client_credentials'
        });

        return response.data.access_token;
    } catch (error) {
        console.error('Error obtaining token:', error);
        throw error;
    }
}

module.exports = getApiToken;
