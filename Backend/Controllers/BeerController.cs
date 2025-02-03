using Backend.DTOs.Beer;
using Backend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private IValidator _validatorInsert;
        private IValidator _validatorUpdate;
        private ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> _service;
        
        public BeerController(
            IValidator<BeerInsertDto> validatorInsert,
            IValidator<BeerUpdateDto> validatorUpdate,
            [FromKeyedServices("beerService")] ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> service
        ){
            _validatorInsert = validatorInsert;
            _validatorUpdate = validatorUpdate;
            _service = service;
        }
        
        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() => await _service.Get();
        
        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beerDto = await _service.GetById(id);
            
            return beerDto is null ? NotFound() : Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            var validationContext= new ValidationContext<object>(beerInsertDto);
            var validationResult = await _validatorInsert.ValidateAsync(validationContext);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_service.Validate(beerInsertDto))
            {
                return BadRequest(_service.Errors);
            }

            var beerDto = await _service.Add(beerInsertDto);
            
            return CreatedAtAction(nameof(GetById), new { id = beerDto.Id }, beerDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var validationContext= new ValidationContext<object>(beerUpdateDto);
            var validationResult = await _validatorUpdate.ValidateAsync(validationContext);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            if (!_service.Validate(beerUpdateDto))
            {
                return BadRequest(_service.Errors);
            }

            var beerDto = await _service.Update(id, beerUpdateDto);
            
            return beerDto is null ? NotFound() : Ok(beerDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> Delete(int id)
        {
            var beerDto = await _service.Delete(id);
            
            return beerDto is null ? NotFound() : Ok(beerDto);
        }
        
    }
}
