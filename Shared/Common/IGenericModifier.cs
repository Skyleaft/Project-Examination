﻿namespace Shared.Common;

public interface IGenericModifier
{
    public string? CreatedBy { get; set; }
    public DateTime? CreatedOn{ get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn{ get; set; }
}