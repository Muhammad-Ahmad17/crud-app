using BankAccount;

// Demo: bank with savings + checking accounts (OOP: encapsulation, inheritance, polymorphism, abstraction)
IAccountService service = new AccountService();

var savings = service.OpenAccount("Alice", AccountType.Savings, 1000m);
var checking = service.OpenAccount("Bob", AccountType.Checking, 500m);

service.Deposit(savings.AccountNumber, 200m);
service.Withdraw(checking.AccountNumber, 50m);
service.Transfer(savings.AccountNumber, checking.AccountNumber, 100m);

Console.WriteLine("--- Account Summary ---");
foreach (var account in service.GetAllAccounts())
{
    Console.WriteLine(account.GetStatement());
}

Console.WriteLine($"\nTotal bank balance: {service.GetTotalBalance():C}");
