using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace SkiServiceAPI.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DbUpdateException ex)
            {

                if (IsForeignKeyViolation(ex, out string errorMessage))
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 400; // Bad Request
                    await context.Response.WriteAsJsonAsync(new { error = errorMessage });
                }
                else if (IsUniqueConstraintViolation(ex, out string uniqueErrorMessage))
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 400; // Bad Request
                    await context.Response.WriteAsJsonAsync(new { error = uniqueErrorMessage });
                }
                else
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 500; // Internal Server Error
                    await context.Response.WriteAsJsonAsync(new { error = "An error occurred processing your request." });
                }
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { error = "An error occurred processing your request." });
            }
        }

        private bool IsForeignKeyViolation(DbUpdateException ex, out string errorMessage)
        {
            errorMessage = null;
            if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                var match = Regex.Match(sqlEx.Message, @"FOREIGN KEY constraint ""([^""]+)"".+table ""([^""]+)""");
                if (match.Success)
                {
                    var constraintName = match.Groups[1].Value;
                    var tableName = match.Groups[2].Value;

                    errorMessage = $"Invalid data: Foreign key constraint violation. Constraint: {constraintName}, Table: {tableName}.";
                    return true;
                }
            }

            return false;
        }

        private bool IsUniqueConstraintViolation(DbUpdateException ex, out string errorMessage)
        {
            errorMessage = null;
            if (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                var match = Regex.Match(sqlEx.Message, @"Cannot insert duplicate key row in object '([^']+)'");
                if (match.Success)
                {
                    var tableName = match.Groups[1].Value;

                    errorMessage = $"Invalid data: A unique constraint violation occurred in table '{tableName}'.";
                    return true;
                }
            }

            return false;
        }
    }
}
