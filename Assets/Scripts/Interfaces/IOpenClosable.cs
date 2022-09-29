using UnityEngine;

namespace Interfaces
{
    public interface IOpenClosable
    {
        bool IsOpen { get; }
        public Transform PointToLook { get; }
        void Close();
        void Open();
    }
}