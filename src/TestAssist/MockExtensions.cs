using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace TestAssist;

public static class MockExtensions {
    public static Mock<DbSet<TEntity>> MockData<TEntity>(this IEnumerable<TEntity> data) where TEntity : class =>
        data.BuildMock().BuildMockDbSet();
    
    public static Mock<TDbContext> MockDbContext<TDbContext>(params Mock[] dataMocks) where TDbContext : DbContext {
        // Find all properties of type DbSet<T> in the DbContext
        var dbSetProperties = GetDbSetProperties<TDbContext>();

        var mockContext = new Mock<TDbContext>();
        foreach (var dataMock in dataMocks)
            mockContext.SetupMockContext(dataMock, dbSetProperties);

        return mockContext;
    }

    private static void SetupMockContext<TDbContext>(this Mock<TDbContext> mockContext, Mock dataMock,
        IEnumerable<PropertyInfo> dbSetProperties)
        where TDbContext : DbContext {
        var dbSetPropertyInfo = GetDbSetPropertyInfo<TDbContext>(dataMock, dbSetProperties);

        // mockContext.Setup(x=> x.DbSetProp).Returns(mockData.Object)
        var setupResult = mockContext.InvokeSetupMethod(dbSetPropertyInfo);
        InvokeReturnsMethod(setupResult, dataMock, dbSetPropertyInfo);
    }

    private static void InvokeReturnsMethod(object setupResult, Mock dataMock, PropertyInfo dbSetPropertyInfo) {
        var returnsMethod = setupResult.GetType().GetMethod("Returns", [dbSetPropertyInfo.PropertyType]);
        returnsMethod!.Invoke(setupResult, [dataMock.Object]);
    }

    private static object InvokeSetupMethod<TDbContext>(this Mock<TDbContext> mockContext,
        PropertyInfo dbSetPropertyInfo)
        where TDbContext : DbContext {
        // Create expression for setting up the property
        // x => x.DbSetProp
        var parameter = Expression.Parameter(typeof(TDbContext), "x");
        var propertyAccess = Expression.Property(parameter, dbSetPropertyInfo);
        var lambda = Expression.Lambda(propertyAccess, parameter);

        // Use reflection to invoke the Setup method
        var setupMethod = mockContext.GetType().GetMethods().Single(method =>
            method.ToString()!.Contains("Setup[TResult](System.Linq.Expressions.Expression"));
        var genericSetupMethod = setupMethod.MakeGenericMethod(dbSetPropertyInfo.PropertyType);
        var setupResult = genericSetupMethod.Invoke(mockContext, [lambda]);

        return setupResult!;
    }

    private static PropertyInfo GetDbSetPropertyInfo<TDbContext>(Mock dataMock,
        IEnumerable<PropertyInfo> dbSetProperties)
        where TDbContext : DbContext {
        var dataMockType = dataMock.GetType().GetGenericArguments().First();

        var dbSetPropertyInfo = dbSetProperties.FirstOrDefault(item => item.PropertyType == dataMockType);
        if (dbSetPropertyInfo is null)
            throw new ArgumentException(
                $"Type of '{dataMockType.GetGenericArguments().First()}' is not valid for '{typeof(TDbContext).FullName}'");

        return dbSetPropertyInfo;
    }

    private static List<PropertyInfo> GetDbSetProperties<TDbContext>() where TDbContext : DbContext {
        var dbContextType = typeof(TDbContext);
        var dbSetType = typeof(DbSet<>);

        var dbSetProperties = dbContextType.GetProperties()
            .Where(prop =>
                prop.PropertyType.IsGenericType &&
                prop.PropertyType.GetGenericTypeDefinition() == dbSetType
            ).ToList();

        return dbSetProperties;
    }
}