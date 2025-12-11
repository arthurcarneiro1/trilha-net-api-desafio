using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TarefasAPI.Data;
using TarefasAPI.Models;
using TarefasAPI.Models.Enums;

namespace TarefasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefasContext _context;

        public TarefaController(TarefasContext context)
        {
            _context = context;
        }

        // GET /Tarefa/{id}
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        // GET /Tarefa/ObterTodos
        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            return Ok(_context.Tarefas.ToList());
        }

        // GET /Tarefa/ObterPorTitulo?titulo=...
        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefas = _context.Tarefas
                .Where(t => t.Titulo.Contains(titulo))
                .ToList();

            return Ok(tarefas);
        }

        // GET /Tarefa/ObterPorData?data=2022-06-08
        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefas = _context.Tarefas
                .Where(t => t.Data.Date == data.Date)
                .ToList();

            return Ok(tarefas);
        }

        // GET /Tarefa/ObterPorStatus?status=Pendente
        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(StatusTarefa status)
        {
            var tarefas = _context.Tarefas
                .Where(t => t.Status == status)
                .ToList();

            return Ok(tarefas);
        }

        // POST /Tarefa
        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        // PUT /Tarefa/{id}
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefaAtualizada)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
                return NotFound();

            tarefa.Titulo = tarefaAtualizada.Titulo;
            tarefa.Descricao = tarefaAtualizada.Descricao;
            tarefa.Data = tarefaAtualizada.Data;
            tarefa.Status = tarefaAtualizada.Status;

            _context.Tarefas.Update(tarefa);
            _context.SaveChanges();

            return Ok(tarefa);
        }

        // DELETE /Tarefa/{id}
        [HttpDelete]
        public IActionResult Deletar(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
                return NotFound();

            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
