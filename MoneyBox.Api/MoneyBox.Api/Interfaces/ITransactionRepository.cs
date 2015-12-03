using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyBox.Api.Models;

namespace MoneyBox.Api.Interfaces
{
    public interface ITransactionRepository
    {
        bool Update(Transaction transaction);
        Transaction GetById(long transactionId);
        IEnumerable<Transaction> All();
        bool Delete(long transactionId);
        Transaction Insert(Transaction transaction);
    }
}
