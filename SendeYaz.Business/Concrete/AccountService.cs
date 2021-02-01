using AutoMapper;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using SendeYaz.Business.Abstract;
using SendeYaz.Business.Validations;
using SendeYaz.Core.Aspect.Security;
using SendeYaz.Core.Aspect.Validation;
using SendeYaz.Core.Enums;
using SendeYaz.Core.Helpers;
using SendeYaz.Core.Messages;
using SendeYaz.Core.Models;
using SendeYaz.Core.Repositories;
using SendeYaz.Core.Utilities.Results.DataResult;
using SendeYaz.Core.Utilities.Results.Result;
using SendeYaz.Entities;
using SendeYaz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendeYaz.Business.Concrete
{
    
    public class AccountService : IAccountService
    {
        private readonly IDataAccessRepository<Account> _dal;
        private readonly IMapper _mapper;

        public AccountService(IDataAccessRepository<Account> dal,IMapper mapper)
        {
            _dal = dal;
            _mapper=mapper;
        }

        private async Task<IResponse> AccountExists(string email)
        {
            var account=await _dal.GetAsync(x => x.Email == email);
            if (account == null) return new SuccessResponse();
            return new ErrorResponse();
        }
        [IsAdminAspect]
        public async Task<IDataResponse<int>> DeleteAsync(int id)
        {
            var entity = await _dal.GetAsync(id);
            return await _dal.DeleteAsync(entity);
        }
        [IsAdminAspect]
        public async Task<IEnumerable<IDataResponse<int>>> DeleteRangeAsync(IEnumerable<int> list)
        {
            var result = new List<IDataResponse<int>>();
            foreach (var item in list)
            {
                result.Add(await DeleteAsync(item));
            }
            return result;
        }
        [IsAdminAspect]
        public async Task<IEnumerable<AccountModel>> GetAllAsync()
        {
            return _mapper.Map<List<AccountModel>>(await _dal.TableNoTracking.Where(x=>!x.IsBlocked).ToListAsync());
        }
        [IsAdminAspect]
        public async Task<AccountModel> GetAsync(int id)
        {
            return _mapper.Map<AccountModel>(await _dal.TableNoTracking.FirstOrDefaultAsync(x => x.Id == id));
        }

        [ValidationAspect(typeof(AccountValidator))]
        public async Task<IDataResponse<int>> InsertAsync(AccountModel model)
        {
            var account = await AccountExists(model.Email);
            if (!account.Success) return new ErrorDataResponse<int>(AccountMessage.AccountIsExists);

            var entity = _mapper.Map<Account>(model);
            HashingHelper.CreatePasswordHash(model.Password, out var passwordHash, out var passwordSalt);

            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;
            entity.RefreshToken = Helper.CreateToken();
            entity.RefreshTokenExpiredDate = DateTime.Now.AddDays(-1);

            var result = await _dal.InsertAsync(entity);

            return !result.Success ? new ErrorDataResponse<int>(result.Message) : result;
        }

        [IsAdminAspect]
        [ValidationAspect(typeof(AccountValidator))]
        public async Task<IResponse> UpdateAsync(AccountModel model)
        {
            var entity = await _dal.GetAsync(x => x.Id == model.Id);
            entity.AccountType = model.AccountType;
            entity.LastName = model.LastName;
            entity.FirstName = model.FirstName;
            entity.Email = model.Email;
            entity.Gsm = model.Gsm;
            entity.IsBlocked = model.IsBlocked;
            entity.RoleId = model.RoleId;
            return await _dal.UpdateAsync(entity);
        }

        [IsAdminAspect]
        public async Task<IEnumerable<DropdownModel>> SelectListAsync()
        {
            var entities = await _dal.TableNoTracking.OrderByDescending(x => x.FirstName).ToListAsync();
            return entities.Select(x => new DropdownModel
            {
                Id = x.Id,
                Description = $"{x.FirstName} {x.LastName}"
            });
        }

        [IsAdminAspect]
        public async Task<IResponse> IsBlockedChangeAsync(int id)
        {
            var entity = await _dal.GetAsync(id);
            if (entity.AccountType == AccountType.Admin)
                return new ErrorResponse();
            entity.IsBlocked = !entity.IsBlocked;
            return await _dal.UpdateAsync(entity);
        }

        [SecurityAspect]
        [ValidationAspect(typeof(AccountMeValidator))]
        public async Task<IResponse> UpdateMeAsync(AccountMeModel model)
        {
            var entity = await _dal.GetAsync(model.Id);
            if (entity == null) return new ErrorResponse(DbMessage.DataNotFound);
            entity.Email = model.Email;
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Gsm = model.Gsm;
            return await _dal.UpdateAsync(entity);
        }
    }
}
