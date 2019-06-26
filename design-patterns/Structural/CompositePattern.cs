using System;
using System.Collections.Generic;

namespace design_patterns
{
    /*
     * Using the Composite pattern makes sense only when the core model of your app can be represented as a tree.
     */

    /// <summary>
    /// Composite is a structural design pattern that lets you compose objects into tree structures 
    /// and then work with these structures as if they were individual objects.
    /// </summary>
    class CompositePattern : DesignPattern
    {
        public override string PatternName => "Composite";
        public override void ExecuteSample()
        {
            base.ExecuteSample();
            //444
            var rootBox = new Box();
            rootBox.AddProduct(new Product(100));
            rootBox.AddProduct(new Product(100));
            rootBox.AddProduct(new TaxProduct(100));

            var boxA = new Box();
            rootBox.AddProduct(boxA);

            boxA.AddProduct(new Product(10));
            boxA.AddProduct(new TaxProduct(10));

            var boxB = new Box();
            boxA.AddProduct(boxB);

            boxB.AddProduct(new Product(10));
            boxB.AddProduct(new TaxProduct(10));

            Console.WriteLine($"Total box price: {rootBox.GetPrice()}$");
            Console.WriteLine($"Inner box price: {boxA.GetPrice()}$");
        }
    }

    interface IComponent
    {
        decimal GetPrice();
    }

    /// <summary>
    /// Leaf
    /// </summary>
    class Product : IComponent
    {
        private decimal _price;
        private decimal _packagePrice;
        public Product(decimal price)
        {
            _price = price;
            _packagePrice = 20;
        }
        public decimal GetPrice()
        {
            return _price + _packagePrice;
        }
    }

    /// <summary>
    /// Leaf
    /// </summary>
    class TaxProduct : IComponent
    {
        private decimal _price;
        private decimal _packagePrice;
        private decimal _taxPercent;
        public TaxProduct(decimal price)
        {
            _price = price;
            _packagePrice = 20;
            _taxPercent = 20;
        }
        public decimal GetPrice()
        {
            var price = _price + _packagePrice;
            return price + (_taxPercent / 100) * price;
        }
    }

    class Box : IComponent
    {
        private List<IComponent> _productCollection;

        public Box()
        {
            _productCollection = new List<IComponent>();
        }
        public void AddProduct(IComponent product)
        {
            _productCollection.Add(product);
        }

        public void RemoveProduct(IComponent product)
        {
            _productCollection.Remove(product);
        }

        public List<IComponent> ProductCollection => _productCollection;

        public decimal GetPrice()
        {
            decimal price = 0;
            foreach (var product in _productCollection)
                price += product.GetPrice();

            return price;
        }
    }
}
