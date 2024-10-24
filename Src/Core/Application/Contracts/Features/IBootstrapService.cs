using Application.Dtos.Bootstrap;

namespace Application.Contracts.Features
{
    public interface IBootstrapService
    {
        Task<BootstrapResponseDto> GetBootstrapDatas(string deviceId, string userEmail, BootstrapRequestDto bootstrapRequestDto);
    }
}