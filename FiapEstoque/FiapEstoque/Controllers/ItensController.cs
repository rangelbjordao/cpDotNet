using FiapEstoque.Data;
using FiapEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FiapEstoque.Messaging;

namespace FiapEstoque.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ItensController
 : ControllerBase
{
    private readonly AppDbContext _ctx;
    public ItensController(
    AppDbContext ctx)
    => _ctx = ctx;

    [HttpGet]
    public async Task<IActionResult>
    GetAll()
    => Ok(await _ctx.Itens
    .ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult>
    GetById(int id)
    {
        var p = await _ctx.Itens
        .FindAsync(id);
        return p is null
        ? NotFound() : Ok(p);
    }

    [HttpPost]
    public async Task<IActionResult>
    Create(Item item)
    {
        _ctx.Itens.Add(item);
        await _ctx.SaveChangesAsync();
        RabbitMqProducer.Publish(item);
        return CreatedAtAction(
        nameof(GetById),
        new { id = item.Id },
        item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult>
 Update(int id, Item p)
    {
        if (id != p.Id)
            return BadRequest();
        _ctx.Entry(p).State =
        EntityState.Modified;
        await _ctx.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult>
 Delete(int id)
    {
        var p = await _ctx.Itens
        .FindAsync(id);
        if (p is null)
            return NotFound();
        _ctx.Itens.Remove(p);
        await _ctx.SaveChangesAsync();
        return NoContent();
    }
}
