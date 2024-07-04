﻿namespace Web.Client.Shared.Models;

public class MailSettings
{
    public string EmailId { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public bool UseSSL { get; set; }
}