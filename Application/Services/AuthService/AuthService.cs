using Application.Repositories;
using Application.Services.AuthService.AuthService;
using Application.Services.AuthService.DTOs;
using Application.Services.CurrentUser;
using Domain.Entities;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services.AuthService.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Domain.Entities.RefreshToken> _refreshTokenRepository;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService; 
        public AuthService(IGenericRepository<User> userRepository, IGenericRepository<Domain.Entities.RefreshToken> refreshTokenRepository, IConfiguration configuration,ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _configuration = configuration;
            _currentUserService = currentUserService;
        }
        
        public async Task ChangeUserPassword(ChangeUserPasswordDto input)
        {
            var userId = _currentUserService.UserId;
            var user = await _userRepository.GetByIdAsync(userId.Value);

            var passwordHasher = new PasswordHasher<User>();
            var passwordStatus = passwordHasher.VerifyHashedPassword(user, user.Password, input.CurrentPassword);

            if (passwordStatus == PasswordVerificationResult.Failed)
            {
                throw new Exception("Current password invalid");
            }

            if (input.NewPassword != input.ConfirmNewPassword)
            {
                throw new Exception("Confirm password not matches");
            }


            user.Password = passwordHasher.HashPassword(user, input.NewPassword);

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto input)
        {
            var user = await _userRepository.GetAll()
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == input.Username.ToLower().Trim() || x.PhoneNumber == input.Username.Trim());//if he want to login by email or phone number

            if (user == null)
            {
                throw new UnauthorizedException("Username or password invalid");
            }

            var passwordHasher = new PasswordHasher<User>();
            var passwordStatus = passwordHasher.VerifyHashedPassword(user, user.Password, input.Password);

            if (passwordStatus == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedException("Username or password invalid");
            }

            var accessToken = await GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            await _refreshTokenRepository.CreateAsync(new Domain.Entities.RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
            });

            await _refreshTokenRepository.SaveChangesAsync();   
            var result = new LoginResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RoleName = user.Role.Name,
                RoleCode = user.Role.Code,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return result;
        }
        public async Task Logout()
        {
            var userId = _currentUserService.UserId;

            // Get all refresh tokens for this user
            var refreshTokens = await _refreshTokenRepository.GetAll()
                .Where(x => x.UserId == userId.Value)
                .ToListAsync();

            // Delete them
            foreach (var token in refreshTokens)
            {
                _refreshTokenRepository.Delete(token);
            }

            await _refreshTokenRepository.SaveChangesAsync();
        }

        public async Task<string> RefreshToken(RefreshTokenDto input)
        {
            var refershToken = await _refreshTokenRepository.GetAll().FirstOrDefaultAsync(x => x.Token == input.Token && x.UserId == _currentUserService.UserId && x.ExpiryDate > DateTime.UtcNow);

            if (refershToken != null)
            {
                var user = await _userRepository.GetAll().Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId.Value);
                var accessToken = await GenerateAccessToken(user);


                refershToken.Token = GenerateRefreshToken();
                refershToken.ExpiryDate = DateTime.UtcNow.AddDays(7);

                _refreshTokenRepository.Update(refershToken);
                await _refreshTokenRepository.SaveChangesAsync();   

                return accessToken;
            }

            return null;
        }
        private async Task<string> GenerateAccessToken(User user)
        {
            var jwtSection = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Role, user.Role.Name),
            };


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = jwtSection["Issuer"],
                Audience = jwtSection["Audience"],
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
            
        }

        private string GenerateRefreshToken()//Random String with 64 byte and convert it to base64 string
        {
            var random = new byte[64];
            RandomNumberGenerator.Fill(random);
            return Convert.ToBase64String(random);
        }
    }
}
