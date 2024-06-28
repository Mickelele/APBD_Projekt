using System.ComponentModel.DataAnnotations;
using APBD_Projekt.Models.DTO_s;
using APBD_Projekt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Projekt.Controllers;

[Authorize(Roles = "user, admin")]
[ApiController]
[Route("api/przychod")]
public class PrzychodController : ControllerBase
{

    private readonly PrzychodService _przychodService;

    public PrzychodController(PrzychodService przychodService)
    {
        _przychodService = przychodService;
    }

    [HttpPost("/PokazBiezacyPrzychod")]
    public async Task<IActionResult> ObliczBiezacyPrzychod([FromBody] PrzychodDTO przychodDto)
    {
        PrzychodDTOReturn przychod = await _przychodService.ObliczBiezacyPrzychod(przychodDto);

        return Ok(przychod);
    }
    
    
    [HttpPost("/PokazPrzewidywanyPrzychod")]
    public async Task<IActionResult> ObliczPrzewidywanyPrzychod([FromBody] PrzychodDTO przychodDto)
    {
        PrzychodDTOReturn przychod = await _przychodService.ObliczPrzewidywanyPrzychod(przychodDto);

        return Ok(przychod);
    }
    

}