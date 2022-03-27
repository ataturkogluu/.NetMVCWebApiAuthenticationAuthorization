using AuthenticationAndAuthorization.Dto;
using AuthenticationAndAuthorization.Repository;
using AutoMapper;
using DestekAybey.Models;
using Microsoft.AspNetCore.Mvc;


namespace AuthenticationAndAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecLangHtmlTitleController : Controller
    {
        private readonly LecLangHtmlTitleRepo _htmlTitleRepository;
        private readonly LecLanguageRepo _languageRepository;
        private readonly IMapper _mapper;

        public LecLangHtmlTitleController(
        LecLangHtmlTitleRepo htmlTitleRepository, 
        LecLanguageRepo languageRepository, 
        IMapper mapper)
        {
            _htmlTitleRepository = htmlTitleRepository;
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetHtmlTitles()
        {
            var htmlTitles = _mapper.Map<List<LecLangHtmlTitleDto>>(_htmlTitleRepository.GetHtmlTitles());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(htmlTitles);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetHtmlTitle(int id)
        {
            if (!_htmlTitleRepository.HtmlTitleExists(id))
                return NotFound();
            
            var htmlTitle = _mapper.Map<LecLangHtmlTitleDto>(_htmlTitleRepository.GetHtmlTitle(id));

            if (!ModelState.IsValid)
                return BadRequest();
            
            return Ok(htmlTitle);
        }
        
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateHtmlTitle([FromQuery] int languageId, [FromBody]LecLangHtmlTitleDto htmlTitleCreate)
        {
            if (htmlTitleCreate == null)
                return BadRequest(ModelState);
            
            var htmlTitles = _htmlTitleRepository.GetHtmlTitles()
            .Where(l => l.Name.Trim().ToUpper() == htmlTitleCreate.Name.TrimEnd().ToUpper())
            .FirstOrDefault();

            if (htmlTitles != null)
            {
                ModelState.AddModelError("","Html title already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var htmlTitleMap = _mapper.Map<LecLangHtmlTitle>(htmlTitleCreate);

            htmlTitleMap.LecLanguage = _languageRepository.GetLanguage(languageId);

            if (!_htmlTitleRepository.CreateHtmlTitle(htmlTitleMap))
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
        public IActionResult UpdateHtmlTitle(int id, 
        [FromQuery] string language, 
        [FromBody]LecLangHtmlTitleDto updatedHtmlTitle)
        {
            if (updatedHtmlTitle == null)
                return BadRequest(ModelState);

            if (!_htmlTitleRepository.HtmlTitleExists(id))
                return NotFound();
            
            if (!ModelState.IsValid)
                return BadRequest();
            
            var htmlTitleMap = _mapper.Map<LecLangHtmlTitle>(updatedHtmlTitle);

            if (!_htmlTitleRepository.UpdateHtmlTitle(language, htmlTitleMap))
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
        public IActionResult DeleteHtmlTitle(int id)
        {
            if(!_htmlTitleRepository.HtmlTitleExists(id))
                return NotFound();

            var htmlTitleToDelete = _htmlTitleRepository.GetHtmlTitle(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_htmlTitleRepository.DeleteHtmlTitle(htmlTitleToDelete))
                ModelState.AddModelError("", "Something went wrong");
            
            return NoContent();
        }
    }
}