using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace LicentaApp.Identity
{
    [CollectionName("users")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
    }
}
