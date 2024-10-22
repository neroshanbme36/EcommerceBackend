using Application.Dtos.Store;
using Application.Helpers;
using Application.Models;
using AutoMapper;
using Domain.Crm.Entities;
using Microsoft.Extensions.Options;

namespace Application.Profiles.Resolvers
{
    public class StoreLogoImgUrlResolver : IValueResolver<Store, StoreDto, string>
    {
        private readonly MicroservicesBaseUrl _microserviceBaseUrl;
        private readonly Content _content;

        public StoreLogoImgUrlResolver(IOptions<MicroservicesBaseUrl> microserviceBaseUrl, IOptions<Content> content)
        {
            _microserviceBaseUrl = microserviceBaseUrl.Value;
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
            string apiUrl = _microserviceBaseUrl.CurrentServerUrl;
            return $"{apiUrl}{path}/{fileNameWithExt}";
        }
    }
}