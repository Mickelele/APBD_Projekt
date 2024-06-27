using System.ComponentModel.DataAnnotations;
using APBD_Projekt.Models.DTO_s;
using APBD_Projekt.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Projekt.Controllers;

[ApiController]
[Route("api/przychod")]
public class PrzychodController : ControllerBase
{

    private readonly PrzychodService _przychodService;

    public PrzychodController(PrzychodService przychodService)
    {
        _przychodService = przychodService;
    }

    [HttpPost("biezacy")]
    public async Task<IActionResult> ObliczBiezacyPrzychod([FromBody] PrzychodDTO przychodDto)
    {
        PrzychodDTOReturn przychod = await _przychodService.ObliczBiezacyPrzychod(przychodDto);

        return Ok(przychod);
    }
    
    
    [HttpPost("przewidywany")]
    public async Task<IActionResult> ObliczPrzewidywanyPrzychod([FromBody] PrzychodDTO przychodDto)
    {
        PrzychodDTOReturn przychod = await _przychodService.ObliczPrzewidywanyPrzychod(przychodDto);

        return Ok(przychod);
    }
    

}