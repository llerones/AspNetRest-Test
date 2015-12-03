using System.Collections.Generic;
using MoneyBox.Api.Interfaces;
using MoneyBox.Api.Models;

namespace MoneyBox.Api.Services
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly MoneyBoxContext _db = new MoneyBoxContext();

        /// <summary>
        /// Method to get all transactions
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Transaction> All()
        {
            return _db.Transactions;
        }


        /// <summary>
        /// Method to Get a transaction by Id
        /// </summary>
        /// <param name="transactionId">transactionId Parameter</param>
        /// <returns></returns>
        public Transaction GetById(long transactionId)
        {
            return _db.Transactions.Find(transactionId);
        }

        

       /// <summary>
       /// Method to Insert a new transaction
       /// </summary>
       /// <param name="transaction">transaction object</param>
       /// <returns></returns>
        public Transaction Insert(Transaction transaction)
        {
            var transactionToInsert = _db.Transactions.Find(transaction.TransactionId);
            if (transactionToInsert != null) return null;

            _db.Transactions.Add(transaction);
            _db.SaveChanges();

            return transaction;
        }

        /// <summary>
        /// Method to update a transaction
        /// </summary>
        /// <param name="transaction">Transaction object</param>
        /// <returns></returns>
        public bool Update(Transaction transaction)
        {
            var transactionToUpdate = _db.Transactions.Find(transaction.TransactionId);
            if (transactionToUpdate == null) return false;

            _db.Entry(transactionToUpdate).CurrentValues.SetValues(transaction);
            _db.SaveChanges();

            return true;
        }


        /// <summary>
        /// Method to delete a transaction by Id
        /// </summary>
        /// <param name="transactionId">Transaction Id to delete</param>
        /// <returns></returns>
        public bool Delete(long transactionId)
        {
            var transactionToDelete = _db.Transactions.Find(transactionId);
            if (transactionToDelete == null) return false;
            _db.Transactions.Remove(transactionToDelete);
            _db.SaveChanges();
            return true;
        }

        

        
    }
}