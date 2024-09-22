using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.Services.Products;
using PawFund.Presentation.Abstractions;

namespace PawFund.Presentation.Controller.V1;

[ApiVersion(1)]
public class ProductController : ApiController
{
    public ProductController(ISender sender) : base(sender)
    {}

    [HttpPost(Name = "CreateProducts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Products([FromBody] Command.CreateProductCommand CreateProduct)
    {
        var result = await Sender.Send(CreateProduct);
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }
}
