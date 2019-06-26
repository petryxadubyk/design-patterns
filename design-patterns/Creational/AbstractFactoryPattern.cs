using System;

namespace design_patterns
{
    /// <summary>
    /// Abstract Factory is a creational design pattern that lets you produce families of related objects 
    /// without specifying their concrete classes.
    /// </summary>
    class AbstractFactoryPattern : DesignPattern
    {
        public override string PatternName => "Abstract Factory";

        public override void ExecuteSample()
        {
            base.ExecuteSample();

            var ukraineFactory = new UkraineFurnitureFactory();
            var sofaA = ukraineFactory.CreateSofa();
            var chairA = ukraineFactory.CreateModernChair();

            var polandFactory = new PolandFurnitureFactory();
            var sofaB = polandFactory.CreateSofa();
            var chairB = polandFactory.CreateArtDecoChair(); 
        }
    }

    enum eMaterial
    {
        VariantA = 1,
        VariantB = 2,
        VariantC = 3
    }


    interface IFurnitureFactory
    {
        IChair CreateModernChair();
        IChair CreateArtDecoChair();
        ISofa CreateSofa();
    }

    class UkraineFurnitureFactory : IFurnitureFactory
    {
        public IChair CreateArtDecoChair()
        {
            var product = new ArtDecoChair(400, 400, 150, eMaterial.VariantA);
            product.ManufactureChair();
            return product;
        }

        public IChair CreateModernChair()
        {
            var product = new ModernChair(300, 500, 50, eMaterial.VariantB);
            product.ManufactureChair();
            return product;
        }

        public ISofa CreateSofa()
        {
            var product = new ModernSofa(500, 2000, eMaterial.VariantC);
            product.ManufactureSofa();
            return product;
        }
    }

    class PolandFurnitureFactory : IFurnitureFactory
    {
        public IChair CreateArtDecoChair()
        {
            var product = new ArtDecoChair(500, 500, 170, eMaterial.VariantB);
            product.ManufactureChair();
            return product;
        }

        public IChair CreateModernChair()
        {
            var product = new ModernChair(400, 400, 70, eMaterial.VariantC);
            product.ManufactureChair();
            return product;
        }

        public ISofa CreateSofa()
        {
            var product = new ArtDecoSofa(600, 2500, eMaterial.VariantA);
            product.ManufactureSofa();
            return product;
        }
    }

    interface IChair
    {
        void ManufactureChair();
        eMaterial Material { get; }
    }

    class ModernChair : IChair
    {
        private int _width;
        private int _height;
        private int _high;
        private eMaterial _material;

        public ModernChair(int width, int height, int high, eMaterial material)
        {
            _width = width;
            _height = height;
            _high = high;
            _material = material;
        }
        public eMaterial Material => _material;

        public void ManufactureChair()
        {
            Console.WriteLine($"Manufactured Modern Chair: {_width}x{_height}x{_high}. Material: {_material}");
        }
    }

    class ArtDecoChair : IChair
    {
        private int _width;
        private int _height;
        private int _high;
        private eMaterial _material;

        public ArtDecoChair(int width, int height, int high, eMaterial material)
        {
            _width = width;
            _height = height;
            _high = high;
            _material = material;
        }
        public eMaterial Material => _material;

        public void ManufactureChair()
        {
            Console.WriteLine($"Manufactured ArtDeco Chair: {_width}x{_height}x{_high}. Material: {_material}");
        }
    }


    interface ISofa
    {
        void ManufactureSofa();
        eMaterial Material { get; }
    }

    class ModernSofa : ISofa
    {
        private int _width;
        private int _height;
        private eMaterial _material;

        public ModernSofa(int width, int height, eMaterial material)
        {
            _width = width;
            _height = height;
            _material = material;
        }
        public eMaterial Material => _material;

        public void ManufactureSofa()
        {
            Console.WriteLine($"Manufactured Modern Sofa: {_width}x{_height}. Material: {_material}");
        }
    }

    class ArtDecoSofa : ISofa
    {
        private int _width;
        private int _height;
        private eMaterial _material;

        public ArtDecoSofa(int width, int height, eMaterial material)
        {
            _width = width;
            _height = height;
            _material = material;
        }
        public eMaterial Material => _material;

        public void ManufactureSofa()
        {
            Console.WriteLine($"Manufactured ArtDeco Sofa: {_width}x{_height}. Material: {_material}");
        }
    }
}
