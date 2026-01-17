using RedeSocial.Domain.Models;

namespace RedeSocial.Tests.Factores;

public static class UserFactore
{
    public static User JoseHenrique()
    {
        var user = new User
        (
            
            "Jose Henrique",
            "jose.henrique@example.com",
            "josehenrique",
            new DateOnly(1990, 5, 20),
            "58808583",
            null
        );

        user.SetPassword("12345");

        typeof(User)
            .GetProperty(nameof(User.Id))!
            .SetValue(user, 1);

        return user;
    }

    public static User MariaSilva()
    {
        var user = new User
        (
            "Maria Silva",
            "maria.silva@example.com", 
            "mariasilva",
            new DateOnly(2008, 5, 20),
            "58808583",
            null
        );

        user.SetPassword("12345");

        typeof(User)
            .GetProperty(nameof(User.Id))!
            .SetValue(user, 2);

        return user;
    }
}
