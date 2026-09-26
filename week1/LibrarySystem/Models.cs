namespace LibrarySystem;

public interface IBorrowable
{
    bool CanBorrow();
    void MarkBorrowed();
    void MarkReturned();
}

public class Book : IBorrowable
{
    public string Isbn { get; }
    public string Title { get; }
    public string Author { get; }
    public int TotalCopies { get; }
    public int AvailableCopies { get; private set; }

    public Book(string isbn, string title, string author, int copies)
    {
        Isbn = isbn;
        Title = title;
        Author = author;
        TotalCopies = copies;
        AvailableCopies = copies;
    }

    public bool CanBorrow() => AvailableCopies > 0;

    public void MarkBorrowed()
    {
        if (!CanBorrow())
            throw new InvalidOperationException($"No copies of '{Title}' available.");
        AvailableCopies--;
    }

    public void MarkReturned()
    {
        if (AvailableCopies >= TotalCopies)
            throw new InvalidOperationException("All copies already returned.");
        AvailableCopies++;
    }
}

public class Member
{
    public string Id { get; }
    public string Name { get; }
    public List<string> BorrowedIsbns { get; } = new();

    public Member(string id, string name)
    {
        Id = id;
        Name = name;
    }
}

public interface ILibrary
{
    IReadOnlyList<Book> Books { get; }
    void AddBook(Book book);
    void AddMember(Member member);
    void Borrow(string memberId, string isbn);
    void Return(string memberId, string isbn);
    string GetReport();
}

public class Library : ILibrary
{
    public string Name { get; }
    private readonly Dictionary<string, Book> _books = new();
    private readonly Dictionary<string, Member> _members = new();

    public Library(string name) => Name = name;

    public IReadOnlyList<Book> Books => _books.Values.ToList();

    public void AddBook(Book book) => _books[book.Isbn] = book;

    public void AddMember(Member member) => _members[member.Id] = member;

    public void Borrow(string memberId, string isbn)
    {
        var member = GetMember(memberId);
        var book = GetBook(isbn);
        book.MarkBorrowed();
        member.BorrowedIsbns.Add(isbn);
        Console.WriteLine($"{member.Name} borrowed '{book.Title}'");
    }

    public void Return(string memberId, string isbn)
    {
        var member = GetMember(memberId);
        var book = GetBook(isbn);
        if (!member.BorrowedIsbns.Remove(isbn))
            throw new InvalidOperationException($"{member.Name} did not borrow this book.");
        book.MarkReturned();
        Console.WriteLine($"{member.Name} returned '{book.Title}'");
    }

    public string GetReport()
    {
        var lines = new List<string> { $"=== {Name} Report ===" };
        foreach (var book in _books.Values)
            lines.Add($"  {book.Title}: {book.AvailableCopies}/{book.TotalCopies} available");

        var activeBorrowers = _members.Values.Count(m => m.BorrowedIsbns.Count > 0);
        lines.Add($"Active borrowers: {activeBorrowers}");
        return string.Join(Environment.NewLine, lines);
    }

    private Book GetBook(string isbn) =>
        _books.TryGetValue(isbn, out var book)
            ? book
            : throw new KeyNotFoundException($"Book {isbn} not found.");

    private Member GetMember(string id) =>
        _members.TryGetValue(id, out var member)
            ? member
            : throw new KeyNotFoundException($"Member {id} not found.");
}
