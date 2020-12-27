using Microsoft.AspNetCore.Mvc;
using netcore_nhibernate.Models;
using netcore_nhibernate.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace netcore_nhibernate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductsController(NHibernate.ISession session) => _productRepository = new ProductRepository(session);

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_productRepository.FindAll().ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _productRepository.FindById(id);

            if (product.Result == null)
                return NotFound();

            return Ok(product.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            if (product == null)
                return BadRequest();

            await _productRepository.Add(product);
            return Created(nameof(Get), product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = _productRepository.FindById(id);
            if (product == null)
                return BadRequest();

            await _productRepository.Remove(id);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            if (product == null)
                return BadRequest();

            await _productRepository.Update(product);

            return Ok(product);
        }
    }
}