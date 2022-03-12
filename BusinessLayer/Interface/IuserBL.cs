using CommonLayer.Model;
using RepositoryLayer.entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IuserBL
    {
        public userEntity Registrartion(UserRegistration User);
        public string login(UserLogin userlogin);
        public string ForgetPassword(string Email);
    }
}
