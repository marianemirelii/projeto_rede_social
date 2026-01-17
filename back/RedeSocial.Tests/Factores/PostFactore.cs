using RedeSocial.Domain.Models;

namespace RedeSocial.Tests.Factores;

public class PostFactore
{
    public static Post Post()
    {
        var request = RequestFactores.postRequest();
        var post = new Post(1, request.Content, request.IsPublic, request.Image, UserFactore.JoseHenrique());

        return post;
    }
}
