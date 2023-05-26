
using AspNetCore.Identity.MongoDbCore.Models;

namespace LicentaApp.Identity
{
    [CollectionName("users")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
    }
}
