const axios = require('axios');

// Replace these constants with your actual Auth0 credentials
const AUTH0_DOMAIN = 'https://dev-rvtsgimbavkuzus5.us.auth0.com';
const CLIENT_ID = 'hAOfKgcT8zZtZKFkoJ9qodCjStXboWLF';
const CLIENT_SECRET = '-DEEjJxhHYs79r0WJFRGLGZlBWD45ZPkb_Rcb4FgiS-OgSZsH-89v4iansfuwwvY';
const AUDIENCE = 'https://dev-rvtsgimbavkuzus5.us.auth0.com/api/v2/';
const GRANT_TYPE = 'client_credentials';

async function getAccessToken() {
    try {
        const response = await axios.post(`${AUTH0_DOMAIN}/oauth/token`, {
            client_id: CLIENT_ID,
            client_secret: CLIENT_SECRET,
            audience: AUDIENCE,
            grant_type: GRANT_TYPE,
        });

        return response.data.access_token;
    } catch (error) {
        console.error('Error fetching access token:', error.response ? error.response.data : error.message);
        throw new Error('Failed to get access token');
    }
}

module.exports = { getAccessToken };
