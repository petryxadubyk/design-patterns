using design_patterns.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace design_patterns.Services
{
    public class PatternService : IHostedService
    {
        private IConfiguration _configuration;
        public PatternService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine(_configuration["Logging:LogLevel:Default"]);

            IDesignPattern specificationPattern = new SpecificationPattern();
            specificationPattern.ExecuteSample();

            IDesignPattern factoryMethod = new FactoryMethodPattern();
            factoryMethod.ExecuteSample();

            IDesignPattern singletonPattern = new SingletonPattern();
            singletonPattern.ExecuteSample();

            IDesignPattern commandPattern = new CommandPattern();
            commandPattern.ExecuteSample();

            IDesignPattern chainOfResponsibilityPattern = new ChainOfResponsibilityPattern();
            chainOfResponsibilityPattern.ExecuteSample();

            IDesignPattern decoratorPattern = new DecoratorPattern();
            decoratorPattern.ExecuteSample();

            IDesignPattern strategyPattern = new StrategyPattern();
            strategyPattern.ExecuteSample();

            IDesignPattern abstractFactoryPattern = new AbstractFactoryPattern();
            abstractFactoryPattern.ExecuteSample();

            IDesignPattern compositePattern = new CompositePattern();
            compositePattern.ExecuteSample();

            IDesignPattern bridgePattern = new BridgePattern();
            bridgePattern.ExecuteSample();

            IDesignPattern observerPattern = new ObserverPattern();
            observerPattern.ExecuteSample();

            IDesignPattern statePattern = new StatePattern();
            statePattern.ExecuteSample();

            IDesignPattern mediatorPattern = new MediatorPattern();
            mediatorPattern.ExecuteSample();

            IDesignPattern visitorPattern = new VisitorPattern();
            visitorPattern.ExecuteSample();

            IDesignPattern momentoPattern = new MomentoPattern();
            momentoPattern.ExecuteSample();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
