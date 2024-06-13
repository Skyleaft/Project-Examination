using Domain.Common;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Common.Database;
using Web.Common.Models;

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
    public async Task<PaginatedResponse<ApplicationUser>> Find(FindRequest r)
    {
        var data =await _appDbContext.Users.WhereIf(!string.IsNullOrEmpty(r.Search),
                x => EF
                    .Functions
                    .Like(x.NamaLengkap, $"%{r.Search}%"))
            .OrderBy(x => x.Id)
            .ToPaginatedListAsync(r.Page, r.PageSize);
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
            TangalLahir = r.TangalLahir,
            Gender = r.Gender,
            PhoneNumber = r.NomorTelp,
            Photo = r.Photo,
        };
        var createUser = await _userManager.CreateAsync(user, r.Password);
        if (createUser.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, r.Role);
            return new ServiceResponse(true, "Berhasil membuat user");
        }
        return new ServiceResponse(false, createUser.Errors.First().Description);
    }
}