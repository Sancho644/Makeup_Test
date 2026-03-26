using System;
using System.Collections.Generic;

namespace Core.Makeup.Strategies
{
    public class ActionSequence
    {
        private readonly Queue<Action<Action>> _steps = new();

        public ActionSequence Step(Action<Action> step)
        {
            _steps.Enqueue(step);
            return this;
        }

        public void Start(Action onComplete = null)
        {
            void Next()
            {
                if (_steps.Count == 0)
                {
                    onComplete?.Invoke();
                    return;
                }

                var step = _steps.Dequeue();
                step(Next);
            }

            Next();
        }
    }
}
