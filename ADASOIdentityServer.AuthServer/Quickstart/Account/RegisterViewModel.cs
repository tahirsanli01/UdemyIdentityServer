﻿namespace ADASOIdentityServer.AuthServer.Quickstart.Account
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string ReturnUrl { get; set; }
    }
}
