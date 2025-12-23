namespace Ecommerce3.Domain;

public static class Common
{
    public static readonly string DateOnlyFormat = "dd-MM-yyyy";
    
    public static string AddedSuccessfully(string name)
        => $"{name} added successfully!";

    public static string EditedSuccessfully(string name)
        => $"{name} updated successfully!";
    
    public static readonly string DeletedSuccessfully = "Deleted successfully!";
}