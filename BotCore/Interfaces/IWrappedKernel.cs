namespace BotCore.Interfaces
{
    public interface IWrappedKernel
    {
        T GetInstance<T>() where T: IBase;
        IWrappedKernel Bind<Tinterface, Timplementation>() where Timplementation : Tinterface, new();
    }
}
