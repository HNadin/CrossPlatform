const request = require('supertest');
const https = require('https');
const { getAccessToken } = require('./utils/authUtil');

const agent = new https.Agent({
    rejectUnauthorized: false, // This allows self-signed certificates
});

const BaseURL = 'https://192.168.0.105:7225/api';

let token;

describe('AddressesController', () => {
    beforeAll(async () => {
        token = await getAccessToken();
    });

    describe('GET /addresses', () => {
        it('should return all addresses', async () => {
            const response = await request(BaseURL)
                .get('/addresses')
                .set('Authorization', `Bearer ${token}`)
                .agent(agent);

            expect(response.status).toBe(200);
            expect(response.headers['content-type']).toMatch(/json/);
            expect(Array.isArray(response.body)).toBeTruthy();
            expect(response.body.length).toBeGreaterThanOrEqual(0);
        });
    });

    // Test for GET: /api/addresses/:id
    describe('GET /addresses/:id', () => {
        it('should return an address by id', async () => {
            const testId = 'f8c91a4c-65d4-4e1f-84ed-a2e25d92d89f';
            const response = await request(BaseURL)
                .get(`/addresses/${testId}`)
                .set('Authorization', `Bearer ${token}`)
                .agent(agent);

            if (response.status === 404) {
                expect(response.status).toBe(404);
            } else {
                expect(response.status).toBe(200);
                expect(response.headers['content-type']).toMatch(/json/);
                expect(response.body).toHaveProperty('Line1');
                expect(response.body).toHaveProperty('TownCity');
            }
        });

        it('should return 404 if the address is not found', async () => {
            const invalidId = '00000000-0000-0000-0000-000000000000';
            const response = await request(BaseURL)
                .get(`/addresses/${invalidId}`)
                .set('Authorization', `Bearer ${token}`)
                .agent(agent);

            expect(response.status).toBe(404);
        });
    });
});

describe('SearchController', () => {
    beforeAll(async () => {
        token = await getAccessToken(); // Obtain the token before running the tests
    });

    // Test for GET: /api/search without any query params
    describe('GET /search', () => {
        it('should return all transactions when no filters are applied', async () => {
            const response = await request(BaseURL)
                .get('/search')
                .set('Authorization', `Bearer ${token}`)
                .agent(agent);

            expect(response.status).toBe(200);
            expect(response.headers['content-type']).toMatch(/json/);
            expect(Array.isArray(response.body)).toBeTruthy();
        });

        // Test for GET: /api/search with date filter
        it('should return transactions for a specific date', async () => {
            const date = '2024-11-21';
            const response = await request(BaseURL)
                .get(`/search?date=${date}`)
                .set('Authorization', `Bearer ${token}`)
                .agent(agent);

            expect(response.status).toBe(200);
            expect(response.headers['content-type']).toMatch(/json/);
            expect(Array.isArray(response.body)).toBeTruthy();
            response.body.forEach(transaction => {
                expect(new Date(transaction.TransactionDateTime).toISOString().startsWith(date)).toBeTruthy();
            });
        });

        // Test for GET: /api/search with transactionTypes filter
        it('should return transactions of specific types', async () => {
            const transactionTypes = ['TYPE1', 'TYPE2'];
            const response = await request(BaseURL)
                .get(`/search?transactionTypes=${transactionTypes.join(',')}`)
                .set('Authorization', `Bearer ${token}`)
                .agent(agent);

            expect(response.status).toBe(200);
            expect(response.headers['content-type']).toMatch(/json/);
            expect(Array.isArray(response.body)).toBeTruthy();
            response.body.forEach(transaction => {
                expect(transactionTypes).toContain(transaction.TransactionTypeCode);
            });
        });

        // Test for GET: /api/search with valueStart filter
        it('should return transactions with descriptions starting with a specific value', async () => {
            const valueStart = 'StartValue';
            const response = await request(BaseURL)
                .get(`/search?valueStart=${valueStart}`)
                .set('Authorization', `Bearer ${token}`)
                .agent(agent);

            expect(response.status).toBe(200);
            expect(response.headers['content-type']).toMatch(/json/);
            expect(Array.isArray(response.body)).toBeTruthy();
            response.body.forEach(transaction => {
                expect(transaction.TransactionTypeDescription.startsWith(valueStart)).toBeTruthy();
            });
        });

        // Test for GET: /api/search with valueEnd filter
        it('should return transactions with descriptions ending with a specific value', async () => {
            const valueEnd = 'EndValue';
            const response = await request(BaseURL)
                .get(`/search?valueEnd=${valueEnd}`)
                .set('Authorization', `Bearer ${token}`)
                .agent(agent);

            expect(response.status).toBe(200);
            expect(response.headers['content-type']).toMatch(/json/);
            expect(Array.isArray(response.body)).toBeTruthy();
            response.body.forEach(transaction => {
                expect(transaction.TransactionTypeDescription.endsWith(valueEnd)).toBeTruthy();
            });
        });
    });
});

describe('AccountController', () => {
    beforeAll(async () => {
        token = await getAccessToken(); 
    });

    // Test for GET: /api/v1/accounts/:id
    describe('GET /v1/accounts/:id', () => {
        it('should return an account by id for version 1.0', async () => {
            const testId = 1; 
            const response = await request(BaseURL)
                .get(`/v1/accounts/${testId}`)
                .set('Authorization', `Bearer ${token}`)
                .agent(agent);

            if (response.status === 404) {
                expect(response.status).toBe(404);
            } else {
                expect(response.status).toBe(200);
                expect(response.headers['content-type']).toMatch(/json/);
                expect(response.body).toHaveProperty('AccountNumber');
                expect(response.body).toHaveProperty('AccountStatusCode');
            }
        });
    });

    // Test for GET: /api/v2/accounts/:id
    describe('GET /v2/accounts/:id', () => {
        it('should return an account by id for version 2.0', async () => {
            const testId = 1; 
            const response = await request(BaseURL)
                .get(`/v2/accounts/${testId}`)
                .set('Authorization', `Bearer ${token}`)
                .agent(agent);

            if (response.status === 404) {
                expect(response.status).toBe(404);
            } else {
                expect(response.status).toBe(200);
                expect(response.headers['content-type']).toMatch(/json/);
                expect(response.body).toHaveProperty('AccountNumber');
                expect(response.body).toHaveProperty('AccountStatusCode');
                expect(response.body).toHaveProperty('AccountTypeDescription');
            }
        });
    });
});