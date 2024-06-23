using DemoAPIInterview.Contracts.Request;
using DemoAPIInterview.Models;

namespace DemoAPIInterview.Respository.Contract
{
    public interface IUsers
    {
        Users SignIn(SignInModel signInModel);
        Users SignUp(SignUpModel signUpModel);
    }
}
