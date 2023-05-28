using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace LicentaApp.Identity
{
    [CollectionName("roles")]
    public class ApplicationRole : MongoIdentityRole<Guid>
    {

    }
}
