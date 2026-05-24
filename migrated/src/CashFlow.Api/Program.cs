var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Health check
app.MapGet("/", () => Results.Ok(new { message = "CashFlow API running on .NET 10", version = "1.0.0" }));

// POST /transactions  →  create a transaction
app.MapPost("/transactions", (CreateRequest req) =>
{
    var error = ValidateTransaction(req.Description, req.Amount);
    if (error is not null)
        return Results.BadRequest(new { error });

    var tx = CreateTransaction(req.Description, req.Amount, req.Category);
    return Results.Created($"/transactions/{tx.Id}", tx);
});

// GET /transactions  →  list all
app.MapGet("/transactions", () => GetAllTransactions());

// GET /transactions/{id}  →  single transaction
app.MapGet("/transactions/{id:guid}", (Guid id) =>
{
    var tx = GetTransaction(id);
    return tx is null ? Results.NotFound() : Results.Ok(tx);
});

// DELETE /transactions/{id}
app.MapDelete("/transactions/{id:guid}", (Guid id) =>
{
    DeleteTransaction(id);
    return Results.NoContent();
});

// GET /report/{year}/{month}
app.MapGet("/report/{year:int}/{month:int}", (int year, int month) =>
{
    var all = GetAllTransactions();
    var report = GenerateMonthlyReport(year, month, all);
    return Results.Ok(report);
});

// GET /summary/categories
app.MapGet("/summary/categories", () =>
{
    var all = GetAllTransactions();
    return Results.Ok(GroupByCategory(all));
});

app.Run();

public record CreateRequest(string Description, decimal Amount, CategoryType Category);
