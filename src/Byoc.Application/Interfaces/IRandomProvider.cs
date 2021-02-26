namespace Byoc.Application.Interfaces
{
    public interface IRandomProvider
    {
        int NextInt(int maxValue);

        int NextInt(int minValue, int maxValue);
    }
}
