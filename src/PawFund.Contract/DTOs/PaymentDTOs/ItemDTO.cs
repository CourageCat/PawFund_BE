namespace PawFund.Contract.DTOs.PaymentDTOs;

public class ItemDTO
{
    public string ItemName { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }

    public ItemDTO(string itemName, int quantity, int price)
    {
        ItemName = itemName;
        Quantity = quantity;
        Price = price;
    }
}
