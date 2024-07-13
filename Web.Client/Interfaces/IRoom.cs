using Shared.Common;
using Shared.RoomSet;
using Web.Client.Shared.Models;

namespace Web.Client.Interfaces;

public interface IRoom
{
    public Task<CreatedResponse<Room>> Create(Room r);
    public Task<ServiceResponse> Update(Room r);
    public Task<ServiceResponse> Delete(Guid Id);
    public Task<Room> Get(Guid Id);
    public Task<Room> Get(string kode);
    public Task<PaginatedResponse<Room>> Find(FindRequest r, CancellationToken ct, string? Username = "");
    public Task<List<Room>> AllRefference(CancellationToken ct);
}