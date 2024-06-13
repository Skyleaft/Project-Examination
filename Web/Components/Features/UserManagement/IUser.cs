using Domain.Common;
using Domain.Users;
using Web.Common.Models;
using Web.Components.Features.Auth;

namespace Web.Components.Features.UserManagement;

public interface IUser
{
    public Task<PaginatedResponse<ApplicationUser>> Find(FindRequest r);
    public Task<ServiceResponse> Register(UserDTO r);
}