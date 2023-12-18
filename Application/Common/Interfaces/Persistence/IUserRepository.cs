using Domain.User;

namespace Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? FindByEmail(string email);

    void Add(User user);    
}
