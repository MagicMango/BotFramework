namespace BotCore.Interfaces
{
    public interface IWrappedKernel
    {
        T GetInstance<T>();
        IWrappedKernel Bind<Tinterface, Timplementation>() where Timplementation : Tinterface;
    }
}
