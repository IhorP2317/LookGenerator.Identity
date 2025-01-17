using MediatR;

namespace Application.Abstractions ;

    public interface ICommand : IRequest;
    public interface ICommand<out TResponse> : IRequest<TResponse>;