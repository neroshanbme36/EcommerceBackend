using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Controllers.Common;
using Api.Middlewares.Builders;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [ApiVersion("1.0")]
    [MiddlewareFilter(typeof(StoreEcommerceAuthMidBuilder))]
    public class BootstrapController : BaseApiController
    {
        
    }
}