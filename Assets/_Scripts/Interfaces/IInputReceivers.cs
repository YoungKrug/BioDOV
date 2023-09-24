using UnityEngine;

namespace _Scripts.Interface
{
    public interface IInputReceivers
    {
        KeyCode Key { get; }
        void ExecuteKey();
    }
}