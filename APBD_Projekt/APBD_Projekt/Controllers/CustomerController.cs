using System.Data;
using System.Threading.Channels;
using APBD_Projekt.Models.DTO_s;
using APBD_Projekt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Projekt.Controllers;


[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{

    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }
    
    [Authorize(Roles = "user,admin")]
    [HttpGet]
    public async Task<IActionResult> PokazKlientowFizycznych()
    {
        var result = await _customerService.PokazKlientowFizycznych();
        return Ok(result);
    }

    [HttpPost("/WstawKlientaFizycznego")]
    public async Task<IActionResult> WstawKlientaFizycznego([FromBody]KlientFizycznyDTO klientFizycznyDto)
    {
        var result = await _customerService.SprawdzIstnienieIZaktualizuj(klientFizycznyDto);

        if (result == 1)
        {
            return NotFound($"Użytkownik o PESEL {klientFizycznyDto.PESEL} już istnieje.");
        }
        if (result == 2)
        {
            return Created();
        }

        await _customerService.WstawKlientaFizycznego(klientFizycznyDto);
        return Created();
    }

    
    [Authorize(Roles = "admin")]
    [HttpPost("/AktualizujKlientaFizycznego/{id:int}")]
    public async Task<IActionResult> AktualizujKlientaFizycznego([FromBody] KlientFizycznyDTOUpdate klientFizycznyDto, int id)
    {
        if (!await _customerService.CzyKlientIstnieje(id))
        {
            return NotFound($"Użytkownik o ID {id} nie istnieje.");
        }
        
        await _customerService.AktualizujKlientaFizycznego(klientFizycznyDto);
        return Created();
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> UsunKlientaFizycznego(int id)
    {
        if (!await _customerService.CzyKlientIstnieje(id))
        {
            return NotFound($"Użytkownik o ID {id} nie istnieje.");
        }

        await _customerService.UsunKlientaFizycznego(id);
        return StatusCode(204);
    }
    

}