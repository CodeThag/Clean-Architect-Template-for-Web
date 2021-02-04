using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class UrlService : IUrlService
    {
        private readonly IHttpContextAccessor _httpContext;

        public UrlService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public string GerenateAbsoluteUrl(string page, string area, object values)
        {
            var request = _httpContext.HttpContext.Request;
            var url = request.Scheme + "://"+ request.Host
            throw new NotImplementedException();
        }
    }
}
