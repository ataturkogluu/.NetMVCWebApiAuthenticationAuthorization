using AuthenticationAndAuthorization.Dto;
using AuthenticationAndAuthorization.Repository;
using AutoMapper;
using DestekAybey.Models;
using Microsoft.AspNetCore.Mvc;


namespace AuthenticationAndAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecProductSerieController : Controller
    {
        private readonly LecProductSerieRepo _productSerieRepository;
        private readonly IMapper _mapper;
        public LecProductSerieController(LecProductSerieRepo productSerieRepository, IMapper mapper)
        {
            _productSerieRepository = productSerieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetProductSeries()
        {
            var productSeries = _mapper.Map<List<LecProductSerieDto>>(_productSerieRepository.GetProductSeries());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(productSeries);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetProductSerie(int id)
        {
            if (!_productSerieRepository.ProductSerieExists(id))
                return NotFound();
            
            var productSerie = _mapper.Map<LecProductSerieDto>(_productSerieRepository.GetProductSerie(id));

            if (!ModelState.IsValid)
                return BadRequest();
            
            return Ok(productSerie);
        }
        
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProductSerie([FromBody]LecProductSerieDto productSerieCreate)
        {
            if (productSerieCreate == null)
                return BadRequest(ModelState);
            
            var productSerie = _productSerieRepository.GetProductSeries()
            .Where(l => l.ProductSerie.Trim().ToUpper() == productSerieCreate.ProductSerie.TrimEnd().ToUpper())
            .FirstOrDefault();

            if (productSerie != null)
            {
                ModelState.AddModelError("","Product Serie already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var productSerieMap = _mapper.Map<LecProductSerie>(productSerieCreate);

            if (!_productSerieRepository.CreateProductSerie(productSerieMap))
            {
                ModelState.AddModelError("","Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProductSerie(int id, [FromBody]LecProductSerieDto updatedProductSerie)
        {
            if (updatedProductSerie == null)
                return BadRequest(ModelState);

            if (!_productSerieRepository.ProductSerieExists(id))
                return NotFound();
            
            if (!ModelState.IsValid)
                return BadRequest();
            
            var productSerieMap = _mapper.Map<LecProductSerie>(updatedProductSerie);

            if (!_productSerieRepository.UpdateProductSerie(productSerieMap))
            {
                ModelState.AddModelError("","Something went wrong");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProductSerie(int id)
        {
            if(!_productSerieRepository.ProductSerieExists(id))
                return NotFound();

            var productSerieToDelete = _productSerieRepository.GetProductSerie(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_productSerieRepository.DeleteProductSerie(productSerieToDelete))
                ModelState.AddModelError("", "Something went wrong");
            
            return NoContent();
        }
    }
}