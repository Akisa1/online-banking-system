using Microsoft.EntityFrameworkCore;
using OnlineBankingSystem.Entity;
using OnlineBankingSystem.Migrations;
using OnlineBankingSystem.Models;

namespace OnlineBankingSystem.Services
{
    public class BankingService : BaseService
    {
        private readonly MyDbContext _myDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BankingService(MyDbContext myDbContext, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _myDbContext = myDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public UserAccount AddUserAccount(int userId)
        {
            var userAccount = new UserAccount()
            {
                Balance = 0,
                IBAN = GenerateRandomIBAN(),
                UserId = userId
            };

            while (_myDbContext.UserAccount.Any(x => x.IBAN == userAccount.IBAN))
            {
                userAccount.IBAN = GenerateRandomIBAN();
            }
            _myDbContext.UserAccount.Add(userAccount);
            _myDbContext.SaveChanges();
            return userAccount;
        }

        public int RegisterUser(RegisterUserRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                throw new Exception("Email zorunludur.");
            }

            var userCheck = _myDbContext.User.Any(u => u.IdentityNumber == request.IdentityNumber);

            if (userCheck)
            {
                throw new Exception("Bu Tc numarası sistemde var.");
            }

            User user = new()
            {
                Email = request.Email,
                BirthDate = request.BirthDate,
                IdentityNumber = request.IdentityNumber,
                Name = request.Name,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                Surname = request.Surname,
                UserName = request.UserName,
            };

            _myDbContext.User.Add(user);
            _myDbContext.SaveChanges();

            AddUserAccount(user.Id);

            return user.Id;
        }
        public User? Login(LoginRequestDto request)
        {
            var user = _myDbContext.User.Where(x => x.IdentityNumber == request.UserName && x.Password == request.Password).FirstOrDefault();

            if (user == null)
            {
                return null;
            }



            return user;

        }

        public List<UserAccount> GetUserAccount()
        {
            int userId = __userId;
            return GetUserAccount(userId);
        }

        public List<UserAccount> GetUserAccount(int userId)
        {
            return _myDbContext.UserAccount.Where(x => x.UserId == userId).OrderBy(x => x.Id).ToList();
        }

        public bool SendMony(SendMonyRequestDto request)
        {
            var userAccount = _myDbContext.UserAccount.Where(x => x.UserId == __userId && x.IBAN == request.SendIBAN).SingleOrDefault();
            if (userAccount == null)
            {
                return false;
            }

            if (userAccount.Balance < request.Balance)
            {
                return false;
            }

            var targetAccount = _myDbContext.UserAccount.Where(x => x.IBAN == request.RecipientIBAN).Include(x => x.User).SingleOrDefault();

            if (targetAccount == null)
            {
                return false;
            }
            if (targetAccount.User?.FullName != request.RecipientFullName)
            {
                return false;
            }

            userAccount.Balance -= request.Balance;
            targetAccount.Balance += request.Balance;

            UserAccountActivity activity = new()
            {
                ActivityType = ActivityType.GidenTransfer,
                Total = request.Balance,
                TransactionAfterBalance = userAccount.Balance,
                UserAccount = userAccount,
                UserAccountId = userAccount.Id,
                TargetIBAN = request.RecipientIBAN,
                TargetUserFullName = request.RecipientFullName
            };

            UserAccountActivity activity2 = new()
            {
                ActivityType = ActivityType.GelenTransfer,
                Total = request.Balance,
                TransactionAfterBalance = targetAccount.Balance,
                UserAccount = targetAccount,
                UserAccountId = targetAccount.Id,
                TargetIBAN = request.SendIBAN,
                TargetUserFullName = __fullName
            };

            _myDbContext.UserAccountActivity.Add(activity);
            _myDbContext.UserAccountActivity.Add(activity2);
            _myDbContext.SaveChanges();

            return true;
        }

        public List<UserAccountActivity> GetAccountyActivities(string iban)
        {
            var query = from ac in _myDbContext.UserAccount
                        join uaa in _myDbContext.UserAccountActivity on ac.Id equals uaa.UserAccountId
                        where ac.IBAN == iban && ac.UserId == __userId
                        select uaa;

            return query.ToList();
        }

        public static string GenerateRandomIBAN()
        {
            Random random = new();

            const string countryCode = "TR";
            const string bankCode = "571";
            const string branchCode = "12345";

            string accountNumber = random.Next(100000000, int.MaxValue).ToString();
            string checkDigits = "00";
            string iban = countryCode + checkDigits + bankCode + branchCode + accountNumber;

            return iban;
        }
    }


}
