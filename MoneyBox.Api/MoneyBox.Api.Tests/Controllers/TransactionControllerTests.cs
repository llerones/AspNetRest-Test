using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyBox.Api.Interfaces;
using MoneyBox.Api.Models;
using Moq;

namespace MoneyBox.Api.Controllers.Tests
{
    [TestClass()]
    public class TransactionControllerTests
    {
        private Mock<ITransactionRepository> _repo;

        [TestInitialize]
        public void SetUp()
        {

            _repo = new Mock<ITransactionRepository>();

        }

        [TestMethod]
        public void GetAll_ShouldReturnOkNegotiated()
        {
            // Arrange
            _repo.Setup(foo => foo.All()).Returns(() => new List<Transaction>{new Transaction()
            {
                TransactionId = 1,
                CurrencyCode = "GPB",
                TransactionAmount = 100,
                Merchant = "PayPal",
                Description = "Buy a car",
                TransactionDate = DateTime.Today,
                ModifiedDate = DateTime.Today,
                CreatedDate = DateTime.Now
            },
            new Transaction()
            {
                TransactionId = 2,
                CurrencyCode = "GPB",
                TransactionAmount = 3400,
                Merchant = "MasterCard",
                Description = "Buy a house",
                TransactionDate = DateTime.Today,
                ModifiedDate = DateTime.Today,
                CreatedDate = DateTime.Now
            }});
            var controller = new TransactionController(_repo.Object);

            // Act
            IHttpActionResult  httpActionResult = controller.GetAll();

            // Assert
            Assert.IsInstanceOfType(httpActionResult, typeof(OkNegotiatedContentResult<IEnumerable<Transaction>>));
        }

        [TestMethod]
        public void GetAll_ShouldReturnTwoTransactions()
        {
            // Arrange
            _repo.Setup(foo => foo.All()).Returns(() => new List<Transaction>{new Transaction()
            {
                TransactionId = 1,
                CurrencyCode = "GPB",
                TransactionAmount = 100,
                Merchant = "PayPal",
                Description = "Buy a car",
                TransactionDate = DateTime.Today,
                ModifiedDate = DateTime.Today,
                CreatedDate = DateTime.Now
            },
            new Transaction()
            {
                TransactionId = 2,
                CurrencyCode = "GPB",
                TransactionAmount = 3400,
                Merchant = "MasterCard",
                Description = "Buy a house",
                TransactionDate = DateTime.Today,
                ModifiedDate = DateTime.Today,
                CreatedDate = DateTime.Now
            }});
            var controller = new TransactionController(_repo.Object);

            // Act
            var httpActionResult = controller.GetAll();

            // Assert
            var response = httpActionResult as OkNegotiatedContentResult<IEnumerable<Transaction>>;
            Assert.IsNotNull(response);

            var transactions = response.Content;
            Assert.AreEqual(2, transactions.Count());
        }

        [TestMethod]
        public void GetById_WhenGettingWithAKnownIdItShouldReturnThatTransaction()
        {
            // Arrange
            _repo.Setup(foo => foo.GetById(4567)).Returns(() => new Transaction()
            {
                TransactionId = 4567,
                CurrencyCode = "GPB",
                TransactionAmount = 100,
                Merchant = "PayPal",
                Description = "Buy a car",
                TransactionDate = DateTime.Today,
                ModifiedDate = DateTime.Today,
                CreatedDate = DateTime.Now
            });
            var controller = new TransactionController(_repo.Object);

            // Act
            var httpActionResult = controller.GetById(4567);

            // Assert
            var response = httpActionResult as OkNegotiatedContentResult<Transaction>;
            Assert.IsNotNull(response);

            Assert.AreEqual(4567, response.Content.TransactionId);
        }

