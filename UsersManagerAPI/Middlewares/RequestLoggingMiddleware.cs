using ClientRegistryAPI.Models.Domain;
using ClientRegistryAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace ClientRegistryAPI.Middlewares
{

    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
           
        }

        public async Task Invoke(HttpContext context, AuditRepository auditRepository)
        {
            try
            {
                //Before
                if(auditRepository == null)
                {
                    throw new ArgumentNullException(nameof(auditRepository));
                }

                if (context == null || context.Request == null || context.Request.Method == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                // Create and save Audit info
                var requestUrl = context.Request.Path + context.Request.QueryString;
                var serviceName = context.GetEndpoint()?.Metadata.GetMetadata<ActionDescriptor>()?.DisplayName;
                if(serviceName != null)
                {
                    await auditRepository.AddAuditAsync(new Audit(serviceName, requestUrl));
                }

                await _next(context); // Call next middleware

                //After
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                throw;
            }
        }
    }
}
