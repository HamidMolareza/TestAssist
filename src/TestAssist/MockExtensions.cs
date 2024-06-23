using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace TestAssist;

public static class MockExtensions {
    public static Mock<DbSet<TEntity>> MockData<TEntity>(this IEnumerable<TEntity> data) where TEntity : class =>
        data.BuildMock().BuildMockDbSet();
}