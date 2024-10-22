using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Controllers.Common;
using Api.Errors;
using Api.Extensions;
using Application.Constants;
using Application.Contracts.Features;
using Application.Dtos.Store;
using Domain.Crm.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
  [ApiVersion("1.0")]
  public class StoresController : BaseApiController
  {
    private readonly IStoreService _storeService;

    public StoresController(IStoreService storeService)
    {
      _storeService = storeService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Store?>> GetStore()
    {
      string? storeGuid = HttpContext.Request.Headers[RequestHeaderCodes.STORE_GUID];
      var storeDto = await _storeService.GetStoreByGuid(storeGuid);
      return storeDto;
    }
  }
}
