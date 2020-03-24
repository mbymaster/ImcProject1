using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImcProject1.Abstractions
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> GetAsync(string urlAddOn);
    }
}
