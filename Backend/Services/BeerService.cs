using AutoMapper;
using Backend.DTOs.Beer;
using Backend.Models;
using Backend.Repository;

namespace Backend.Services;

public class BeerService: ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
{
    private IRepository<Beer> _repository;
    private IMapper _mapper;
    public List<string> Errors { get; }
    
    public BeerService(
        IRepository<Beer> repository,
        IMapper mapper
    ){
        _repository = repository;
        _mapper = mapper;
        Errors = [];
    }

    public async Task<IEnumerable<BeerDto>> Get()
    {
        var beers =await _repository.Get();
        return beers.Select(b => _mapper.Map<BeerDto>(b));
    }

    public async Task<BeerDto?> GetById(int id)
    {
        var beer = await _repository.GetById(id);
        if (beer != null)
        {
            return _mapper.Map<BeerDto>(beer);
        }

        return null;

    }

    public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
    {
        var beer = _mapper.Map<Beer>(beerInsertDto);
        
        await _repository.Add(beer);
        await _repository.Save();
        
        return _mapper.Map<BeerDto>(beerInsertDto);
    }

    public async Task<BeerDto?> Update(int id, BeerUpdateDto beerUpdateDto)
    {
        var beer = await _repository.GetById(id);
        if (beer == null) return null;
        beer = _mapper.Map(beerUpdateDto, beer);
            
        _repository.Update(beer);
        await _repository.Save();
            
        return _mapper.Map<BeerDto>(beer);
    }

    public async Task<BeerDto?> Delete(int id)
    {
        var beer = await _repository.GetById(id);
        if (beer == null) return null;
        var beerDto = _mapper.Map<BeerDto>(beer);
            
        _repository.Delete(beer);
        await _repository.Save();

        return beerDto;

    }

    public bool Validate(BeerInsertDto dto)
    {
        if (_repository.Search(beer => beer.Name == dto.Name).Count() > 0
            ) 
        {
            Errors.Add("No se puede crear una cerveza con el mismo nombre.");
            return false;
        }
        return true;
    }

    public bool Validate(BeerUpdateDto dto)
    {
        if (_repository.Search(beer => beer.Name == dto.Name
                                       && dto.Id != beer.BeerID).Count() <= 0) return true;
        Errors.Add("No se puede crear una cerveza con el mismo nombre.");
        return false;
    }
}