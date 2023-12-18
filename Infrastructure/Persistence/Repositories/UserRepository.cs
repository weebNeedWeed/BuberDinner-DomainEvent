using Application.Common.Interfaces.Persistence;
using Domain.User;

namespace Infrastructure.Persistence.Repositories;
public class UserRepository : IUserRepository
{
    private List<User> users = new List<User>();

    public void Add(User user)
    {
        users.Add(user);
    }

    public User? FindByEmail(string email)
    {
        return users.FirstOrDefault(x => x.Email.Equals(email));
    }
}
