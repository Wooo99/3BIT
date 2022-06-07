namespace ShareRide.App.Factories
{
    public interface IFactory<out T>
    {
        T Create();
    }
}