using Ecommerce3.Contracts.DTOs.Image;

namespace Ecommerce3.Admin.ViewModels.Product;

public class EditProductViewModel
{
    public IReadOnlyList<ImageDTO> Images { get; set; } = [];

}