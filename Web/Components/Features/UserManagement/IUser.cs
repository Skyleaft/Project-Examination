using Shared.Common;
using Shared.Users;
using Web.Client.Shared.Models;
using Web.Components.Features.Auth;

namespace Web.Components.Features.UserManagement;

public interface IUser
{
    public Task<PaginatedResponse<ApplicationUser>> Find(FindRequest r,CancellationToken ct);
    public Task<ServiceResponse> Register(UserDTO r);
    public Task<ServiceResponse> Update(UserEditDTO r);
    public Task<ServiceResponse> Delete(UserDTO r);
    public Task<UserDTO> Get(string id);
}