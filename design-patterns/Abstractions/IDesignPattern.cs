namespace design_patterns.Abstractions
{
    public interface IDesignPattern
    {
        string PatternName { get; }
        void ExecuteSample();
    }
}
