namespace BankAccount;

/// <summary>Account type discriminator.</summary>
public enum AccountType
{
    Savings,
    Checking
}

/// <summary>Abstraction: contract every account must fulfill.</summary>
public interface IBankAccount
{
    string AccountNumber { get; }
    string OwnerName { get; }
    decimal Balance { get; }
    void Deposit(decimal amount);
    void Withdraw(decimal amount);
    string GetStatement();
}

/// <summary>Encapsulation: balance is private-set; mutations go through methods.</summary>
public abstract class BankAccountBase : IBankAccount
{
    public string AccountNumber { get; }
    public string OwnerName { get; }
    public decimal Balance { get; private set; }

    protected BankAccountBase(string ownerName, decimal initialBalance)
    {
        if (string.IsNullOrWhiteSpace(ownerName))
            throw new ArgumentException("Owner name is required.");
        if (initialBalance < 0)
            throw new ArgumentException("Initial balance cannot be negative.");

        AccountNumber = Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
        OwnerName = ownerName;
        Balance = initialBalance;
    }

    public virtual void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit must be positive.");
        Balance += amount;
    }

    public virtual void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Withdraw must be positive.");
        if (amount > Balance)
            throw new InvalidOperationException("Insufficient funds.");
        Balance -= amount;
    }

    protected void AdjustBalance(decimal delta) => Balance += delta;

    public abstract string GetStatement();
}

/// <summary>Inheritance + polymorphism: savings earns interest conceptually.</summary>
public class SavingsAccount : BankAccountBase
{
    public decimal InterestRate { get; }

    public SavingsAccount(string ownerName, decimal initialBalance, decimal interestRate = 0.03m)
        : base(ownerName, initialBalance)
    {
        InterestRate = interestRate;
    }

    public override string GetStatement() =>
        $"[Savings {AccountNumber}] {OwnerName} | Balance: {Balance:C} | Rate: {InterestRate:P1}";
}

/// <summary>Checking allows overdraft up to a limit (override Withdraw).</summary>
public class CheckingAccount : BankAccountBase
{
    public decimal OverdraftLimit { get; }

    public CheckingAccount(string ownerName, decimal initialBalance, decimal overdraftLimit = 200m)
        : base(ownerName, initialBalance)
    {
        OverdraftLimit = overdraftLimit;
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Withdraw must be positive.");
        if (amount > Balance + OverdraftLimit)
            throw new InvalidOperationException("Exceeds overdraft limit.");
        AdjustBalance(-amount);
    }

    public override string GetStatement() =>
        $"[Checking {AccountNumber}] {OwnerName} | Balance: {Balance:C} | Overdraft: {OverdraftLimit:C}";
}

public interface IAccountService
{
    IBankAccount OpenAccount(string owner, AccountType type, decimal initialBalance);
    void Deposit(string accountNumber, decimal amount);
    void Withdraw(string accountNumber, decimal amount);
    void Transfer(string from, string to, decimal amount);
    IReadOnlyList<IBankAccount> GetAllAccounts();
    decimal GetTotalBalance();
}

public class AccountService : IAccountService
{
    private readonly Dictionary<string, IBankAccount> _accounts = new();

    public IBankAccount OpenAccount(string owner, AccountType type, decimal initialBalance)
    {
        IBankAccount account = type switch
        {
            AccountType.Savings => new SavingsAccount(owner, initialBalance),
            AccountType.Checking => new CheckingAccount(owner, initialBalance),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
        _accounts[account.AccountNumber] = account;
        return account;
    }

    public void Deposit(string accountNumber, decimal amount) =>
        Get(accountNumber).Deposit(amount);

    public void Withdraw(string accountNumber, decimal amount) =>
        Get(accountNumber).Withdraw(amount);

    public void Transfer(string from, string to, decimal amount)
    {
        var source = Get(from);
        var dest = Get(to);
        source.Withdraw(amount);
        dest.Deposit(amount);
    }

    public IReadOnlyList<IBankAccount> GetAllAccounts() => _accounts.Values.ToList();

    public decimal GetTotalBalance() => _accounts.Values.Sum(a => a.Balance);

    private IBankAccount Get(string accountNumber)
    {
        if (!_accounts.TryGetValue(accountNumber, out var account))
            throw new KeyNotFoundException($"Account {accountNumber} not found.");
        return account;
    }
}
