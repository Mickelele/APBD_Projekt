using APBD_Projekt.Models;
using APBD_Projekt.Models.DTO_s;
using APBD_Projekt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Projekt.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/subskrybcje")]
public class SubskrybcjaController : ControllerBase
{


    private readonly SubskrybcjaService _subskrybcjaService;

    public SubskrybcjaController(SubskrybcjaService subskrybcjaService)
    {
        _subskrybcjaService = subskrybcjaService;
    }



    [HttpPost("/ZakupNowejSubskrybcji/{id:int}/{type}")]
    public async Task<IActionResult> ZakupNowejSubskrybcji(SubskrybcjaDTO subskrybcjaDto, int id, string type)
    {

        await _subskrybcjaService.SprawdzCzasOdnowienia(subskrybcjaDto);
        if (!await _subskrybcjaService.CzyKlientIstnieje(id, type))
        {
            return NotFound($"Klient o ID {id} nie istnieje jako {type}");
        }

        if (!await _subskrybcjaService.CzyOprogramowanieIstnieje(subskrybcjaDto))
        {
            return NotFound($"Oprogramowanie o ID {id} nie istnieje.");
        }

        if (await _subskrybcjaService.CzyKlientMaJuzAktywneOprogramowanie(subskrybcjaDto, id, type))
        {
            return BadRequest($"Klient ma juz aktywna subskrybcje na to oprogramowanie");
        }

        await _subskrybcjaService.ZakupNowejSubskrybcji(subskrybcjaDto, id, type);

        await _subskrybcjaService.StworzNowaSubskrybcje(subskrybcjaDto, id, type);
        
        return Created();
    }


    [HttpPost("/ZaplacZaSubskrybcje/{id:int}/{type}")]
    public async Task<IActionResult> ZaplacZaSubskrybcje(SubskrybcjaDTOPlatnosc subskrybcjaDtoPlatnosc, int id, string type)
    {

        if (!await _subskrybcjaService.CzySubskrybcjaIstnieje(subskrybcjaDtoPlatnosc, id, type))
        {
            return BadRequest("Subskrybcja nieistnieje.");
        }

        if (await _subskrybcjaService.CzyCenaSieZgadza(subskrybcjaDtoPlatnosc, id, type))
        {
            return BadRequest("Niepoprawna cena.");
        }

        await _subskrybcjaService.ZaplacZaSubskrybcje(subskrybcjaDtoPlatnosc, id, type);

        return Created();
    }
    
    

}