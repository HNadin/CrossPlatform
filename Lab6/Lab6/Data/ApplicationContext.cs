using Lab6.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<RefAccountType> RefAccountTypes { get; set; }
        public DbSet<RefBranchType> RefBranchTypes { get; set; }
        public DbSet<RefTransactionType> RefTransactionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary keys
            modelBuilder.Entity<Bank>().HasKey(b => b.BankId);
            modelBuilder.Entity<Branch>().HasKey(b => b.BranchId);
            modelBuilder.Entity<Customer>().HasKey(c => c.CustomerId);
            modelBuilder.Entity<Account>().HasKey(a => a.AccountNumber);
            modelBuilder.Entity<Transaction>().HasKey(t => t.TransactionId);
            modelBuilder.Entity<Address>().HasKey(a => a.AddressId);
            modelBuilder.Entity<RefAccountType>().HasKey(r => r.AccountTypeCode);
            modelBuilder.Entity<RefBranchType>().HasKey(r => r.BranchTypeCode);
            modelBuilder.Entity<RefTransactionType>().HasKey(r => r.TransactionTypeCode);

            // Configure relationships
            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Bank)
                .WithMany(bk => bk.Branches)
                .HasForeignKey(b => b.BankId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Address)
                .WithMany(a => a.Branches)
                .HasForeignKey(b => b.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Branch>()
                .HasOne(b => b.RefBranchType)
                .WithMany(rt => rt.Branches)
                .HasForeignKey(b => b.BranchTypeCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Branch)
                .WithMany(b => b.Customers)
                .HasForeignKey(c => c.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Address)
                .WithMany(a => a.Customers)
                .HasForeignKey(c => c.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Accounts)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.RefAccountType)
                .WithMany(rt => rt.Accounts)
                .HasForeignKey(a => a.AccountTypeCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountNumber)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.RefTransactionType)
                .WithMany(tt => tt.Transactions)
                .HasForeignKey(t => t.TransactionTypeCode)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure field properties
            modelBuilder.Entity<Bank>().Property(b => b.BankId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Branch>().Property(b => b.BranchId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Customer>().Property(c => c.CustomerId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Models.Transaction>().Property(t => t.TransactionId).ValueGeneratedOnAdd();
        }

        public void Seed()
        {
            if (Addresses.Any()) return;

            var accountTypes = new List<RefAccountType>
            {
                new RefAccountType { AccountTypeCode = "PRM", AccountTypeDescription = "Premium Savings Account" },
                new RefAccountType { AccountTypeCode = "STD", AccountTypeDescription = "Standard Checking Account" },
                new RefAccountType { AccountTypeCode = "BUS", AccountTypeDescription = "Business Investment Account" }
            };
            RefAccountTypes.AddRange(accountTypes);
            SaveChanges();

            var branchTypes = new List<RefBranchType>
            {
                new RefBranchType { BranchTypeCode = "HQ", BranchTypeDescription = "Headquarters" },
                new RefBranchType { BranchTypeCode = "SUB", BranchTypeDescription = "Suburban Branch" }
            };
            RefBranchTypes.AddRange(branchTypes);
            SaveChanges();

            var transactionTypes = new List<RefTransactionType>
            {
                new RefTransactionType { TransactionTypeCode = "CASHIN", TransactionTypeDescription = "Cash Deposit" },
                new RefTransactionType { TransactionTypeCode = "CASHOUT", TransactionTypeDescription = "Cash Withdrawal" },
                new RefTransactionType { TransactionTypeCode = "FEE", TransactionTypeDescription = "Account Maintenance Fee" }
            };
            RefTransactionTypes.AddRange(transactionTypes);
            SaveChanges();

            var addresses = new List<Address>
            {
                new Address { Line1 = "123 Baker Street", Line2 = "Suite 3A", TownCity = "London", ZipPostcode = "NW1 6XE", StateProvinceCounty = "Greater London", Country = "UK", OtherDetails = "Near Regents Park" },
                new Address { Line1 = "500 Maple Avenue", Line2 = "Unit 12", TownCity = "Toronto", ZipPostcode = "M5H 2N2", StateProvinceCounty = "Ontario", Country = "Canada", OtherDetails = "Downtown Office" },
                new Address { Line1 = "789 Park Lane", Line2 = "Building B", TownCity = "Sydney", ZipPostcode = "2000", StateProvinceCounty = "NSW", Country = "Australia", OtherDetails = "Main Business Hub" }
            };
            Addresses.AddRange(addresses);
            SaveChanges();

            // 3. Seed Banks
            var banks = new List<Bank>
            {
                new Bank { BankId = Guid.NewGuid(), BankDetails = "Global Investment Bank" },
                new Bank { BankId = Guid.NewGuid(), BankDetails = "Northern Savings" }
            };
            Banks.AddRange(banks);
            SaveChanges();

            var branches = new List<Branch>
            {
                new Branch { BranchId = Guid.NewGuid(), AddressId = addresses[0].AddressId, BankId = banks[0].BankId, BranchTypeCode = "HQ", BranchDetails = "London Headquarters" },
                new Branch { BranchId = Guid.NewGuid(), AddressId = addresses[1].AddressId, BankId = banks[1].BankId, BranchTypeCode = "SUB", BranchDetails = "Toronto Suburban Office" },
                new Branch { BranchId = Guid.NewGuid(), AddressId = addresses[2].AddressId, BankId = banks[1].BankId, BranchTypeCode = "SUB", BranchDetails = "Sydney Business Hub" }
            };
            Branches.AddRange(branches);
            SaveChanges();

            var customers = new List<Customer>
            {
                new Customer { CustomerId = Guid.NewGuid(), AddressId = addresses[0].AddressId, BranchId = branches[0].BranchId, PersonalDetails = "Sherlock Holmes", ContactDetails = "Tel: 020-7946-0958, Email: sherlock@holmes.com" },
                new Customer { CustomerId = Guid.NewGuid(), AddressId = addresses[1].AddressId, BranchId = branches[1].BranchId, PersonalDetails = "Emma Johnson", ContactDetails = "Tel: 416-123-4567, Email: emma.johnson@mail.com" },
                new Customer { CustomerId = Guid.NewGuid(), AddressId = addresses[2].AddressId, BranchId = branches[2].BranchId, PersonalDetails = "Oliver Smith", ContactDetails = "Tel: 02-9876-5432, Email: oliver.smith@australia.com" }
            };
            Customers.AddRange(customers);
            SaveChanges();

            var accounts = new List<Account>
            {
                new Account { AccountStatusCode = "ACT", AccountTypeCode = "PRM", CustomerId = customers[0].CustomerId, CurrentBalance = 25000.00M, OtherDetails = "High-Value Premium Account" },
                new Account { AccountStatusCode = "ACT", AccountTypeCode = "BUS", CustomerId = customers[1].CustomerId, CurrentBalance = 120000.00M, OtherDetails = "Business Investment Account" },
                new Account { AccountStatusCode = "ACT", AccountTypeCode = "STD", CustomerId = customers[2].CustomerId, CurrentBalance = 5000.00M, OtherDetails = "Standard Checking Account" }
            };
            Accounts.AddRange(accounts);
            SaveChanges();

            var transactions = new List<Transaction>
            {
                new Transaction { TransactionId = Guid.NewGuid(), AccountNumber = accounts[0].AccountNumber, MerchantId = "M100", TransactionTypeCode = "CASHIN", TransactionDateTime = DateTime.Now.AddDays(-15), TransactionAmount = 5000.00M, OtherDetails = "Client Deposit" },
                new Transaction { TransactionId = Guid.NewGuid(), AccountNumber = accounts[1].AccountNumber, MerchantId = "M101", TransactionTypeCode = "FEE", TransactionDateTime = DateTime.Now.AddDays(-7), TransactionAmount = -150.00M, OtherDetails = "Monthly Account Maintenance Fee" },
                new Transaction { TransactionId = Guid.NewGuid(), AccountNumber = accounts[2].AccountNumber, MerchantId = "M102", TransactionTypeCode = "CASHOUT", TransactionDateTime = DateTime.Now.AddDays(-3), TransactionAmount = -200.00M, OtherDetails = "ATM Withdrawal" }
            };
            Transactions.AddRange(transactions);
            SaveChanges();
        }
    }
}
