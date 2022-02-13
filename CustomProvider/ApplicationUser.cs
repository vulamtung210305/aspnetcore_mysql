using System.Security.Principal;
namespace MvcMovie.CustomProvider;
public class ApplicationUser : IIdentity
{
    public virtual int Id { get; set; }
    public virtual string UserName { get; set; }
    public virtual string Email { get; set; }
    public virtual bool EmailConfirmed { get; set; }
    public virtual String PasswordHash { get; set; }
    public string NormalizedUserName { get; internal set; }
    public string AuthenticationType { get; set; }
    public bool IsAuthenticated { get; set; }
    public string Name { get; set; }
}