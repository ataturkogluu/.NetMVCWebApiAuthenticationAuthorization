using AuthenticationAndAuthorization.Dto;
using AuthenticationAndAuthorization.Repository;
using AutoMapper;
using DestekAybey.Models;
using Microsoft.AspNetCore.Mvc;


namespace AuthenticationAndAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecFaqController : Controller
    {
        private readonly LecFaqRepo _faqRepository;
        private readonly LecLanguageRepo _languageRepository;
        private readonly LecProductSerieRepo _productSerieRepository;
        private readonly IMapper _mapper;

        public LecFaqController(
        LecFaqRepo faqRepository, 
        LecLanguageRepo languageRepository, 
        LecProductSerieRepo productSerieRepository,
        IMapper mapper)
        {
            _faqRepository = faqRepository;
            _languageRepository = languageRepository;
            _productSerieRepository = productSerieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetFaqs()
        {
            var faqs = _mapper.Map<List<LecFaqDto>>(_faqRepository.GetFaqs());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(faqs);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetFaq(int id)
        {
            if (!_faqRepository.FaqExists(id))
                return NotFound();
            
            var faq = _mapper.Map<LecFaqDto>(_faqRepository.GetFaq(id));

            if (!ModelState.IsValid)
                return BadRequest();
            
            return Ok(faq);
        }
        
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateFaq(
            [FromQuery] int productSerieId,
            [FromQuery] int languageId, 
            [FromBody]LecFaqDto faqCreate)
        {
            if (faqCreate == null)
                return BadRequest(ModelState);
            
            var faqs = _faqRepository.GetFaqs()
            .Where(l => l.Title.Trim().ToUpper() == faqCreate.Title.TrimEnd().ToUpper())
            .FirstOrDefault();

            if (faqs != null)
            {
                ModelState.AddModelError("","faq already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var faqMap = _mapper.Map<LecFaq>(faqCreate);

            faqMap.LecLanguage = _languageRepository.GetLanguage(languageId);

            faqMap.LecProductSerie = _productSerieRepository.GetProductSerie(productSerieId);

            if (!_faqRepository.CreateFaq(faqMap))
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
        public IActionResult UpdateFaq(int id, 
        [FromQuery] int productSerieId, 
        [FromQuery] string language, 
        [FromBody]LecFaqDto updatedFaq)
        {
            if (updatedFaq == null)
                return BadRequest(ModelState);

            if (!_faqRepository.FaqExists(id))
                return NotFound();
            
            if (!ModelState.IsValid)
                return BadRequest();
            
            var faqMap = _mapper.Map<LecFaq>(updatedFaq);

            if (!_faqRepository.UpdateFaq(faqMap))
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
        public IActionResult DeleteFaq(int id)
        {
            if(!_faqRepository.FaqExists(id))
                return NotFound();
            
            var faqToDelete = _faqRepository.GetFaq(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_faqRepository.DeleteFaq(faqToDelete))
                ModelState.AddModelError("", "Something went wrong");
            
            return NoContent();
        }
    }
}