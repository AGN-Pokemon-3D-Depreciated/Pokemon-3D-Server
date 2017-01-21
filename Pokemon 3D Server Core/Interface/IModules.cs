namespace Pokemon_3D_Server_Core.Interface
{
    public interface IModules
    {
        string Name { get; }
        string Version { get; }

        void Start();

        void Stop();
    }
}