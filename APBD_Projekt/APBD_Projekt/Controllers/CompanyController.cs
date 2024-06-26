﻿using APBD_Projekt.Models.DTO_s;
using APBD_Projekt.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Projekt.Controllers;

[ApiController]
[Route("api/companies")]
public class CompanyController : ControllerBase
{
    private readonly CompanyService _companyService;

    public CompanyController(CompanyService companyService)
    {
        _companyService = companyService;
    }
    
    
    [HttpGet("/PokazFirmy")]
    public async Task<IActionResult> PokazFirmy()
    {
        var result = await _companyService.PokazFirmy();
        
        return Ok(result);
    }
    
    
    [HttpPost("/WstawFirme")]
    public async Task<IActionResult> WstawKlientaFizycznego([FromBody]FirmaDTO firmaDto)
    {

        if (await _companyService.SprawdzCzyFirmaIstnieje(firmaDto))
        {
            return NotFound($"Firma o KRS {firmaDto.KRS} juz istnieje.");
        }

        await _companyService.WstawKlientaFizycznego(firmaDto);

        return Created();
    }
    
    
    [HttpPost("/AktualizujDaneFirmy/{id:int}")]
    public async Task<IActionResult> AktualizujDaneFirmy([FromBody]FirmaDTOUpdate firmaDto, int id)
    {

        if (!await _companyService.SprawdzCzyFirmaIstnieje(id))
        {
            return NotFound($"Firma o id {id} nie istnieje.");
        }

        await _companyService.AktualizujDaneFirmy(firmaDto, id);

        return Created();
    }
    
    
    
}