
using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(UpdateProductDto Product);
    public record UpdateProductResponse(UpdateProductResponseDto Product);

    public class UpdateProductEndpoint  : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender)
            =>{
              
                    var command = request.Adapt<UpdateProductCommand>();
                    var result = await sender.Send(command);
                    var response = result.Adapt<UpdateProductResponse>();
                    return Results.Ok(response);
               
                return Results.Ok();

            })
              .WithName("UpdateProduct")
             .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Update Product")
             .WithDescription("Update Product");
        }
    }
}