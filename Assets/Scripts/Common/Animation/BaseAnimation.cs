using System;
using UniRx;
using UnityEngine;

namespace Common.Animation
{
    public abstract class BaseAnimation : MonoBehaviour
    {
        public delegate void PostAnimationAction();

        public abstract void OnStart();
        public abstract IObservable<Unit> Show(PostAnimationAction action = null);
        public abstract IObservable<Unit> Hide(PostAnimationAction action = null);

        public abstract IObservable<Unit> Play(string name, PostAnimationAction action = null);

        public void Start()
        {
            OnStart();
        }
    }
}
