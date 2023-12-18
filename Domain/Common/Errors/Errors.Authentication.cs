using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error DuplicatedEmail =>
            Error.Conflict("Authentication.DuplicatedEmail", "Email already exists.");

        public static Error InvalidCredenticals =>
            Error.Validation("Authentication.InvalidCreds", "Invalid credentials");
    }
}
