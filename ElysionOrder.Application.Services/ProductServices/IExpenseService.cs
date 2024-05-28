using ElysionOrder.Application.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElysionOrder.Application.Services.ProductServices
{
    public interface IExpenseService
    {

        public Task<List<ExpenseDto>> GetAllExpensesAsync();
        public Task<ExpenseDto> GetExpenseWithIdAsync(Guid id);
        public Task<List<ExpenseDto>> GetExpenseListWithTypeIdAsync(Guid id);
        public Task AddExpenseAsync(ExpenseDto expenseDto);
        public Task UpdateExpenseAsync(ExpenseDto expenseDto);
        public Task DeleteExpenseAsync(Guid id);



        public Task<List<ExpenseTypeDto>> GetAllExpenseTypesAsync();
        public Task<ExpenseTypeDto> GetExpenseTypeWithIdAsync(Guid id);
        public Task AddExpenseTypeAsync(ExpenseTypeDto ExpenseTypeDto);
        public Task UpdateExpenseTypeAsync(ExpenseTypeDto ExpenseTypeDto);
        public Task DeleteExpenseTypeAsync(Guid id);
    }
}
