<div align="center">
  <h1>TestAssist</h1>
  <br />
  <a href="#getting-started"><strong>Getting Started »</strong></a>
  <br />
  <br />
  <a href="https://github.com/HamidMolareza/TestAssist/issues/new?assignees=&labels=bug&template=BUG_REPORT.md&title=bug%3A+">Report a Bug</a>
  ·
  <a href="https://github.com/HamidMolareza/TestAssist/issues/new?assignees=&labels=enhancement&template=FEATURE_REQUEST.md&title=feat%3A+">Request a Feature</a>
  .
  <a href="https://github.com/HamidMolareza/TestAssist/issues/new?assignees=&labels=question&template=SUPPORT_QUESTION.md&title=support%3A+">Ask a Question</a>
</div>

<div align="center">
<br />

![GitHub](https://img.shields.io/github/license/HamidMolareza/TestAssist)
[![Pull Requests welcome](https://img.shields.io/badge/PRs-welcome-ff69b4.svg?style=flat-square)](https://github.com/HamidMolareza/TestAssist/issues?q=is%3Aissue+is%3Aopen+label%3A%22help+wanted%22)

</div>

## About

Welcome to the `TestAssist` NuGet package!
> This project is not yet complete and is not suitable for commercial use.

### Purpose
The `TestAssist` package aims to streamline and enhance the process of writing unit tests in C#. It provides a collection of helpful utilities and extensions designed to simplify common tasks encountered during unit testing.

### Problem it Solves
Writing effective unit tests often involves repetitive tasks such as creating mock objects, asserting certain conditions, or generating test data. `TestAssist` addresses these challenges by offering a set of ready-to-use functionalities that save time and effort for developers.

### Key Features
- **Mocking Helpers**: Simplifies the creation of mock objects using popular mocking frameworks.
- **Assertion Extensions**: Provides additional assertion methods to cover common scenarios not included in standard testing libraries.
- **Utility Classes**: Offers various utility classes that assist in setting up tests, handling exceptions, and managing test environments.

### Benefits
This package improves the lives of developers by:
- **Increasing Productivity**: Reduces boilerplate code and speeds up the creation of robust unit tests.
- **Enhancing Test Coverage**: Facilitates testing of edge cases and complex scenarios through efficient data generation and mocking.
- **Improving Code Quality**: Encourages best practices in unit testing by providing tools that promote clear, concise, and maintainable tests.

### Built With

- .NET 8

## Getting Started

### Installation

Add the `TestAssist` NuGet package to your project. You can do this via the NuGet Package Manager or the Package Manager Console:

```bash
dotnet add package TestAssist
```

## Usage

The `TestAssist` package includes useful methods to help you create mock data and mock DbContext instances with ease. These methods uses `MockQueryable.Moq`, `Reflection`, and `Expression` to simplify testing of `DbContext`.

### Mocking Data
The `MockData` method allows you to create a mock `DbSet` from a collection of entities. This is particularly useful when you need to simulate a database table in your unit tests.

### Mocking DbContext
The `MockDbContext` method helps you create a mock `DbContext` by setting up multiple mock `DbSet` instances. This is useful when you need to mock a context that includes several tables.

**Example:**

```csharp
// Sample DbContext
public class MyDbContext : DbContext {
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
}

public void Example_MockDbContext() {
    // Create lists of sample data
    var users = new List<User> {
        new User { Id = 1, Name = "Alice" },
        new User { Id = 2, Name = "Bob" }
    };

    var orders = new List<Order> {
        new Order { Id = 1, UserId = 1 },
        new Order { Id = 2, UserId = 2 }
    };

    // Create mock DbSets
    var mockUserDbSet = users.MockData();
    var mockOrderDbSet = orders.MockData();

    // Use the MockDbContext method to create a mock DbContext
    // This extension uses reflections to setup `mockContext` with the given data
    var mockContext = MockExtensions.MockDbContext<MyDbContext>(mockUserDbSet, mockOrderDbSet);

    // Now you can use the mockContext in your tests

}
```
By using these methods, you can efficiently create mock data and `DbContext` instances, allowing you to write robust unit tests for your data access code without relying on an actual database.

## Roadmap

See the [open issues](https://github.com/HamidMolareza/TestAssist/issues) for a list of proposed features (and known
issues).

- [Top Feature Requests](https://github.com/HamidMolareza/TestAssist/issues?q=label%3Aenhancement+is%3Aopen+sort%3Areactions-%2B1-desc) (
  Add your votes using the 👍 reaction)
- [Top Bugs](https://github.com/HamidMolareza/TestAssist/issues?q=is%3Aissue+is%3Aopen+label%3Abug+sort%3Areactions-%2B1-desc) (
  Add your votes using the 👍 reaction)
- [Newest Bugs](https://github.com/HamidMolareza/TestAssist/issues?q=is%3Aopen+is%3Aissue+label%3Abug)

## Support

Reach out to the maintainer at one of the following places:

- [GitHub issues](https://github.com/HamidMolareza/TestAssist/issues/new?assignees=&labels=question&template=SUPPORT_QUESTION.md&title=support%3A+)
- Contact options listed on [this GitHub profile](https://github.com/HamidMolareza)

## FAQ

#### Is this project complete?

No. This is a personal project and will probably be completed over time.

## Contributing

First off, thanks for taking the time to contribute! Contributions are what make the free/open-source community such an
amazing place to learn, inspire, and create. Any contributions you make will benefit everybody else and are **greatly
appreciated**.

Please read [our contribution guidelines](docs/CONTRIBUTING.md), and thank you for being involved!

## Authors & contributors

The original setup of this repository is by [HamidMolareza](https://github.com/HamidMolareza).

For a full list of all authors and contributors,
see [the contributors page](https://github.com/HamidMolareza/TestAssist/contributors).

## Security

`TestAssist` follows good practices of security, but 100% security cannot be assured. `TestAssist` is provided **"as
is"** without any **warranty**.

_For more information and to report security issues, please refer to our [security documentation](docs/SECURITY.md)._

## License

This project is licensed under the **GPLv3**.

See [LICENSE](LICENSE) for more information.
