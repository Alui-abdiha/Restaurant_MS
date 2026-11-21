using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_MS.Application.DTOs.Auth
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public IList<string> Roles { get; set; } = new List<string>();
    }
}
