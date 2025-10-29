using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Bank;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Admin.ViewModels.Bank;

public record BanksIndexViewModel
{
    public BankFilter Filter { get; init; }
    public PagedResult<BankListItemDTO> Banks { get; init; }
    public string PageTitle { get; init; }
}