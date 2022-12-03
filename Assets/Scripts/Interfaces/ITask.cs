using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Interfaces
{
    public enum TaskPriorityEnum
    {
        Default,
        High,
        InterruptHigh
    }
    public interface ITask
    {
        TaskPriorityEnum Priority { get; }
        
        void Start();
        ITask Subscribe(Action completeCallBack);
        void Complete();
    }
}