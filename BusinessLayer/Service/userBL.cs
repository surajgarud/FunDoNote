using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class userBL : IuserBL
    {
        private readonly IuserRL userRL;
        public userBL(IuserRL userRL)
        {
            this.userRL = userRL;
        }

        public string ForgetPassword(string Email)
        {
            try
            {
                return userRL.ForgetPassword(Email);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string login(UserLogin userlogin)
        {
            try
            {
                return userRL.login(userlogin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public userEntity Registrartion(UserRegistration User)
        {
            try
            {
                return userRL.Registrartion(User);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
