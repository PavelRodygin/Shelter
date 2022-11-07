using UnityEngine;

namespace Interfaces
{
    public interface IOpenClosable
    {
        bool IsOpen { get; }
        bool IsInteractable { get; }

        public Transform PointToLook { get; }
        void Close();
        void Open();
    }
}