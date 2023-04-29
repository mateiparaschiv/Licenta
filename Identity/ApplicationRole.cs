using AspNetCore.Identity.MongoDbCore.Models;

namespace LicentaApp.Identity
{
    [CollectionName("roles")]
    public class ApplicationRole : MongoIdentityRole<Guid>
    {

    }
}
