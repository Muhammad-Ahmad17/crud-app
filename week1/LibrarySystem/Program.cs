using LibrarySystem;

ILibrary library = new Library("City Library");

library.AddBook(new Book("978-0132350884", "Clean Code", "Robert Martin", 3));
library.AddBook(new Book("978-0201633610", "Design Patterns", "GoF", 2));
library.AddMember(new Member("M001", "Sara Ahmed"));
library.AddMember(new Member("M002", "Ali Khan"));

library.Borrow("M001", "978-0132350884");
library.Borrow("M002", "978-0132350884");
library.Return("M001", "978-0132350884");

Console.WriteLine(library.GetReport());

// LINQ demo: books with available copies
var available = library.Books
    .Where(b => b.AvailableCopies > 0)
    .OrderBy(b => b.Title)
    .Select(b => $"{b.Title} ({b.AvailableCopies} left)");

Console.WriteLine("\nAvailable:");
foreach (var line in available)
    Console.WriteLine($"  - {line}");
