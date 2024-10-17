using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawFund.Contract.DTOs.PaymentDTOs;
using PawFund.Contract.Services.Products;
using PawFund.Presentation.Abstractions;
using static PawFund.Contract.Services.Products.Filter;

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

    [HttpGet(Name = "GetProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct([FromQuery] ProductFilter filterParams,
    [FromQuery] int pageIndex = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string[] selectedColumns = null)
    {
        var result = await Sender.Send(new Query.GetProductsPaginQueryHandler(pageIndex, pageSize, filterParams, selectedColumns));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpPost("create-payment", Name = "Payment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Payment([FromBody] CreatePaymentDTO paymentDto)
    {
        var result = await Sender.Send(new Query.GetPaymentProductQueryHandler(paymentDto));
        if (result.IsFailure)
            return HandlerFailure(result);

        return Ok(result);
    }

    [HttpGet("create", Name = "Payment123")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Payment1()
    {
        var redirectUrl = $"http://127.0.0.1:3000/payment-return?orderId={1}";
        // Chuyển hướng đến URL
        return Redirect(redirectUrl);
    }
}