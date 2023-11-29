namespace SkiServiceModels.Enums
{
    /// <summary>
    /// Ensure higher security by using an enum for the role names
    /// The Role with the least access should be the first in the enum
    /// </summary>
    public enum RoleNames
    {
        User,
        SuperAdmin
    }
}
