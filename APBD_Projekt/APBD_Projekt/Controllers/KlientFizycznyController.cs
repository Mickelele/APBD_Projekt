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
public class KlientFizycznyController : ControllerBase
{

    private readonly KlientFizycznyService _klientFizycznyService;

    public KlientFizycznyController(KlientFizycznyService klientFizycznyService)
    {
        _klientFizycznyService = klientFizycznyService;
    }
    
    [Authorize(Roles = "user, admin")]
    [HttpGet("/PokazKlientowFizycznych")]
    public async Task<IActionResult> PokazKlientowFizycznych()
    {
        var result = await _klientFizycznyService.PokazKlientowFizycznych();
        return Ok(result);
    }

    
    [Authorize(Roles = "user, admin")]
    [HttpPost("/WstawKlientaFizycznego")]
    public async Task<IActionResult> WstawKlientaFizycznego([FromBody]KlientFizycznyDTO klientFizycznyDto)
    {
        var result = await _klientFizycznyService.SprawdzIstnienieIZaktualizuj(klientFizycznyDto);

        if (result == 1)
        {
            return NotFound($"Użytkownik o PESEL {klientFizycznyDto.PESEL} już istnieje.");
        }
        if (result == 2)
        {
            return Created();
        }

        await _klientFizycznyService.WstawKlientaFizycznego(klientFizycznyDto);
        return Created();
    }

    
    [Authorize(Roles = "admin")]
    [HttpPost("/AktualizujKlientaFizycznego/{id:int}")]
    public async Task<IActionResult> AktualizujKlientaFizycznego([FromBody] KlientFizycznyDTOUpdate klientFizycznyDto, int id)
    {
        if (!await _klientFizycznyService.CzyKlientIstnieje(id))
        {
            return NotFound($"Użytkownik o ID {id} nie istnieje.");
        }
        
        await _klientFizycznyService.AktualizujKlientaFizycznego(klientFizycznyDto, id);
        return Created();
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("/UsunKlientaFizycznego/{id:int}")]
    public async Task<IActionResult> UsunKlientaFizycznego(int id)
    {
        if (!await _klientFizycznyService.CzyKlientIstnieje(id))
        {
            return NotFound($"Użytkownik o ID {id} nie istnieje.");
        }

        await _klientFizycznyService.UsunKlientaFizycznego(id);
        return StatusCode(204);
    }
    

}