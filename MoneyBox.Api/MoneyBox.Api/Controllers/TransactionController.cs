using System.Web.Http;
using System.Web.Http.Results;
using MoneyBox.Api.Interfaces;
using MoneyBox.Api.Models;

namespace MoneyBox.Api.Controllers
{
    /// <summary>
    /// Transactions Controller 
    /// </summary>
    public class TransactionController : ApiController
    {

        private readonly ITransactionRepository _repository;

        /// <summary>
        /// Constructor with IoC
        /// </summary>
        /// <param name="repository">Inject transactionService </param>
        public TransactionController(ITransactionRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Method to get all transactions
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetAll()
        {
            return Ok(_repository.All());
        }


        /// <summary>
        /// Method to get a specific transaction by Id
        /// </summary>
        /// <param name="transactionId">Transaction Id</param>
        /// <returns></returns>
        public IHttpActionResult GetById(long transactionId)
        {
            var data = _repository.GetById(transactionId);
            if (data != null)
            {
                return Ok(data);
            }
            return NotFound();
        }

        /// <summary>
        /// Method to Post a new transaction
        /// </summary>
        /// <param name="transaction"> Transaction object</param>
        /// <returns></returns>
        public IHttpActionResult Post(Transaction transaction)
        {
            var newTrans = _repository.Insert(transaction);
            if (newTrans != null)
            {
                return CreatedAtRoute("DefaultApi", new { newTrans.TransactionId} , newTrans);
            }
            return Conflict();
        }

        /// <summary>
        /// Method to Update a transaction
        /// </summary>
        /// <param name="transaction">Transaction to Update</param>
        /// <returns></returns>
        public IHttpActionResult Put(Transaction transaction)
        {
            if (_repository.Update(transaction))
            {
                return Ok(transaction);
            }
            return NotFound();
        }


        /// <summary>
        /// Method to delete a trasaction by Id
        /// </summary>
        /// <param name="transactionId">Transaction Id to delete</param>
        /// <returns></returns>
        public IHttpActionResult Delete(long transactionId)
        {

            if (_repository.Delete(transactionId))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}