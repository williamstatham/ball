using System;
using UniRx;

namespace Game.Scripts.Core.UniRX
{
    public abstract class ControllerBase : IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        
        void IDisposable.Dispose()
        {
            _compositeDisposable.Dispose();
        }

        public void AddDisposable(IDisposable disposable)
        {
            _compositeDisposable.Add(disposable);
        }
    }

    public static class DisposableExtensions
    {
        public static T AddTo<T>(this T disposable, ControllerBase controller)
            where T: IDisposable
        {
            controller.AddDisposable(disposable);
            return disposable;
        }
    }
}