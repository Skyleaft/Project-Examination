namespace Web.Client.Shared.Models;

public record ServiceResponse(bool Flag, string Message=null!)
{
    public bool IsSuccessStatusCode { get; set; }
}