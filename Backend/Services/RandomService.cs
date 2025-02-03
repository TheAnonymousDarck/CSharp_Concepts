namespace Backend.Services;

public class RandomService: IRandomService
{
    public int value { get; }

    public RandomService()
    {
        value = new Random().Next(1000);
    }
}