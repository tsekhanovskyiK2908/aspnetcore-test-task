using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationTestTask.Bl.Abstraction.Services;
using WebApplicationTestTask.Bl.Implementation.Services;
using WebApplicationTestTask.Dal.Abstraction;
using WebApplicationTestTask.Dal.Implementation;
using WebApplicationTestTask.Dal.Implementation.Seed;
using WebApplicationTestTask.Entities;
using WebApplicationTestTask.Mappers.Abstraction;
using WebApplicationTestTask.Models.Customer;
using WebApplicationTestTask.Models.Response;

namespace WebApplicationTestTask.Tests.Services
{   
    [TestFixture]
    public class CustomerServiceTest : EntityServiceTest
    {
        private Mock<ICustomerMapper> _customerMapperMock;
        private ICustomerRepository _customerRepository;
        private IOrderRepository _orderRepository;
        private OrderingSystemDbContext _orderingSystemDbContext;
        private ICustomerService _customerService;

        [SetUp]
        public void SetUp()
        {
            _orderingSystemDbContext = CreateCleanDbContext();
            Seed(_orderingSystemDbContext);
            _customerRepository = new CustomerRepository(_orderingSystemDbContext);
            _orderRepository = new OrderRepository(_orderingSystemDbContext);
            _customerMapperMock = new Mock<ICustomerMapper>();

            CustomerMapperSetup();
            _customerService = new CustomerService(_customerRepository, _customerMapperMock.Object, _orderRepository);
        }

        [Test]
        [TestCase(1)]
        public async Task ShouldReturnCustomerById(int customerId)
        {
            //using (var context = new OrderingSystemDbContext(CreateNewContextOptions()))
            //{
            //    context.Database.EnsureDeleted();

            //    Seed(context);

            //    ICustomerRepository customerRepository = new CustomerRepository(context);
            //    IOrderRepository orderRepository = new OrderRepository(context);

            //    ICustomerService customerService = new CustomerService(customerRepository, _customerMapperMock.Object, orderRepository);

            //    DataResult<CustomerModel> dataResult = await customerService.GetCustomer(customerId);

            //    Assert.IsNotNull(dataResult);
            //    Assert.AreEqual(ResponseMessageType.Success, dataResult.ResponseMessageType);
            //    Assert.IsInstanceOf<CustomerModel>(dataResult.Data);
            //    Assert.AreEqual("Danylo Kozak", dataResult.Data.Name);

            //    context.Database.EnsureDeleted();
            //}
            //_customerService = new CustomerService(_customerRepository, _customerMapperMock.Object, _orderRepository);
            Customer customerFromDb = _orderingSystemDbContext.Customers.Find(customerId);

            DataResult<CustomerModel> dataResult = await _customerService.GetCustomer(customerId);

            Assert.IsNotNull(dataResult);
            Assert.IsNotNull(customerFromDb);
            Assert.AreEqual(ResponseMessageType.Success, dataResult.ResponseMessageType);
            Assert.IsInstanceOf<CustomerModel>(dataResult.Data);
            Assert.AreEqual(customerFromDb.Name, dataResult.Data.Name);
        }

        [Test]
        [TestCase(100)]
        public async Task ShouldReturnNullCustomerById(int customerId)
        {
            //using (var context = new OrderingSystemDbContext(CreateNewContextOptions()))
            //{
            //    context.Database.EnsureDeleted();

            //    Seed(context);

            //    ICustomerRepository customerRepository = new CustomerRepository(context);
            //    IOrderRepository orderRepository = new OrderRepository(context);

            //    ICustomerService customerService = new CustomerService(customerRepository, _customerMapperMock.Object, orderRepository);

            //    DataResult<CustomerModel> dataResult = await customerService.GetCustomer(customerId);

            //    Assert.IsNull(dataResult.Data);
            //    Assert.AreEqual(dataResult.ResponseMessageType, ResponseMessageType.Error);

            //    context.Database.EnsureDeleted();
            //}

            //_customerService = new CustomerService(_customerRepository, _customerMapperMock.Object, _orderRepository);
            Customer customerInDb = _orderingSystemDbContext.Customers.Find(customerId);

            DataResult<CustomerModel> dataResult = await _customerService.GetCustomer(customerId);

            Assert.IsNull(customerInDb);
            Assert.IsNull(dataResult.Data);
            Assert.AreEqual(ResponseMessageType.Error, dataResult.ResponseMessageType);
        }