        [TestMethod]
        public void GetById_WhenGettingWithAnUnknownIdItShouldReturnNotFound()
        {
            // Arrange
            _repo.Setup(foo => foo.GetById(1)).Returns(() => new Transaction()
            {
                TransactionId = 1,
                CurrencyCode = "GPB",
                TransactionAmount = 100,
                Merchant = "PayPal",
                Description = "Buy a car",
                TransactionDate = DateTime.Today,
                ModifiedDate = DateTime.Today,
                CreatedDate = DateTime.Now
            });
            var controller = new TransactionController(_repo.Object);

            // Act
            var httpActionResult = controller.GetById(99999);

            // Assert
            Assert.IsInstanceOfType(httpActionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Put_WhenPuttingATransactionItShouldBeUpdated()
        {
            // Arrange
            _repo.SetupSequence(foo => foo.Update(It.IsAny<Transaction>())).Returns(true).Returns(false);

            var controller = new TransactionController(_repo.Object);

            //Act  => Return True
            var updatetransaction = new Transaction() { TransactionId = 1, Merchant = "New Merchant", TransactionAmount = 8888};
            var actionResult = controller.Put(updatetransaction);

            // Assert
            var response = actionResult as OkNegotiatedContentResult<Transaction>;
            Assert.IsNotNull(response);
            var transaction = response.Content;

            Assert.AreEqual(1, transaction.TransactionId);
            Assert.AreEqual("New Merchant", transaction.Merchant);
            Assert.AreEqual(8888, transaction.TransactionAmount);


            //Act  => Return False
            var actionResultFalse = controller.Put(updatetransaction);
            var responseFalse = actionResultFalse as NotFoundResult;

            // Assert
            Assert.IsInstanceOfType(responseFalse, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Post_WhenPostingANewTransactionShouldBeAddedOrReturnConflict()
        {
            // Arrange
            _repo.SetupSequence(foo => foo.Insert(It.IsAny<Transaction>())).Returns(new Transaction()
            {
                TransactionId = 2,
                CurrencyCode = "GPB",
                TransactionAmount = 3400,
                Merchant = "MasterCard",
                Description = "Buy a house",
                TransactionDate = DateTime.Today,
                ModifiedDate = DateTime.Today,
                CreatedDate = DateTime.Now
            }).Returns(null);
            var controller = new TransactionController(_repo.Object);

            //Act => return a transaction
            var addtransaction = new Transaction()
            {
                CurrencyCode = "GPB",
                TransactionAmount = 3400,
                Merchant = "MasterCard",
                Description = "Buy a house",
                TransactionDate = DateTime.Today,
                ModifiedDate = DateTime.Today,
                CreatedDate = DateTime.Now
            };
            var actionResult = controller.Post(addtransaction);

            // Assert
            var response = actionResult as CreatedAtRouteNegotiatedContentResult<Transaction>;
            Assert.IsNotNull(response);
            Assert.AreEqual("DefaultApi", response.RouteName);
            Assert.AreEqual(response.Content.TransactionId, response.RouteValues["transactionId"]);


            //Act => return Null
            actionResult = controller.Post(addtransaction);

            // Assert
            var response2 = actionResult as ConflictResult;
            Assert.IsInstanceOfType(response2, typeof(ConflictResult));
        }

        [TestMethod]
        public void Delete_WhenDeleteATransactionShouldBeDeletedOrReturnBadRequest()
        {
            // Arrange
            _repo.SetupSequence(foo => foo.Delete(It.IsAny<long>())).Returns(true);
            var controller = new TransactionController(_repo.Object);

            //Act => return true
            var actionResult = controller.Delete(2);

            // Assert
            var response = actionResult as OkResult;
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkResult));

        }

        [TestMethod]
        public void Delete_WhenTryToDeleteATransactionSIdInventedhouldReturnNotFound()
        {
            // Arrange
            _repo.Setup(foo => foo.Delete(It.IsAny<long>())).Returns(false);
            var controller = new TransactionController(_repo.Object);

            //Act => return false
            var actionResult = controller.Delete(2);

            // Assert
            var response = actionResult as NotFoundResult;
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }
    }
}
