using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructer
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}