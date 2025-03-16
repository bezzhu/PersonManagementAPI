using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PersonManagementAPI.Filters
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.RequestBody?.Content?.Any(x => x.Key == "multipart/form-data") == true)
            {
                var schema = operation.RequestBody.Content["multipart/form-data"].Schema;
                schema.Properties.Clear(); 
                schema.Properties.Add("file", new OpenApiSchema { Type = "string", Format = "binary" }); 
            }
        }
    }
}
