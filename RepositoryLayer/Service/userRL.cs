using CommonLayer.Model;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
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
using Xamarin.Essentials;

namespace RepositoryLayer.Service
{
    public class userRL : IuserRL
    {
        private readonly FunDoContext funDoContext;
        private readonly IConfiguration configuration;
        public userRL(FunDoContext funDoContext, IConfiguration configuration)
        {
            this.funDoContext = funDoContext;
            this.configuration = configuration;
        }

        public userEntity GetEmail(string Email)
        {
            var result = funDoContext.User.FirstOrDefault(e => e.Email == Email);

            return result;
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
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:secretkey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Email,Email),
                new Claim("Id",Id.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
              configuration["Jwt:Issuer"],
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
        
        public bool ResetPassword(string Email, String Password, string ConfirmPassword)
        {
            try
            {
                if (Password.Equals(ConfirmPassword))
                {
                    var user = funDoContext.User.Where(x => x.Email == Email).FirstOrDefault();
                    user.Password = ConfirmPassword;
                    funDoContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
