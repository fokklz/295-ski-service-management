using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using SkiServiceModels.DTOs.Responses;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SkiServiceAPI.Common.OpenAPI
{
    public class GenericResponseOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Filter to fully document the response types of the generic controller
        /// </summary>
        /// <param name="operation">OpenApiOperation</param>
        /// <param name="context">OperationFilterContext</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var controllerActionDescriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

            if (controllerActionDescriptor != null)
            {
                var baseType = controllerActionDescriptor.ControllerTypeInfo.BaseType;
                if (baseType != null && baseType.IsGenericType)
                {
                    var genericArguments = baseType.GetGenericArguments();
                    // Assuming at least two generic type arguments
                    if (genericArguments.Length == 5)
                    {
                        Type dto = genericArguments[2]; // admin dto

                        // Check if the action name is "GetAll", adjust the type to be a list of DTOs
                        if (controllerActionDescriptor.ActionName == "GetAll")
                        {
                            dto = typeof(List<>).MakeGenericType(dto);

                        }
                        // Check if the action name is "Delete", adjust the type to be of DeleteResponse
                        else if (controllerActionDescriptor.ActionName == "Delete")
                        {
                            dto = typeof(DeleteResponse);
                        }

                        var schema = context.SchemaGenerator.GenerateSchema(dto, context.SchemaRepository);

                        operation.Responses[StatusCodes.Status200OK.ToString()] = new OpenApiResponse
                        {
                            Description = "Success",
                            Content = new Dictionary<string, OpenApiMediaType>
                            {
                                ["application/json"] = new OpenApiMediaType
                                {
                                    Schema = schema
                                }
                            }
                        };
                    }
                }
            }
        }
    }
}
