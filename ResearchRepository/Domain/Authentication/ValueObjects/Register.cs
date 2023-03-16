using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Domain.Authentication.ValueObjects
{
    public class Register
    {
        public string FirstName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string ConfirmedPassword { set; get; }
        public string University { set; get; }
        public string AcademicUnit { set; get; }
        public string? Degree { get; set; }
        public string? Title { get; set; }
        public string? Biography { get; set; }
        public string? Country { get; set; }
        public bool SamePass { get; set; }



        public Register(string e, string p)//para el sign in
        {
            Email = e;
            Password = p;
        }

        public Register(string e, string p, string c)
        {
            Email = e;
            Password = p;
            ConfirmedPassword = c;
        }

        public Register(string firstName, string firstLastName, string secondLastName, string email, string password, string confirmedPassword, string university, string academicUnit, string? degree, string? title, string? biography, string? country)
        {
            SamePass = true;
            FirstName = firstName;
            FirstLastName = firstLastName;
            SecondLastName = secondLastName;
            Email = email;
            Password = password;
            ConfirmedPassword = confirmedPassword;
            University = university;
            AcademicUnit = academicUnit;
            Degree = degree;
            Title = title;
            Biography = biography;
            Country = country;
            sameConfirmedPass();
        }

        public bool sameConfirmedPass()
        {
            if (Password != ConfirmedPassword)
            {
                SamePass = false;
            }
            return SamePass;
        }

    }
}
