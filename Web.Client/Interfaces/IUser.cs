using Shared.Common;
using Shared.Users;
using Web.Client.Feature.UserManagements;
using Web.Client.Shared.Models;

namespace Web.Client.Interfaces;

public interface IUser
{
    public Task<PaginatedResponse<ApplicationUser>> Find(FindRequest r,CancellationToken ct);
    public Task<CreatedResponse<UserDTO>> Register(UserAddDTO r,CancellationToken? ct);
    public Task<ServiceResponse> Update(UserEditDTO r,CancellationToken ct);
    public Task<ServiceResponse> Delete(string id,CancellationToken? ct);
    public Task<UserDTO> Get(string id);
}