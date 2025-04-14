using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace TestSupport.Helpers;

public class TransactionHelper
{
    public delegate Task DatabaseOperationAsync(TaskManagerContext context);

    public static async Task WrapInTransactionAndRollbackAsync(DatabaseOperationAsync databaseOperation)
    {
        var options = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted
        };

        var dbContextOptions = new DbContextOptionsBuilder<TaskManagerContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var transaction = new TransactionScope(TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled);

        await using var context = new TaskManagerContext(dbContextOptions);

        context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        await databaseOperation(context);

        transaction.Dispose();
    }
}
