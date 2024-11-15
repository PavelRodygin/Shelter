using System;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.MVVM
{
    public interface IScreenViewModel : IDisposable
    {
        bool CanExit { get; }
        
        UniTask Enter(object param);

        UniTask Exit();

        void NativeExit();
    }
}