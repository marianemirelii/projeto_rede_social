using RedeSocial.Domain.Models;

namespace RedeSocial.Tests.Factores;

public class CommentFactore
{
    public static Comment Comment()
    {
        return new Comment(1, PostFactore.Post(), 1, UserFactore.JoseHenrique(), "Teste");
    }
}
