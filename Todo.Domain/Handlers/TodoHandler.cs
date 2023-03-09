using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers;

public class TodoHandler :
    Notifiable,
    IHandler<CreateTodoCommand>,
    IHandler<UpdateTodoCommand>,
    IHandler<MarkTodoAsDoneCommand>,
    IHandler<MarkTodoAsUndoneCommand>
{
    private readonly ITodoRepository _repository;

    public TodoHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public ICommandResult Handle(CreateTodoCommand command)
    {
        // Fail Fast Validation
        command.Validate();
        if (command.Invalid)
            return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);

        // Gerar um item
        var todo = new TodoItem(command.Title, command.User, command.Date);
        
        // Salvar
        _repository.Create(todo);
        
        // Retorna o resultado
        return new GenericCommandResult(true, "Tarefa salva!", todo);
    }

    public ICommandResult Handle(UpdateTodoCommand command)
    {
        // Fail fast validation
        command.Validate();
        if (command.Invalid)
            return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);
        
        // Recupera o TodoItem (Rehidratação)
        var todo = _repository.GetById(command.Id, command.User);

        // Altera o título
        todo.UpdateTitle(command.Title);
        
        _repository.Update(todo);

        return new GenericCommandResult(true, "Tarefa salva", todo);
    }

    public ICommandResult Handle(MarkTodoAsDoneCommand command)
    {
        command.Validate();
        if (command.Invalid)
            return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);

        var todo = _repository.GetById(command.Id, command.User);
        
        todo.MarkAsDone();
        
        _repository.Update(todo);
        
        return new GenericCommandResult(true, "Tarefa salva", todo);
    }

    public ICommandResult Handle(MarkTodoAsUndoneCommand command)
    {
        command.Validate();
        if (command.Invalid)
            return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);

        var todo = _repository.GetById(command.Id, command.User);
        
        todo.MarkAsUndone();
        
        _repository.Update(todo);
        
        return new GenericCommandResult(true, "Tarefa salva", todo);
    }
}