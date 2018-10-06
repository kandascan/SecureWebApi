using System.ComponentModel;

namespace SecureWebAPI.Enums
{
    public enum Errors
    {
        [Description("Duplicate user")]
        DUPLICATE,
        NOT_VALID,
        NOT_EXIST,
        [Description("System Exception")]
        EXCEPTION,
        [Description("User password incorrect")]
        PASSWORD_INCORRECT
    }
}