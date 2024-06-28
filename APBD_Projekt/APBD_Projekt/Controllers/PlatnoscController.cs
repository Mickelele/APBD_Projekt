using APBD_Projekt.Models.DTO_s;
using APBD_Projekt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Projekt.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/platnosci")]
public class PlatnoscController : ControllerBase
{

    private readonly PlatnoscService _platnoscService;
    private readonly KlientFizycznyService _klientFizycznyService;
    private readonly KontraktService _kontraktService;
    private readonly FirmaService _firmaService;
   

    public PlatnoscController(PlatnoscService platnoscService, KlientFizycznyService klientFizycznyService, KontraktService kontraktService, FirmaService firmaService)
    {
        _platnoscService = platnoscService;
        _klientFizycznyService = klientFizycznyService;
        _kontraktService = kontraktService;
        _firmaService = firmaService;
    }

    [HttpGet("/PokazPlatnosci")]
    public async Task<IActionResult> PokazPlatnosci()
    {
        return Ok(await _platnoscService.PokazPlatnosci());
    }

    [HttpPost("/DodajPlatnosc")]
    public async Task<IActionResult> DodajPlatnosc([FromBody] PlatnoscDTO platnoscDto)
    {
        if (!(await _klientFizycznyService.CzyKlientIstnieje(platnoscDto.KlientID) ||  await _firmaService.SprawdzCzyFirmaIstnieje(platnoscDto.KlientID)))
        {
            return NotFound($"Klient o id {platnoscDto.KlientID} nie istnieje.");
        }

        if (!await _platnoscService.CzyKontraktDanegoKlientaIstnieje(platnoscDto))
        {
            return NotFound($"Kontrakt o id {platnoscDto.KontraktID} klienta lub id {platnoscDto.KlientID} nie istnieje."); 
        }

        if (platnoscDto.IleZaplacono <= 0)
        {
            return BadRequest($"Niepoprawna kwota");
        }

        if (await _platnoscService.czyKwotaJestZaWysoka(platnoscDto))
        {
            return BadRequest("Kwota na platnosci przekracza pozostale naleznosci.");
        }

        if (await _platnoscService.czyTerminMinal(platnoscDto))
        {
            await _platnoscService.PrzygotujNowaUmowe(platnoscDto);
            return BadRequest("Termin minal umowa nie aktywna. Przygotowano nowa umowe");
        }

        await _platnoscService.DodajPlatnosc(platnoscDto);

        return Created();
    }


}