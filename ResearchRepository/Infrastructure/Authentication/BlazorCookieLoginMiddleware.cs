using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResearchRepository.Domain.Authentication.Repositories;

namespace ResearchRepository.Infrastructure.Authentication
{
    public class BlazorCookieLoginMiddleware<TUser> where TUser : class
    {
        private readonly RequestDelegate _next;

        public BlazorCookieLoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAuthenticationRepository auth)
        {
            if (context.Request.Path == "/login" && context.Request.Query.ContainsKey("key"))
            {
                var key = context.Request.Query["key"];

                var result = await auth.SignInInternalAsync(key, true);

                if (result)
                {
                    context.Response.Redirect("/");
                    return;
                }
                else
                {
                    await _next.Invoke(context);
                }
            }
            else if (context.Request.Path.StartsWithSegments("/logout"))
            {
                await auth.SignOut();
                context.Response.Redirect("/");
                return;
            }

            //Continue http middleware chain:
            await _next.Invoke(context);
        }
    }
}
