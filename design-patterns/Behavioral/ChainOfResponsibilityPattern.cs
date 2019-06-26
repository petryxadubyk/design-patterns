using System;
using System.Collections.Generic;
using System.Linq;

namespace design_patterns
{
    /// <summary>
    /// Chain of Responsibility is a behavioral design pattern that lets you pass requests along a chain of handlers. 
    /// Upon receiving a request, each handler decides either to process the request or to pass it to the next handler in the chain.
    /// </summary>
    class ChainOfResponsibilityPattern : DesignPattern
    {
        public override string PatternName => "Chain of Responsibility";

        public override void ExecuteSample()
        {
            base.ExecuteSample();

            var authHandler = new AuthenticationHandler();
            var rolsHandler = new AuthorizationHandler();
            var cacheHandler = new CacheHandler();

            authHandler.SetNext(rolsHandler).SetNext(cacheHandler);

            var orderRequest = new OrderRequest() {
                AuthToken = Guid.NewGuid().ToString(),
                UserRoles = new List<string> { "Admin" }
            };
            var valid = authHandler.Handle(orderRequest);

            Console.WriteLine($"Chain of Responsibility result: {valid}");
        }
    }

    interface IHandler
    {
        IHandler SetNext(IHandler handler);
        bool Handle(OrderRequest request);
    }

    abstract class Handler : IHandler
    {
        private IHandler _next;

        public virtual bool Handle(OrderRequest request)
        {
            Console.WriteLine($"Base handler: {DateTime.Now.ToLongTimeString()}");
            if (_next != null)
                return _next.Handle(request);
            return true;
        }

        public IHandler SetNext(IHandler handler)
        {
            _next = handler;
            return _next;
        }
    }

    class AuthenticationHandler : Handler
    {
        public override bool Handle(OrderRequest request)
        {
            Console.WriteLine($"Auth handler: {DateTime.Now.ToLongTimeString()}");
            var valid = !string.IsNullOrEmpty(request.AuthToken);
            if (valid)
                return base.Handle(request);
            return false;
        }
    }

    class AuthorizationHandler: Handler
    {
        public override bool Handle(OrderRequest request)
        {
            Console.WriteLine($"Roles handler: {DateTime.Now.ToLongTimeString()}");
            var valid = request.UserRoles != null && request.UserRoles.Any();
            if (valid)
                return base.Handle(request);
            return false;
        }
    }

    class CacheHandler: Handler
    {
        private Dictionary<string, OrderRequest> _cache = new Dictionary<string, OrderRequest>();
        public override bool Handle(OrderRequest request)
        {
            Console.WriteLine($"Cache handler: {DateTime.Now.ToLongTimeString()}");
            if (!_cache.ContainsKey(request.AuthToken))
                _cache[request.AuthToken] = request;
            return base.Handle(request);
        }
    }

    class OrderRequest
    {
        public string AuthToken { get; set; }
        public List<string> UserRoles { get; set; }
    }
}

