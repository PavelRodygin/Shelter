using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Task //: ITask
{
    // private Action _feedback;
    // private MonoBehaviour _coroutineHost;
    // private Coroutine _coroutine;
    // private IEnumerator _taskAction;
    // private TaskPriorityEnum _taskPriority = TaskPriorityEnum.Default;
    // public TaskPriorityEnum Priority
    // {
    //     get
    //     {
    //         return _taskPriority;
    //     }
    // }
    //
    // public static Task Create(IEnumerator taskAction, TaskPriorityEnum priority = TaskPriorityEnum.Default)
    // {
    //     return new Task(taskAction, priority);
    // }
    //
    // public Task(IEnumerator taskAction, TaskPriorityEnum priority = TaskPriorityEnum.Default)
    // {
    //     //_coroutineHost = TaskManager.CoroutineHost;
    //     _taskPriority = priority;
    //     _taskAction = taskAction;
    // }
    //
    // public void Start()
    // {
    //     if (_coroutine == null)
    //     {
    //         _coroutine = _coroutineHost.StartCoroutine(RunTask());
    //     }
    // }
    //
    // public void Stop()
    // {
    //     if (_coroutine != null)
    //     {
    //         _coroutineHost.StopCoroutine(_coroutine);
    //         _coroutine = null;
    //     }
    // }
    //
    // public ITask Subscribe(Action feedback)
    // {
    //     _feedback += feedback;
    //
    //     return this;
    // }
    //
    // public void Complete()
    // {
    //     throw new NotImplementedException();
    // }
    //
    //
    // private IEnumerator RunTask()
    // {
    //     yield return _taskAction;
    //
    //     CallSubscribe();
    // }
    //
    // private void CallSubscribe()
    // {
    //     if (_feedback != null)
    //     {
    //         _feedback();
    //     }
    // }
}