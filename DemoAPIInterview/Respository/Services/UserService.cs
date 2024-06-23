using DemoAPIInterview.Contracts.Request;
using DemoAPIInterview.Models;
using DemoAPIInterview.Respository.Contract;

namespace DemoAPIInterview.Respository.Services
{
    public class UserService : IUsers
    {
        private readonly ApplicationDbContext dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
      

        public Users SignUp(SignUpModel signUpModel)
        {
            var user = new Users()
            {
                Name = signUpModel.Name,
                Email = signUpModel.Email,
                Password = signUpModel.Password,
                ConfirmPassword=signUpModel.ConfirmPassword,
                Gender = signUpModel.Gender
            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();    
            return user;
        }

        Users IUsers.SignIn(SignInModel signInModel)
        {
            var user = dbContext.Users.SingleOrDefault(e => e.Email == signInModel.Email && e.Password == signInModel.Password);
            if (user == null)
            {
                return null;
            }
            else {
                return user;
            }
        }
    }
}
