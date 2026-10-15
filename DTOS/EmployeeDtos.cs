using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.DTOS
{
    public class EmployeeDtos
    {
        public class CreateEmployee
        {
            public int? Id { get; set; }
            public string userName {  get; set; }
            [PasswordPropertyText]
            public string password { get; set; }
            public string name { get; set; }
            [EmailAddress]
            public string email { get; set; }
            private int _age;
            public int age { get { return _age; } set
                {
                    if(value < 0)
                    {
                        throw new ArgumentException("age should not be less than 0");
                    }
                     _age=value;

                } }

        }
        public class Login
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
