using AuthenticationAndAuthorization.Dto;
using AuthenticationAndAuthorization.Repository;
using AutoMapper;
using DestekAybey.Models;
using Microsoft.AspNetCore.Mvc;


namespace AuthenticationAndAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecLanguageController : Controller
    {
        private readonly LecLanguageRepo _languageRepository;
        private readonly IMapper _mapper;
        public LecLanguageController(LecLanguageRepo languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetLanguages()
        {
            var languages = _mapper.Map<List<LecLanguageDto>>(_languageRepository.GetLanguages());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(languages);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetLanguage(int id)
        {
            if (!_languageRepository.LanguageExists(id))
                return NotFound();
            
            var language = _mapper.Map<LecLanguageDto>(_languageRepository.GetLanguage(id));

            if (!ModelState.IsValid)
                return BadRequest();
            
            return Ok(language);
        }
        
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateLanguage([FromBody] LecLanguageDto languageCreate)
        {
            if (languageCreate == null)
                return BadRequest(ModelState);
            
            var language = _languageRepository.GetLanguages()
            .Where(l => l.Language.Trim().ToUpper() == languageCreate.Language.TrimEnd().ToUpper())
            .FirstOrDefault();

            if (language != null)
            {
                ModelState.AddModelError("","Language already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var languageMap = _mapper.Map<LecLanguage>(languageCreate);

            if (!_languageRepository.CreateLanguage(languageMap))
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
        public IActionResult UpdateLanguage(int id, [FromBody]LecLanguageUpdateDto updatedLanguage)
        {
            var updateLanguage = new LecLanguage();
            //lng içini db den select ile alacak.
            updateLanguage.Language = updatedLanguage.Language;
            _languageRepository.UpdateLanguage(updateLanguage);

            if (updatedLanguage == null)
                return BadRequest(ModelState);
            
            if (!_languageRepository.LanguageExists(id))
                return NotFound();
            
            if (!ModelState.IsValid)
                return BadRequest();
            
            var languageMap = _mapper.Map<LecLanguage>(updatedLanguage);

            if (!_languageRepository.UpdateLanguage(languageMap))
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
        public IActionResult DeleteLanguage(int id)
        {
            if(!_languageRepository.LanguageExists(id))
                return NotFound();

            var languageToDelete = _languageRepository.GetLanguage(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_languageRepository.DeleteLanguage(languageToDelete))
                ModelState.AddModelError("", "Something went wrong");
            
            return NoContent();
        }
    }

}

