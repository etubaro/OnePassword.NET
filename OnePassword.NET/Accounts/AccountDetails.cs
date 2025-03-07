﻿using OnePassword.Common;

namespace OnePassword.Accounts;

/// <summary>
/// Represents a 1Password account with details.
/// </summary>
public sealed class AccountDetails : IAccount
{
    /// <inheritdoc />
    [JsonInclude]
    [JsonPropertyName("id")]
    public string Id { get; internal init; } = "";

    /// <summary>
    /// The account name.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("name")]
    public string Name { get; internal init; } = "";

    /// <summary>
    /// The account domain.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("domain")]
    public string Domain { get; internal init; } = "";

    /// <summary>
    /// The account type.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("type")]
    public AccountType Type { get; internal init; } = AccountType.Unknown;

    /// <summary>
    /// The state of the account.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("state")]
    public State State { get; internal init; } = State.Unknown;

    /// <summary>
    /// The date and time when the account was created.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("created_at")]
    public DateTimeOffset Created { get; internal init; }

    /// <inheritdoc />
    public void Deconstruct(out string id, out string name)
    {
        id = Id;
        name = Name;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Name;
    }

    public static bool operator ==(AccountDetails a, IAccount b) => a.Equals(b);

    public static bool operator !=(AccountDetails a, IAccount b) => !a.Equals(b);

    public static bool operator <(AccountDetails a, IAccount b) => a.CompareTo(b) < 0;

    public static bool operator <=(AccountDetails a, IAccount b) => a.CompareTo(b) <= 0;

    public static bool operator >(AccountDetails a, IAccount b) => a.CompareTo(b) > 0;

    public static bool operator >=(AccountDetails a, IAccount b) => a.CompareTo(b) >= 0;

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is IAccount other && Equals(other);
    }

    /// <inheritdoc />
    public bool Equals(IAccount? other)
    {
        if (other is null) return false;
        return ReferenceEquals(this, other) || string.Equals(Id, other.Id, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        if (ReferenceEquals(this, obj)) return 0;
        return obj is IAccount other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(IAccount)}");
    }

    /// <inheritdoc />
    public int CompareTo(IAccount? other)
    {
        if (other is null) return 1;
        if (ReferenceEquals(this, other)) return 0;
        return other switch
        {
            Account account => CompareTo(account),
            AccountDetails accountDetails => CompareTo(accountDetails),
            _ => string.Compare(Id, other.Id, StringComparison.Ordinal)
        };
    }

    private int CompareTo(Account? other)
    {
        return other is null ? 1 : string.Compare(Domain, other.Shorthand, StringComparison.Ordinal);
    }

    private int CompareTo(AccountDetails? other)
    {
        if (other is null) return 1;
        return ReferenceEquals(this, other) ? 0 : string.Compare(Domain, other.Domain, StringComparison.Ordinal);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(Id);
    }
}