using CashFlow.Core.Domain;

namespace CashFlow.Core.Modules;

// ═══════════════════════════════════════════════════════════════
// Module: ValidationModule
// Pure validation functions (no side effects)
// ═══════════════════════════════════════════════════════════════
public static class ValidationModule
{
    public static bool IsValidTransaction(string description, decimal amount) =>
        !string.IsNullOrWhiteSpace(description) && amount != 0;

    public static string? ValidateTransaction(string description, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(description))
            return "Description cannot be empty.";
        if (amount == 0)
            return "Amount cannot be zero.";
        return null;
    }
}
