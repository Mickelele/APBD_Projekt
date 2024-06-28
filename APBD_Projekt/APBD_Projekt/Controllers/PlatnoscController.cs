using APBD_Projekt.Models.DTO_s;
using APBD_Projekt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Projekt.Controllers;

[Authorize(Roles = "user,admin")]
[ApiController]
[Route("api/platnosci")]
public class PlatnoscController : ControllerBase
{

    private readonly PlatnoscService _platnoscService;
    private readonly CustomerService _customerService;
    private readonly KontraktService _kontraktService;
    private readonly CompanyService _companyService;
   

    public PlatnoscController(PlatnoscService platnoscService, CustomerService customerService, KontraktService kontraktService, CompanyService companyService)
    {
        _platnoscService = platnoscService;
        _customerService = customerService;
        _kontraktService = kontraktService;
        _companyService = companyService;
    }

    [HttpGet("/PokazPlatnosci")]
    public async Task<IActionResult> PokazPlatnosci()
    {
        return Ok(await _platnoscService.PokazPlatnosci());
    }

    [HttpPost("/DodajPlatnosc")]
    public async Task<IActionResult> DodajPlatnosc([FromBody] PlatnoscDTO platnoscDto)
    {
        if (!(await _customerService.CzyKlientIstnieje(platnoscDto.KlientID) ||  await _companyService.SprawdzCzyFirmaIstnieje(platnoscDto.KlientID)))
        {
            return NotFound($"Klient o id {platnoscDto.KlientID} nie istnieje.");
        }

        if (!await _platnoscService.CzyKontraktDanegoKlientaIstnieje(platnoscDto))
        {
            return NotFound($"Kontrakt o id {platnoscDto.KontraktID} klienta o id {platnoscDto.KlientID} nie istnieje."); 
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