namespace LibraryMS.Application.Common.Extensions;

public static class SecurityValidationExtensions
{
    public static async Task<Result> ValidateUserPersonMatchAsync(
        this IIdentityUser identityUser,
        int userId,
        int entityPersonId)
    {
        var userResult = await identityUser.CurrentUserByIdAsync(userId);
        if (userResult.IsFailure)
            return Result.Failure("User not found");

        var user = userResult.Data;
        if (user?.PersonId != entityPersonId)
            return Result.Failure("Security Alert: User does not belong to the same person as the entity.");

        return Result.Success;
    }
}