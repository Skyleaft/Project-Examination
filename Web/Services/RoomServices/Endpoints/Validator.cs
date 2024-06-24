using FastEndpoints;
using FluentValidation;
using Shared.BankSoal;
using Shared.RoomSet;

namespace Web.Services.RoomServices.Endpoints;

public class RoomValidator : Validator<Room>
{
    public RoomValidator()
    {

    }
}
