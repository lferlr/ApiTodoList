using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Commands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;

namespace Todo.Domain.Api.Controllers;

[ApiController]
[Route("v1/todos")]
public class TodoController : ControllerBase
{
    [HttpGet("")]
    public IEnumerable<TodoItem> GetAll(string user, [FromServices] ITodoRepository repository)
    {
        return repository.GetAll(user);
    }
    
    [HttpGet("done")]
    public IEnumerable<TodoItem> GetAllDone(string user, [FromServices] ITodoRepository repository)
    {
        return repository.GetAllDone(user);
    }
    
    [HttpGet("undone")]
    public IEnumerable<TodoItem> GetAllUndone(string user, [FromServices] ITodoRepository repository)
    {
        return repository.GetAllUndone(user);
    }
    
    [HttpGet("done/today")]
    public IEnumerable<TodoItem> GetDoneForToday(string user, [FromServices] ITodoRepository repository)
    {
        return repository.GetByPeriod(user, DateTime.Now.Date, true);
    }
    
    [HttpGet("undone/today")]
    public IEnumerable<TodoItem> GetInactiveForToday(string user, [FromServices] ITodoRepository repository)
    {
        return repository.GetByPeriod(user, DateTime.Now.Date, false);
    }
    
    [HttpGet("done/tomorrow")]
    public IEnumerable<TodoItem> GetDoneForTomorrow(string user, [FromServices] ITodoRepository repository)
    {
        return repository.GetByPeriod(user, DateTime.Now.Date.AddDays(1), true);
    }
    
    [HttpGet("undone/tomorrow")]
    public IEnumerable<TodoItem> GetUndoneForTomorrow(string user, [FromServices] ITodoRepository repository)
    {
        return repository.GetByPeriod(user, DateTime.Now.Date.AddDays(1), false);
    }
    
    [HttpPost("")]
    public GenericCommandResult Create(CreateTodoCommand command, [FromServices] TodoHandler handler)
    {
        command.User = "lucasrodrigues";
        return (GenericCommandResult) handler.Handle(command);
    }

    [HttpPut("")]
    public GenericCommandResult Update(UpdateTodoCommand command, [FromServices] TodoHandler handler)
    {
        command.User = "lucasrodrigues";
        return (GenericCommandResult)handler.Handle(command);
    }

    [HttpPut("mark-as-done")]
    public GenericCommandResult MarkAsDone(MarkTodoAsDoneCommand command, [FromServices] TodoHandler handler)
    {
        command.User = "lucasrodrigues";
        return (GenericCommandResult)handler.Handle(command);
    }
    
    [HttpPut("mark-as-undone")]
    public GenericCommandResult MarkAsDone(MarkTodoAsUndoneCommand command, [FromServices] TodoHandler handler)
    {
        command.User = "lucasrodrigues";
        return (GenericCommandResult)handler.Handle(command);
    }
}