using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlyingDutchmanAirlines.RepositoryLayer;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using FlyingDutchmanAirlines.DatabaseLayer;
namespace FlyingDutchmanAirlines_Test
{
    [TestClass]
    public class CustomerRepositoryTests
    {

        private FlyingDutchmanAirlinesContext _context;
        private CustomerRepository _repository;
        [TestMethod]
        public void CreateCustomer_Success()
        {
             
        }
        [TestMethod]
        public async Task  CreateCustomer_Failure_NameIsNull()
        {
            

            bool result = await _repository.CreateCustomer(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task  CreateCustomer_NameIsEmpty() 
        {
           

            bool result = await _repository.CreateCustomer("");
            Assert.IsFalse(result);
        }
        
        [TestMethod]
        [DataRow('#')]
        [DataRow('$')]
        [DataRow('!')]
        [DataRow('*')]
        [DataRow('@')]
        public async Task CreateCustomer_NameContainsInvalidCharacter(char invalidCharacter) 
        {
         
            bool result =  await _repository.CreateCustomer("Donald Knuth" + invalidCharacter);
            Assert.IsFalse(result);

        }
    
        [TestInitialize]
        public void TestInitialize()
        {
            DbContextOptions<FlyingDutchmanAirlinesContext> dbcontextOptions = new DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>()
            .UseInMemoryDatabase("FlyingDutchman").Options;
            _context = new FlyingDutchmanAirlinesContext(dbcontextOptions);

            _repository = new CustomerRepository(_context);
            Assert.IsNotNull(_repository);
        }

        [TestMethod]
        public async Task CreateCustomer_Failure_DatabaseError()
        {
            

          CustomerRepository repository = new CustomerRepository(null);     
          Assert.IsNotNull(repository);         
           bool result = await repository.CreateCustomer("Donald Knuth");     
           Assert.IsFalse(result);
        }
    }
}
