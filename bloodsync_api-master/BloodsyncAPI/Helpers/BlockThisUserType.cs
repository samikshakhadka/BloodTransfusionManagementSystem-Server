using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace BloodSyncAPI.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class BlockThisUserType : Attribute, IAuthorizationFilter
    {
        private readonly string[] _blockedUserTypes;

        public BlockThisUserType(params string[] blockedUserTypes)
        {
            _blockedUserTypes = blockedUserTypes;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            const string TokenUserId = "userTypeId";
            context.HttpContext.Request.Headers.TryGetValue(TokenUserId, out var userTypeId);

            IConfiguration configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            foreach (var blockedUserType in _blockedUserTypes)
            {
                var blockedUserTypeValue = configuration.GetValue<string>($"UserType:{blockedUserType}");

                if (blockedUserTypeValue == userTypeId)
                {
                    context.Result = new ObjectResult("Method not allowed for this user type")
                    {
                        StatusCode = 401
                    };
                    return;
                }
            }
        }
    }
}
