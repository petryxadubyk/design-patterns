using System;

namespace design_patterns
{
    /// <summary>
    /// Singleton is a creational design pattern that lets you ensure that a class has only one instance, 
    /// while providing a global access point to this instance.
    /// </summary>
    class SingletonPattern : DesignPattern
    {
        public override string PatternName => "Singleton";

        public override void ExecuteSample()
        {
            base.ExecuteSample();
            var objectA = GodObject.GetInstance();
            var objectB = GodObject.GetInstance();

            Console.WriteLine($"Objects are equal: {objectA.Equals(objectB)}");
            Console.WriteLine($"Objects are equal: {Object.ReferenceEquals(objectA, objectB)}");
        }
    }

    class GodObject
    {
        private GodObject() { }
        private static GodObject _instance;

        public static GodObject GetInstance()
        {
            if (_instance == null)
                _instance = new GodObject();
            return _instance;
        }
    }
}
