
using Api_Users.DAL.Context;
using Api_Users.DAL.Entities;
using Api_Users.DAL.Interfaces;
using Api_Users.DAL.Validations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api_Users.DAL.Daos
{
    public class DaoUsers : IDaoUsers
    {
        private readonly ApiUserContext _context;
        private readonly ILogger<DaoUsers> _logger;
        UserValidator validationRules = new UserValidator();

        public DaoUsers(ApiUserContext context, ILogger<DaoUsers> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task CreateAsync(Users user)
        {
           
            ValidationResult result = await validationRules.ValidateAsync(user);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogError($"Property {error.PropertyName} failed validation. Error was: {error.ErrorMessage}");
                }
                return;
            }

            try
            {
               
                bool emailExists = await _context.Users.AnyAsync(u => u.Email == user.Email);
                if (emailExists)
                {
                    _logger.LogError($"El email {user.Email} ya está registrado.");
                    throw new InvalidOperationException($"El email {user.Email} ya está registrado.");
                }

             
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el usuario.");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var idvalidation = new InlineValidator<int>();
            idvalidation.RuleFor(id => id).NotNull().GreaterThan(0);

            ValidationResult result = idvalidation.Validate(id);

            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    _logger.LogError("Property " + item.PropertyName + " failed validation. Error was: " + item.ErrorMessage);
                }
            }
            else
            {
                try
                {
                    var user = await GetByIdAsync(id);
                    user.IsDeleted = true;
                    user.DeleteDate = DateTime.Now;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }

        public async Task<List<Users>> GetAllAsync()
        {
            try
            {
                return await _context.Users.Where(x => x.IsDeleted == false).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Users> GetByIdAsync(int id)
        {
            var idvalidation = new InlineValidator<int>();
            idvalidation.RuleFor(id => id).NotNull().GreaterThan(0);

            ValidationResult result = idvalidation.Validate(id);

            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    _logger.LogError("Property " + item.PropertyName + " failed validation. Error was: " + item.ErrorMessage);
                }
                return null;
            }
            else
            {
                try
                {
                    return await _context.Users.Where(x => x.IsDeleted == false).FirstAsync(user => user.Id == id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return null;
                }
            }
        }

        public async Task UpdateAsync(Users user)
        {
            ValidationResult result = await validationRules.ValidateAsync(user);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogError("Property " + error.PropertyName + " failed validation. Error was: " + error.ErrorMessage);
                }
            }
            else
            {
                try
                {
                    user.UpdateDate = DateTime.Now;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }
    }
}
