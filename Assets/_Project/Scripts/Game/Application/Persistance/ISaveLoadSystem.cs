namespace Game.Application.Persistance
{
    public interface ISaveLoadSystem
    {
        bool Exists(string path);
        bool Save(object target, string path);
        T Load<T>(string path);
    }
}