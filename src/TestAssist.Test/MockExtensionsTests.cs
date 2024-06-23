using Microsoft.EntityFrameworkCore;
using Moq;
using TestAssist;
using MockExtensions = TestAssist.MockExtensions;

namespace TestProject;

public class MockExtensionsTests {
    [Fact]
    public void MockData_ShouldReturnMockedDbSet() {
        // Arrange
        var data = new List<TestEntity> {
            new() { Id = 1, Name = "Test 1" },
            new() { Id = 2, Name = "Test 2" },
            new() { Id = 3, Name = "Test 3" }
        };

        // Act
        var mockedDbSet = data.MockData();

        // Assert
        Assert.NotNull(mockedDbSet);
        Assert.IsType<Mock<DbSet<TestEntity>>>(mockedDbSet);
        var dbSet = mockedDbSet.Object;
        var entities = dbSet.ToList();
        Assert.Equal(3, entities.Count);
        Assert.Equal(data, entities);
    }

    [Fact]
    public void MockDbContext_GiveCorrectData_ShouldReturnMockedDbContext() {
        // Arrange
        var data = new List<TestEntity> {
            new() { Id = 1, Name = "Test 1" },
            new() { Id = 2, Name = "Test 2" },
            new() { Id = 3, Name = "Test 3" }
        };
        var mockedDbSet = data.MockData();

        // Act
        var mockedDbContext = MockExtensions.MockDbContext<TestDbContext>(mockedDbSet);

        // Assert
        Assert.NotNull(mockedDbContext);
        Assert.IsType<Mock<TestDbContext>>(mockedDbContext);

        var dbContext = mockedDbContext.Object;
        var dbSet = dbContext.TestEntities.ToList();
        Assert.Equal(3, dbSet.Count);
        Assert.Equal(data, dbSet);
    }

    [Fact]
    public void MockDbContext_GiveInvalidData_ThrowArgumentException() {
        // Arrange
        var validData = new List<TestEntity> {
            new() { Id = 1, Name = "Test 1" },
        };
        var invalidData = new List<UserNotExistInDbContext> {
            new() { Id = 1, Name = "Mohammad" }
        };
        
        var mockedDbSetValid = validData.MockData();
        var mockedDbSetInValid = invalidData.MockData();
        
        Assert.Throws<ArgumentException>(() =>
            MockExtensions.MockDbContext<TestDbContext>(mockedDbSetValid, mockedDbSetInValid));
    }

    public class TestDbContext : DbContext {
        public virtual DbSet<TestEntity> TestEntities { get; set; }
    }

    public class TestEntity {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserNotExistInDbContext {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}