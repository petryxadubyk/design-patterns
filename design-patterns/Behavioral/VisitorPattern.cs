using System;
using System.Collections.Generic;

namespace design_patterns
{
    /*
     The Visitor pattern suggests that you place the new behavior into a separate class called visitor, instead of trying to integrate it into existing classes. 
     The original object that had to perform the behavior is now passed to one of the visitor’s methods as an argument, 
     providing the method access to all necessary data contained within the object.
     */

    /// <summary>
    /// Visitor is a behavioral design pattern that lets you separate algorithms from the objects on which they operate.
    /// </summary>
    class VisitorPattern : DesignPattern
    {
        public override string PatternName => "Visitor";
        public override void ExecuteSample()
        {
            base.ExecuteSample();

            var shapes = new List<IShape>()
            {
                new Dot(),
                new Dot(),
                new Circle(),
                new Dot(),
                new Rectangle(),
                new Circle()
            };

            var xmlExportVisitor = new XMLExportVisitor();

            foreach (var shape in shapes)
            {
                shape.Accept(xmlExportVisitor);
            }
        }
    }

    interface IShape
    {
        void Accept(IVisitor visitor);
        void Draw();
    }

    class Dot : IShape
    {
        public void Accept(IVisitor visitor)
        {
            visitor.VisitDot(this);
        }

        public void Draw()
        {
            Console.WriteLine("Draw Dot");
        }
    }

    class Circle : IShape
    {
        public void Accept(IVisitor visitor)
        {
            visitor.VisitCircle(this);
        }

        public void Draw()
        {
            Console.WriteLine("Draw Circle");
        }
    }

    class Rectangle : IShape
    {
        public void Accept(IVisitor visitor)
        {
            visitor.VisitRectangle(this);
        }

        public void Draw()
        {
            Console.WriteLine("Draw Rectangle");
        }
    }

    interface IVisitor
    {
        void VisitDot(Dot dot);
        void VisitCircle(Circle circle);
        void VisitRectangle(Rectangle rectangle);
    }

    class XMLExportVisitor : IVisitor
    {
        public void VisitCircle(Circle circle)
        {
            Console.WriteLine("Export Circle to XML");
        }

        public void VisitDot(Dot dot)
        {
            Console.WriteLine("Export Dot to XML");
        }

        public void VisitRectangle(Rectangle rectangle)
        {
            Console.WriteLine("Export Rectangle to XML");
        }
    }
}
