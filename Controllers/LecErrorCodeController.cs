using AuthenticationAndAuthorization.Dto;
using AuthenticationAndAuthorization.Repository;
using AutoMapper;
using DestekAybey.Models;
using Microsoft.AspNetCore.Mvc;


namespace AuthenticationAndAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecErrorCodeController : Controller
    {
        private readonly LecErrorCodeRepo _errorCodeRepository;
        private readonly LecLanguageRepo _languageRepository;
        private readonly LecProductSerieRepo _productSerieRepository;
        private readonly IMapper _mapper;

        public LecErrorCodeController(
        LecErrorCodeRepo errorCodeRepository, 
        LecLanguageRepo languageRepository, 
        LecProductSerieRepo productSerieRepository,
        IMapper mapper)
        {
            _errorCodeRepository = errorCodeRepository;
            _languageRepository = languageRepository;
            _productSerieRepository = productSerieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetErrorCodes()
        {
            var errorCodes = _mapper.Map<List<LecErrorCodeDto>>(_errorCodeRepository.GetErrorCodes());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(errorCodes);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetErrorCode(int id)
        {
            if (!_errorCodeRepository.ErrorCodeExists(id))
                return NotFound();
            
            var errorCode = _mapper.Map<LecErrorCodeDto>(_errorCodeRepository.GetErrorCode(id));

            if (!ModelState.IsValid)
                return BadRequest();
            
            return Ok(errorCode);
        }
        
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateErrorCode([FromQuery] int languageId, [FromQuery] int productSerieId, [FromBody]LecErrorCodeDto errorCodeCreate)
        {
            if (errorCodeCreate == null)
                return BadRequest(ModelState);
            
            var errorCodes = _errorCodeRepository.GetErrorCodes()
            .Where(ec => ec.ErrorCode == errorCodeCreate.ErrorCode)
            .FirstOrDefault();

            if (errorCodes != null)
            {
                ModelState.AddModelError("","Error Code already exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var errorCodeMap = _mapper.Map<LecErrorCode>(errorCodeCreate);

            errorCodeMap.LecLanguage = _languageRepository.GetLanguage(languageId);

            if (!_errorCodeRepository.CreateErrorCode(productSerieId, errorCodeMap))
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
        public IActionResult UpdateErrorCode(int id, 
        [FromQuery] string language, 
        [FromBody]LecErrorCodeDto updatedErrorCode)
        {
            if (updatedErrorCode == null)
                return BadRequest(ModelState);

            if (!_errorCodeRepository.ErrorCodeExists(id))
                return NotFound();
            
            if (!ModelState.IsValid)
                return BadRequest();
            
            var errorCodeMap = _mapper.Map<LecErrorCode>(updatedErrorCode);

            if (!_errorCodeRepository.UpdateErrorCode(errorCodeMap))
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
        public IActionResult DeleteErrorCode(int id)
        {
            if(!_errorCodeRepository.ErrorCodeExists(id))
                return NotFound();
            
            var errorCodeToDelete = _errorCodeRepository.GetErrorCode(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_errorCodeRepository.DeleteErrorCode(errorCodeToDelete))
                ModelState.AddModelError("", "Something went wrong");
            
            return NoContent();
        }

        [HttpGet("GetErrorCodesByErrorName")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetErrorCodesByErrorName(
        [FromQuery] int languageId,
        [FromQuery] int productSerieId,
        string errorName)
        {
                if (!_errorCodeRepository.ErrorNameExists(errorName))
                return NotFound();
            
            var errorCodes = _errorCodeRepository.GetErrorCodesByErrorName(languageId, productSerieId, errorName);

            if (!ModelState.IsValid)
                return BadRequest();
            
            return Ok(errorCodes);
        }

        [HttpGet("GetErrorNamesByErrorCode")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetErrorNamesByErrorCode(
        [FromQuery] int languageId,
        [FromQuery] int productSerieId,
        int errorCode)
        {
            if (!_errorCodeRepository.ErrorCodeExists(errorCode))
                return NotFound();

            var errorNames = _errorCodeRepository.GetErrorNamesByErrorCode(languageId, productSerieId, errorCode);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(errorNames);
        } 

        [HttpGet("GetErrorNamesErrorCodes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetErrorNamesErrorCodes(
        [FromQuery] int languageId,
        [FromQuery] int productSerieId)
        {
            var getAll = _errorCodeRepository.GetErrorNamesErrorCodes(languageId, productSerieId);
            
            return Ok(getAll);
        }

    }
}