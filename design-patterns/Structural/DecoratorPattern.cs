using design_patterns.Infrastructure;
using System;
using System.Text;

namespace design_patterns
{
    /*
    Extending a class is the first thing that comes to mind when you need to alter an object’s behavior. 
    However, inheritance has several serious caveats that you need to be aware of.
     
        1. Inheritance is static. You can’t alter the behavior of an existing object at runtime. 
        You can only replace the whole object with another one that’s created from a different subclass.
        
        2. Subclasses can have just one parent class. 
        In most languages, inheritance doesn’t let a class inherit behaviors of multiple classes at the same time.

    One of the ways to overcome these caveats is by using Composition instead of Inheritance. 
    With composition one object has a reference to another and delegates it some work, whereas with inheritance, 
    the object itself is able to do that work, inheriting the behavior from its superclass.
    */

    /// <summary>
    /// Decorator is a structural design pattern that lets you attach new behaviors to objects 
    /// by placing these objects inside special wrapper objects that contain the behaviors.
    /// </summary>
    class DecoratorPattern : DesignPattern
    {
        public override string PatternName => "Decorator";

        public override void ExecuteSample()
        {
            base.ExecuteSample();

            var fileDataSource = new FileDataSource();
            fileDataSource.WriteData("New data appended");
            Console.WriteLine($"File read: {fileDataSource.ReadData()}");

            var encryptedDataSource = new EncryptionDecorator(fileDataSource);
            encryptedDataSource.WriteData("New data appended");
            Console.WriteLine($"File read: {encryptedDataSource.ReadData()}");

            var compressedDataSource = new CompressionDecorator(fileDataSource);
            compressedDataSource.WriteData("New data appended");
            Console.WriteLine($"File read: {compressedDataSource.ReadData()}");
        }
    }

    interface IDataSource
    {
        void WriteData(string data);
        string ReadData();
    }

    class FileDataSource : IDataSource
    {
        private string _fileContent = "File content for testing purposes";
        public string ReadData()
        {
            return _fileContent;
        }

        public void WriteData(string data)
        {
            var builder = new StringBuilder(_fileContent);
            builder.Append(" -");
            builder.Append(data);
            _fileContent = builder.ToString();
        }
    }

    abstract class DataSourceDecorator : IDataSource
    {
        IDataSource _wrappee;
        public DataSourceDecorator(IDataSource dataSource)
        {
            _wrappee = dataSource;
        }

        public virtual string ReadData()
        {
            return _wrappee.ReadData();
        }

        public virtual void WriteData(string data)
        {
            _wrappee.WriteData(data);
        }
    }

    class EncryptionDecorator : DataSourceDecorator
    {
        public EncryptionDecorator(IDataSource dataSource) : base(dataSource)
        {
        }

        public override string ReadData()
        {
            var data = base.ReadData();
            if (!StringEncryptor.IsEncrypted(data))
                data = StringEncryptor.Encrypt(data);
            return StringEncryptor.Decrypt(data);
        }

        public override void WriteData(string data)
        {
            base.WriteData(StringEncryptor.Encrypt(data));
        }
    }

    class CompressionDecorator : DataSourceDecorator
    {
        public CompressionDecorator(IDataSource dataSource) : base(dataSource)
        {
        }
        public override string ReadData()
        {
            return base.ReadData();
        }

        public override void WriteData(string data)
        {
            var zip = StringCompressor.Zip(data);
            base.WriteData(zip.ToString());
        }
    }
}
