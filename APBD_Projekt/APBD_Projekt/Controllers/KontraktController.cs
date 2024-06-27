using APBD_Projekt.Models.DTO_s;
using APBD_Projekt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Projekt.Controllers;

[Authorize(Roles = "user,admin")]
[ApiController]
[Route("api/kontrakty")]
public class KontraktController : ControllerBase
{

    private readonly KontraktService _kontraktService;

    public KontraktController(KontraktService kontraktService)
    {
        _kontraktService = kontraktService;
    }

    [HttpPost("/DodajKontrakt")]
    public async Task<IActionResult> DodajKontrakt(KontraktDTO kontraktDto)
    {

        if (!await _kontraktService.CzyKlientIstnieje(kontraktDto))
        {
            return NotFound($"Klient o id {kontraktDto.ClientID} nie istnieje.");
        }
        
        if (!await _kontraktService.CzyOprogramowanieIstnieje(kontraktDto.OprogramowanieID))
        {
            return NotFound($"Oprogramowanie o id {kontraktDto.OprogramowanieID} nie istnieje.");
        }

        if (await _kontraktService.CzyKlientNieMaJuzAktywnegoKontrkatu(kontraktDto))
        {
            return BadRequest($"Klient o id {kontraktDto.ClientID} ma juz aktywny kontrakt na to oprogramowanie");
        }
        
        if (!_kontraktService.CzyDataMiesciSieWZakresie(kontraktDto))
        {
            return BadRequest($"Data poza przedzialem 3 - 30 dni lub zla liczba lat wsparcie.");
        }
        
        
        
        
        await _kontraktService.DodajKontrakt(kontraktDto);
        return Created();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> UsunKontrakt(int id)
    {
        if (!await _kontraktService.CzyKontraktIstnieje(id))
        {
            return BadRequest($"Kontrakt o id {id} nie istnieje.");
        }

        await _kontraktService.UsunKontrakt(id);

        return StatusCode(204);
    }
    
    
}