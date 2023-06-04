using System.Security.Claims;

namespace OnlineBankingSystem.Services
{
    public abstract class BaseService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected BaseService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int __userId
        {
            get
            {
                string userIdT = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                return Convert.ToInt32(userIdT);
            }
        }

        public string __fullName
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "FullName").Value;
            }
        }

        public string __email
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            }
        }
    }
}
