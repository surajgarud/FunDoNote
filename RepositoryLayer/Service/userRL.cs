using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.context;
using RepositoryLayer.entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class userRL : IuserRL
    {
        private readonly FunDoContext funDoContext;
        private readonly IConfiguration _Toolsettings;
        public userRL(FunDoContext funDoContext, IConfiguration _Toolsettings)
        {
            this.funDoContext = funDoContext;
            this._Toolsettings = _Toolsettings;
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
        public string login(UserLogin userlogin)
        {
            try
            {
                var user = funDoContext.User.Where(x => x.Email == userlogin.Email && x.Password == userlogin.Password).FirstOrDefault();
                if (user != null)
                {
                    string token = GenerateSecurityToken(user.Email, user.Id);
                    return token;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private string GenerateSecurityToken(string Email, long Id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Toolsettings["Jwt:secretkey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Email,Email),
                new Claim("Id",Id.ToString())
            };
            var token = new JwtSecurityToken(_Toolsettings["Jwt:Issuer"],
              _Toolsettings["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public string ForgetPassword(string Email)
        {
            try
            {
                    var user = funDoContext.User.Where(x => x.Email == Email).FirstOrDefault();
                    if (user != null)
                    {
                       var token = GenerateSecurityToken(user.Email, user.Id);
                        new MSMQModel().send(token);
                        return token;
                    }
                    else
                    {
                        return null;
                    }
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
