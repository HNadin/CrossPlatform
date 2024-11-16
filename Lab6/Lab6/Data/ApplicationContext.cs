using Lab6.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<RefAccountType> RefAccountTypes { get; set; }
        public DbSet<RefBranchType> RefBranchTypes { get; set; }
        public DbSet<RefTransactionType> RefTransactionTypes { get; set; }

        public void Seed()
        {
            // Seed Banks
            var banks = new List<Bank>
            {
                new Bank { BankId = Guid.NewGuid(), BankDetails = "Bank A Details" },
                new Bank { BankId = Guid.NewGuid(), BankDetails = "Bank B Details" }
            };
            Banks.AddRange(banks);

            // Seed Branch Types
            var branchTypes = new List<RefBranchType>
            {
                new RefBranchType { BranchTypeCode = "LU", BranchTypeDescription = "Large Urban" },
                new RefBranchType { BranchTypeCode = "SR", BranchTypeDescription = "Small Rural" }
            };
            RefBranchTypes.AddRange(branchTypes);

            // Seed Branches
            var branches = new List<Branch>
            {
                new Branch { BranchId = Guid.NewGuid(), BankId = banks[0].BankId, AddressId = Guid.NewGuid(), BranchTypeCode = "LU", BranchDetails = "Branch 1 Details" },
                new Branch { BranchId = Guid.NewGuid(), BankId = banks[1].BankId, AddressId = Guid.NewGuid(), BranchTypeCode = "SR", BranchDetails = "Branch 2 Details" }
            };
            Branches.AddRange(branches);

            // Seed Addresses
            var addresses = new List<Address>
            {
                new Address { AddressId = branches[0].AddressId, Line1 = "123 Main St", Line2 = "Suite 100", TownCity = "City A", ZipPostcode = "12345", StateProvinceCounty = "State A", Country = "Country A", OtherDetails = "Near central park" },
                new Address { AddressId = branches[1].AddressId, Line1 = "456 Side St", Line2 = "Apt 200", TownCity = "Town B", ZipPostcode = "67890", StateProvinceCounty = "State B", Country = "Country B", OtherDetails = "Next to library" }
            };
            Addresses.AddRange(addresses);

            // Seed Customers
            var customers = new List<Customer>
            {
                new Customer { CustomerId = Guid.NewGuid(), AddressId = addresses[0].AddressId, BranchId = branches[0].BranchId, PersonalDetails = "Customer 1 Details", ContactDetails = "Contact 1" },
                new Customer { CustomerId = Guid.NewGuid(), AddressId = addresses[1].AddressId, BranchId = branches[1].BranchId, PersonalDetails = "Customer 2 Details", ContactDetails = "Contact 2" }
            };
            Customers.AddRange(customers);

            // Seed Account Types
            var accountTypes = new List<RefAccountType>
            {
                new RefAccountType { AccountTypeCode = "CHK", AccountTypeDescription = "Checking" },
                new RefAccountType { AccountTypeCode = "SVG", AccountTypeDescription = "Savings" }
            };
            RefAccountTypes.AddRange(accountTypes);

            // Seed Accounts
            var accounts = new List<Account>
            {
                new Account { AccountNumber = 1001, AccountStatusCode = "Active", AccountTypeCode = "CHK", CustomerId = customers[0].CustomerId, CurrentBalance = 1000.00m, OtherDetails = "Account 1 Details" },
                new Account { AccountNumber = 1002, AccountStatusCode = "Active", AccountTypeCode = "SVG", CustomerId = customers[1].CustomerId, CurrentBalance = 2000.00m, OtherDetails = "Account 2 Details" }
            };
            Accounts.AddRange(accounts);

            // Seed Transaction Types
            var transactionTypes = new List<RefTransactionType>
            {
                new RefTransactionType { TransactionTypeCode = "DEP", TransactionTypeDescription = "Deposit" },
                new RefTransactionType { TransactionTypeCode = "WTH", TransactionTypeDescription = "Withdrawal" }
            };
            RefTransactionTypes.AddRange(transactionTypes);

            // Seed Transactions
            var transactions = new List<Transaction>
            {
                new Transaction { TransactionId = Guid.NewGuid(), AccountNumber = accounts[0].AccountNumber, MerchantId = "M001", TransactionTypeCode = "DEP", TransactionDateTime = DateTime.Now, TransactionAmount = 500.00m, OtherDetails = "Transaction 1 Details" },
                new Transaction { TransactionId = Guid.NewGuid(), AccountNumber = accounts[1].AccountNumber, MerchantId = "M002", TransactionTypeCode = "WTH", TransactionDateTime = DateTime.Now, TransactionAmount = 200.00m, OtherDetails = "Transaction 2 Details" }
            };
            Transactions.AddRange(transactions);

            SaveChanges();
        }
    }
}