        [Test]
        public async Task ShouldReturnAllCustomers()
        {
            //using (var context = new OrderingSystemDbContext(CreateNewContextOptions()))
            //{
            //    Seed(context);

            //    ICustomerRepository customerRepository = new CustomerRepository(context);
            //    IOrderRepository orderRepository = new OrderRepository(context);

            //    ICustomerService customerService = new CustomerService(customerRepository, _customerMapperMock.Object, orderRepository);

            //    DataResult<List<CustomerModel>> dataResult = await customerService.GetAllCustomers();

            //    Assert.NotZero(dataResult.Data.Count);
            //    Assert.AreEqual(dataResult.Data.Count, context.Customers.Count());

            //    context.Database.EnsureDeleted();
            //}

            //_customerService = new CustomerService(_customerRepository, _customerMapperMock.Object, _orderRepository);

            DataResult<List<CustomerModel>> dataResult = await _customerService.GetAllCustomers();

            Assert.NotZero(dataResult.Data.Count);
            Assert.AreEqual(dataResult.Data.Count, _orderingSystemDbContext.Customers.Count());
        }

        [Test]
        public async Task ShouldCreateCustomer()
        {
            //using (var context = new OrderingSystemDbContext(CreateNewContextOptions()))
            //{
            //    context.Database.EnsureDeleted();

            //    Seed(context);

            //    _customerMapperMock.Setup(cm => cm.MapBackToEntity(It.IsAny<CustomerCreationModel>()))
            //                    .Returns((CustomerCreationModel ccm) => new Customer
            //                    {
            //                        Name = ccm.Name,
            //                        Address = ccm.Address
            //                    });

            //    _customerMapperMock.Setup(cm => cm.MapToModel(It.IsAny<Customer>()))
            //                       .Returns((Customer c) => new CustomerModel
            //                       {
            //                           Id = c.Id,
            //                           Address = c.Address,
            //                           Name = c.Name
            //                       });

            //    ICustomerRepository customerRepository = new CustomerRepository(context);
            //    IOrderRepository orderRepository = new OrderRepository(context);

            //    ICustomerService customerService = new CustomerService(customerRepository, _customerMapperMock.Object, orderRepository);

            //    DataResult<CustomerModel> dataResult = await customerService.CreateCustomer(CustomerCreationModels[0]);

            //    Assert.Greater(dataResult.Data.Id, 0);
            //    Assert.AreEqual(dataResult.ResponseMessageType, ResponseMessageType.Success);

            //    context.Database.EnsureDeleted();
            //}
            int maxCustomerId = _orderingSystemDbContext.Customers.Max(c => c.Id);
            int expectedCustomerId = maxCustomerId + 1;

            DataResult<CustomerModel> dataResult = await _customerService.CreateCustomer(CustomerCreationModels[0]);

            Assert.AreEqual(expectedCustomerId, dataResult.Data.Id);
            Assert.AreEqual(ResponseMessageType.Success, dataResult.ResponseMessageType);
        }

        [TearDown]
        public void TearDown()
        {
            _orderingSystemDbContext.Database.EnsureDeleted();
        }

        public void CustomerMapperSetup()
        {
            _customerMapperMock.Setup(cm => cm.MapBackToEntity(It.IsAny<CustomerCreationModel>()))
                                .Returns((CustomerCreationModel ccm) => new Customer
                                {
                                    Name = ccm.Name,
                                    Address = ccm.Address
                                });

            _customerMapperMock.Setup(cm => cm.MapToModel(It.IsAny<Customer>()))
                               .Returns((Customer c) => new CustomerModel
                               {
                                   Id = c.Id,
                                   Address = c.Address,
                                   Name = c.Name
                               });
        }

        private static List<CustomerCreationModel> CustomerCreationModels = new List<CustomerCreationModel>
        {
            new CustomerCreationModel
            {
                Name = "Andrii Grishyn",
                Address = "Kyiv, Obolon"
            }
        };
    }
}
