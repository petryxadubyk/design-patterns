using design_patterns.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace design_patterns
{
    /// <summary>
    /// Specification pattern is a pattern that allows us to encapsulate some piece of domain knowledge into a single unit – 
    /// specification – and reuse it in different parts of the code base.
    /// </summary>
    class SpecificationPattern : DesignPattern
    {
        private PersonRepository _personRepository;
        public SpecificationPattern()
        {
            _personRepository = new PersonRepository();
        }

        public override string PatternName => "Specification";

        public override void ExecuteSample()
        {
            base.ExecuteSample();

            var adultSpecification = new AdultsSpecification();
            var dantistSpecification = new ProfessionSpecification(eProffesion.Dantist);
            var doctorSpecification = new ProfessionSpecification(eProffesion.Doctor);

            var adults = _personRepository.Find(adultSpecification);
            bool hasDantist = adults.Any(r => dantistSpecification.IsSatisfiedBy(r));
            bool hasDoctorOrDantist = adults.All(person => dantistSpecification.And(doctorSpecification).IsSatisfiedBy(person));
        }
    }

    enum eProffesion
    {
        Doctor = 1,
        Dantist = 2,
        Professor = 3
    }

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public eProffesion Proffesion { get; set; }
    }

    class PersonRepository
    {
        private readonly IQueryable<Person> _collection;
        public PersonRepository()
        {
            _collection = new List<Person>().AsQueryable();
        }

        public IReadOnlyList<Person> Find(ISpecification<Person> specification)
        {
            return _collection.Where(specification.ToExpression()).ToList();
        }
    }

    //-----------------------------------------------------------
    interface ISpecification<T> where T : class
    {
        bool IsSatisfiedBy(T Entity);
        Expression<Func<T, bool>> ToExpression();
        ISpecification<T> And(ISpecification<T> specification);
    }

    abstract class CompositeSpecification<T> : ISpecification<T> where T : class
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T entity)
        {
            var predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public ISpecification<T> And(ISpecification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }
    }

    class AdultsSpecification : CompositeSpecification<Person>
    {
        public override Expression<Func<Person, bool>> ToExpression()
        {
            return person => person.Age > 18;
        }
    }

    class ProfessionSpecification : CompositeSpecification<Person>
    {
        private eProffesion _profession;
        public ProfessionSpecification(eProffesion profession)
        {
            _profession = profession;
        }
        public override Expression<Func<Person, bool>> ToExpression()
        {
            return person => person.Proffesion == _profession;
        }
    }

    class AndSpecification<T> : CompositeSpecification<T> where T : class
    {
        private ISpecification<T> _left;
        private ISpecification<T> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var left = _left.ToExpression();
            var right = _right.ToExpression();

            BinaryExpression andExpession = Expression.AndAlso(left.Body, right.Body);

            return Expression.Lambda<Func<T, bool>>(andExpession, left.Parameters.Single());
        }
    }
}
