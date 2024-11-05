using Application.Dtos.Store;
using Application.Helpers;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Application.Profiles.Resolvers
{
    public class StoreLogoImgUrlResolver : IValueResolver<Store, StoreDto, string>
    {
       private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Content _content;

        public StoreLogoImgUrlResolver(IHttpContextAccessor httpContextAccessor, IOptions<Content> content)
        {
            _httpContextAccessor = httpContextAccessor;
            _content = content.Value;
        }

        public string Resolve(Store source, StoreDto destination, string destMember, ResolutionContext context)
        {
            string path = _content.StoreLogosPath.Replace("{{StoreId}}", source.Id);
            string fileNameWithExt = FileHelper.GetFileNameWithExt(path, "logo", "noimage");
            if (string.IsNullOrWhiteSpace(fileNameWithExt))
            {
                path = _content.NoImagePath;
                fileNameWithExt = FileHelper.GetFileNameWithExt(path, "noimage", "noimage");
            }
            HttpRequest httpRequest = _httpContextAccessor.HttpContext.Request;
            string apiUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";
            return $"{apiUrl}{path}/{fileNameWithExt}";
        }
    }
}