using Ecommerce3.Contracts.DTO.StoreFront.Page;

namespace Ecommerce3.Application.Services.StoreFront.Interfaces;

public interface IPageService
{
    Task<PageDTO?> GetByPathAsync(string path, CancellationToken cancellationToken);    
}