using APBD_Projekt.Models.DTO_s;
using APBD_Projekt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Projekt.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/companies")]
public class FirmaController : ControllerBase
{
    private readonly FirmaService _firmaService;

    public FirmaController(FirmaService firmaService)
    {
        _firmaService = firmaService;
    }
    

    [HttpGet("/PokazFirmy")]
    public async Task<IActionResult> PokazFirmy()
    {
        var result = await _firmaService.PokazFirmy();
        
        return Ok(result);
    }
    
    

    [HttpPost("/WstawFirme")]
    public async Task<IActionResult> WstawFirme([FromBody]FirmaDTO firmaDto)
    {

        if (await _firmaService.SprawdzCzyFirmaIstnieje(firmaDto))
        {
            return NotFound($"Firma o KRS {firmaDto.KRS} juz istnieje.");
        }

        await _firmaService.WstawFirme(firmaDto);

        return Created();
    }
    

    [HttpPost("/AktualizujDaneFirmy/{id:int}")]
    public async Task<IActionResult> AktualizujDaneFirmy([FromBody]FirmaDTOUpdate firmaDto, int id)
    {

        if (!await _firmaService.SprawdzCzyFirmaIstnieje(id))
        {
            return NotFound($"Firma o id {id} nie istnieje.");
        }

        await _firmaService.AktualizujDaneFirmy(firmaDto, id);

        return Created();
    }
    
    
    
}