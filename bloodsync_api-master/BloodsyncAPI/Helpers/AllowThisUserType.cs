using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace BloodSyncAPI.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AllowThisUserType : Attribute, IAuthorizationFilter
    {
        private readonly string[] _allowedUserTypes;

        public AllowThisUserType(params string[] allowedUserTypes)
        {
            _allowedUserTypes = allowedUserTypes;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            const string TokenUserId = "userTypeId";
            context.HttpContext.Request.Headers.TryGetValue(TokenUserId, out var userTypeId);

            IConfiguration configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            foreach (var allowedUserType in _allowedUserTypes)
            {
                var allowedUserTypeValue = configuration.GetValue<string>($"UserType:{allowedUserType}");

                if (allowedUserTypeValue?.ToLower() == userTypeId)
                {
                    return;
                }
            }
            context.Result = new ObjectResult("User type not allowed to access this method Allow wala")
            {
                StatusCode = 401
            };
        }
    }
}
