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
        [HttpGet("/")] //Dessa maneira, ja esta com a rota definida
        //[Route("/")] //Define a rota
        public List<TodoModel> Get([FromServices] AppDbContext context)
        {
            return context.Todos.ToList();
        }

        [HttpPost("/")]
        public TodoModel Post
        (
            [FromBody] TodoModel todo, //espera que os dados da tarefa venham no corpo da requisição ([FromBody]
            [FromServices] AppDbContext context //injeta automaticamente o banco de dados via injeção de dependência ([FromServices]).
        )
        {
            context.Todos.Add(todo);//Adiciona o novo item (todo) na tabela Todos do banco.
            context.SaveChanges();

            return todo;
        }
    }


}