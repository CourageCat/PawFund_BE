using PawFund.Contract.Enumarations.Cat;

namespace PawFund.Contract.DTOs.CatDTOs;

public class CatDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Age { get; set; }
    public string Breed { get; set; }
    public string Color { get; set; }
    public string Description { get; set; }
    public bool Sterilization { get; set; }
    public CatSex Sex { get; set; }
    public List<ImageCatDto> ImageCats { get; set; }
}
