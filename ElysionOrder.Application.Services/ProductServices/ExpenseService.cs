using AutoMapper;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Domain.Entitys;
using ElysionOrder.Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ElysionOrder.Application.Services.ProductServices
{
    public class ExpenseService : IExpenseService
    {

        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ExpenseService(IUnitOfWork unitOfWork, IMapper mapper) {


            _unitOfWork = unitOfWork;
            _mapper = mapper;


        }

        public async Task AddExpenseAsync(ExpenseDto expenseDto)
        {
            var e = _mapper.Map<Expense>(expenseDto);
            e.Status = true;
           await  _unitOfWork.GetRepository<Expense>().AddAsync(e);
          await  _unitOfWork.SaveChangesAsync();
        }

        public async Task AddExpenseTypeAsync(ExpenseTypeDto expenseTypeDto)
        {
            var e = _mapper.Map<ExpenseType>(expenseTypeDto);
            e.Status = true;
            await _unitOfWork.GetRepository<ExpenseType>().AddAsync(e);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteExpenseAsync(Guid id)
        {
            var e = await _unitOfWork.GetRepository<Expense>().GetFirstWhereAsync(x => x.Id == id);
            e.Status = false;
            _unitOfWork.GetRepository<Expense>().Update(e);
          await  _unitOfWork.SaveChangesAsync(); 
        }

        public async Task DeleteExpenseTypeAsync(Guid id)
        {
            var e = await _unitOfWork.GetRepository<ExpenseType>().GetFirstWhereAsync(x => x.Id == id);
            e.Status = false;
            _unitOfWork.GetRepository<ExpenseType>().Update(e);

            var l = await _unitOfWork.GetRepository<Expense>().GetWhere(x=>x.Status && x.ExpenseTypeId== id).ToListAsync();
            l.ForEach(x => x.Status = false);
            _unitOfWork.GetRepository<Expense>().UpdateRange(l);

          await  _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ExpenseDto>> GetAllExpensesAsync()
        {
            var l = await _unitOfWork.GetRepository<Expense>().GetWhere(x=>x.Status).Include(x => x.ExpenseType).ToArrayAsync();

            var ml = _mapper.Map<List<ExpenseDto>>(l);

            return ml;  
        }

        public async Task<List<ExpenseTypeDto>> GetAllExpenseTypesAsync()
        {
            var l = await _unitOfWork.GetRepository<ExpenseType>().GetWhere(x => x.Status).ToArrayAsync();

            var ml = _mapper.Map<List<ExpenseTypeDto>>(l);

            return ml;
        }

        public async Task<List<ExpenseDto>> GetExpenseListWithTypeIdAsync(Guid id)
        {
            var l = await _unitOfWork.GetRepository<Expense>().GetWhere(x => x.Status && x.ExpenseTypeId== id).Include(x => x.ExpenseType).ToArrayAsync();

            var ml = _mapper.Map<List<ExpenseDto>>(l);

            return ml;
        }

       

        public async Task<ExpenseTypeDto> GetExpenseTypeWithIdAsync(Guid id)
        {
            var et = await _unitOfWork.GetRepository<ExpenseType>().GetFirstWhereAsync(x => x.Id == id);
            var met = _mapper.Map<ExpenseTypeDto>(et);

            return met;
           
        }

        public async Task<ExpenseDto> GetExpenseWithIdAsync(Guid id)
        {
            var et = await _unitOfWork.GetRepository<Expense>().GetWhere(x => x.Id == id).Include(x=>x.ExpenseType).FirstOrDefaultAsync();
            var met = _mapper.Map<ExpenseDto>(et);

            return met;
        }

        public async Task UpdateExpenseAsync(ExpenseDto expenseDto)
        {
            var e = await _unitOfWork.GetRepository<Expense>().GetFirstWhereAsync(x=>x.Id==expenseDto.Id);
            e.Status = true;
            e.Cost = expenseDto.Cost;
            e.Description = expenseDto.Description;
            e.ExpenseTypeId = expenseDto.ExpenseTypeId;
            _unitOfWork.GetRepository<Expense>().Update(e);
          await  _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateExpenseTypeAsync(ExpenseTypeDto expenseTypeDto)
        {
            var e = await _unitOfWork.GetRepository<ExpenseType>().GetFirstWhereAsync(x => x.Id == expenseTypeDto.Id);
            e.Status = true;
            e.Name= expenseTypeDto.Name;
            e.Description= expenseTypeDto.Description;
            _unitOfWork.GetRepository<ExpenseType>().Update(e);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
