using UnityEngine;

namespace _Scripts.Interface
{
    public interface IInputReceivers
    {
        KeyCode[] Keys { get; }
        void ExecuteKey(KeyCode code);
    }
}