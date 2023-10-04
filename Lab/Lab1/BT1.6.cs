public class Mamal
{
    public String characteristics;
}
public class Whale : Mamal
{
    public Whale() { }
}
public interface IThinking
{
    public void thinking_behavior();
}
public interface IIntelligent
{
    public void intelligent_behavior();
}
public interface IAbility : IThinking, IIntelligent{}

public class Human:Mamal, IAbility
{
    public Human() { }
    public void thinking_behavior() { }
    public void intelligent_behavior() { }
}