
using _Scripts.ScriptableObjects;

namespace _Scripts.Interface
{
    public interface IEventReactor
    {
        public BaseEventScriptableObject ScriptableObject { get; }
        public void Execute(object obj);
    }
}