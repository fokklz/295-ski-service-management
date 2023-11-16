using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SkiServiceAPI.Common.OpenAPI
{
    public class SortPathsByLengthDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// Sorts the paths by length to make the Swagger UI more readable & organized
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var pathsOrdered = swaggerDoc.Paths.OrderBy(p => p.Key.Length).ThenBy(p => p.Key);

            var pathsDict = new OpenApiPaths();
            foreach (var path in pathsOrdered)
            {
                pathsDict.Add(path.Key, path.Value);
            }

            swaggerDoc.Paths = pathsDict;
        }
    }
}
