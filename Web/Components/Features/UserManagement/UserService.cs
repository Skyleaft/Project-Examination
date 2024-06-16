using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Common;
using Shared.Users;
using Web.Client.Shared.Models;
using Web.Common.Database;

namespace Web.Components.Features.UserManagement;

public class UserService:IUser
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _appDbContext;
    
    public UserService(UserManager<ApplicationUser> userManager, AppDbContext appDbContext)
    {
        _userManager = userManager;
        _appDbContext = appDbContext;
    }
    public async Task<PaginatedResponse<ApplicationUser>> Find(FindRequest r,CancellationToken ct)
    {
        var data =await _appDbContext.Users.WhereIf(!string.IsNullOrEmpty(r.Search),
                x => x.NamaLengkap.ToLower()
                    .Contains(r.Search.ToLower()))
            .OrderBy(x=>x.UserName)
            .ToPaginatedListAsync(r.Page, r.PageSize,r.OrderBy,r.Direction,ct);
        return data;
    }

    public async Task<ServiceResponse> Register(UserDTO r)
    {
        var finduser = await _userManager.FindByNameAsync(r.UserName);
        if(finduser!=null) return new ServiceResponse(false,"Username sudah ada");
        var user = new ApplicationUser()
        {
            NamaLengkap = r.NamaLengkap,
            UserName = r.UserName,
            Email = r.Email,
            Gender = r.Gender,
            PhoneNumber = r.PhoneNumber,
            Photo = r.Photo,
            Pekerjaan = r.Pekerjaan,
            KotaId = r.KotaId
        };
        var createUser = await _userManager.CreateAsync(user, r.Password);
        if (createUser.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, r.Role);
            return new ServiceResponse(true, "Berhasil membuat user");
        }
        return new ServiceResponse(false, createUser.Errors.First().Description);
    }

    public async Task<ServiceResponse> Update(UserEditDTO r)
    {
        var finduser = await _userManager.FindByNameAsync(r.UserName);
        var oldData = finduser.Adapt<UserDTO>();
        var rol = await _userManager.GetRolesAsync(finduser);
        oldData.Role = rol.First();
        
        finduser.NamaLengkap = r.NamaLengkap;
        finduser.Email = r.Email;
        finduser.Gender = r.Gender;
        finduser.PhoneNumber = r.PhoneNumber;
        finduser.Photo = r.Photo;
        finduser.Pekerjaan = r.Pekerjaan;
        finduser.KotaId = r.KotaId;
        var updated = await _userManager.UpdateAsync(finduser);
        if (oldData.Role != r.Role)
        {
            await _userManager.RemoveFromRoleAsync(finduser, oldData.Role);
            await _userManager.AddToRoleAsync(finduser, r.Role);
        }

        if (updated.Succeeded)
        {
            return  new ServiceResponse(true, "Berhasil mengupdate user");
        }
        else
        {
            return  new ServiceResponse(false, updated.Errors.First().Description);
        }
    }

    public async Task<ServiceResponse> Delete(UserDTO r)
    {
        var finduser = await _userManager.FindByNameAsync(r.UserName);
        await _userManager.DeleteAsync(finduser);
        return new ServiceResponse(true, "Berhasil menghapus user");
    }

    public async Task<UserDTO> Get(string id)
    {
        var finduser = await _userManager.FindByIdAsync(id);
        var data = finduser.Adapt<UserDTO>();
        var rol = await _userManager.GetRolesAsync(finduser);
        data.Role = rol.First();
        return data;
    }
}