﻿using eCommerce.Application.ServiceContracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid? guid = userId != null ? Guid.Parse(userId) : null;
            return guid;
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext?.User.Identity?.Name ?? string.Empty;
        }

        public string GetUserEmail()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirst("email")?.Value ?? string.Empty;
        }
    }
}
