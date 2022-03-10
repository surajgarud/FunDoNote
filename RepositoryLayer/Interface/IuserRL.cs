using CommonLayer.Model;
using RepositoryLayer.entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IuserRL
    {
        public userEntity Registrartion(UserRegistration User);
    }
}
