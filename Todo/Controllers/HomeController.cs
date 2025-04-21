using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController] //habilita recursos
    [Route("home")] //prefixo de rota  
    public class HomeController : ControllerBase //classe base para APIs (sem suporte a views, diferente de Controller
    {
        [HttpGet("/")] //dessa maneira, ja esta com a rota definida
        //[Route("/")] //define a rota
        public IActionResult Get([FromServices] AppDbContext context)
            => Ok(context.Todos.ToList());


        [HttpGet("/{id:int}")] //diz que o parâmetro id deve ser um número inteiro.
        public IActionResult GetById(
            [FromRoute] int id, //pega o id diretamente da URL.
            [FromServices] AppDbContext context)
        {
            var todos = context.Todos.FirstOrDefault(x => x.Id == id);//busca no banco a primeira tarefa com o ID informado
            if (todos == null)
                return NotFound();

            return Ok(todos);
        }

        [HttpPost("/")]
        public IActionResult Post(
            [FromBody] TodoModel todo, //espera que os dados da tarefa venham no corpo da requisição ([FromBody]
            [FromServices] AppDbContext context) //injeta automaticamente o banco de dados via injeção de dependência ([FromServices]).

        {
            context.Todos.Add(todo);//adiciona o novo item (todo) na tabela Todos do banco.
            context.SaveChanges();

            return Created($"/{todo.Id}", todo);
        }

        [HttpPut("/{id:int}")]
        public IActionResult Put(
            [FromRoute] int id,
            [FromBody] TodoModel todo,
            [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return NotFound();

            model.Title = todo.Title; //se a condicao for falsa, ele recebe o que esta atualmente no body   
            model.Done = todo.Done;

            context.Todos.Update(model);
            context.SaveChanges();
            return Ok(model);
        }

        [HttpDelete("/{id:int}")]
        public IActionResult Delete(
            [FromRoute] int id,
            [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return NotFound();

            context.Todos.Remove(model);
            context.SaveChanges();
            return Ok(model);
        }

    }
    //IActionResult é uma interface do ASP.NET Core que representa o resultado de uma ação em um controller.
    /*
    Dados (como objetos JSON)

    Status HTTP apropriados (200, 404, 201, etc.)

    Respostas personalizadas
    */

}