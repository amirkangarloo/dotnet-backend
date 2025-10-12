using api.Dto.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepository.GetAllAsync();
            var response = stocks.Select(s => s.ToStockDto());

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByeId([FromRoute] int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDto();
            stockModel = await _stockRepository.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetByeId), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            var existingStock = await _stockRepository.UpdateAsync(id, stockDto);
            if (existingStock == null)
            {
                return NotFound();
            }

            return Ok(existingStock.ToStockDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var existingStock = await _stockRepository.DeleteAsync(id);
            if (existingStock == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}