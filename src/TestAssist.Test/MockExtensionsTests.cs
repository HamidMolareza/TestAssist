using Microsoft.EntityFrameworkCore;
using Moq;
using TestAssist;

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

    public class TestEntity {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}