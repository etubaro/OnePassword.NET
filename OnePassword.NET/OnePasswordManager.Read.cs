using OnePassword.Common;
using OnePassword.Items;
using OnePassword.Templates;

namespace OnePassword;

public sealed partial class OnePasswordManager
{
    /// <inheritdoc />
    public string Read(string uri)
    {
        var trimmedName = uri.Trim();
        if (trimmedName.Length == 0)
            throw new ArgumentException($"{nameof(uri)} cannot be empty.", nameof(uri));
        var command = $"read \"{uri}\"";
        return Op(command);
    }
}
