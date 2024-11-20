const request = require('supertest');
const baseURL = 'https://localhost:7225';

describe('Integration Tests for Lab6 API', () => {

    describe('AccountController Tests', () => {
        it('GET /api/v1/accounts should return list of accounts', async () => {
            const response = await request(baseURL).get('/api/v1/accounts');
            expect(response.status).toBe(200);
            expect(response.body).toBeInstanceOf(Array);
        });

        it('GET /api/v1/accounts/:id should return specific account', async () => {
            const accountId = 1; // «м≥н≥ть на на€вний ID дл€ тестуванн€
            const response = await request(baseURL).get(`/api/v1/accounts/${accountId}`);
            if (response.status === 200) {
                expect(response.body).toHaveProperty('AccountNumber', accountId);
            } else {
                expect(response.status).toBe(404);
            }
        });
    });

    describe('AddressesController Tests', () => {
        it('GET /api/addresses should return list of addresses', async () => {
            const response = await request(baseURL).get('/api/addresses');
            expect(response.status).toBe(200);
            expect(response.body).toBeInstanceOf(Array);
        });

        it('GET /api/addresses/:id should return specific address', async () => {
            const addressId = 'some-valid-guid'; // «м≥н≥ть на на€вний ID дл€ тестуванн€
            const response = await request(baseURL).get(`/api/addresses/${addressId}`);
            if (response.status === 200) {
                expect(response.body).toHaveProperty('Line1');
            } else {
                expect(response.status).toBe(404);
            }
        });
    });

    describe('CustomersController Tests', () => {
        it('GET /api/customers should return list of customers', async () => {
            const response = await request(baseURL).get('/api/customers');
            expect(response.status).toBe(200);
            expect(response.body).toBeInstanceOf(Array);
        });

        it('GET /api/customers/:id should return specific customer', async () => {
            const customerId = 'some-valid-guid'; // «м≥н≥ть на на€вний ID дл€ тестуванн€
            const response = await request(baseURL).get(`/api/customers/${customerId}`);
            if (response.status === 200) {
                expect(response.body).toHaveProperty('PersonalDetails');
            } else {
                expect(response.status).toBe(404);
            }
        });
    });

    describe('SearchController Tests', () => {
        it('GET /api/search should return filtered transactions', async () => {
            const response = await request(baseURL).get('/api/search').query({ date: '2024-10-10' });
            expect(response.status).toBe(200);
            expect(response.body).toBeInstanceOf(Array);
        });
    });
});
