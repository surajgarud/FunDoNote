using CommonLayer.Model;
using RepositoryLayer.context;
using RepositoryLayer.entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class userRL : IuserRL
    {
        private readonly FunDoContext funDoContext;
        public userRL(FunDoContext funDoContext)
        {
            this.funDoContext = funDoContext;
        }

        public userEntity Registrartion(UserRegistration User)
        {
            try
            {
                userEntity userentity = new userEntity();
                userentity.FirstName = User.FirstName;
                userentity.LastName = User.LastName;
                userentity.Email = User.Email;
                userentity.Password = User.Password;
                funDoContext.Add(userentity);
                int result = funDoContext.SaveChanges();
                if (result > 0)
                    return userentity;
                else
                    return null; 
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
