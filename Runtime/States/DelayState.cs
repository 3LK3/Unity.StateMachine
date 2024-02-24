using System;
using System.Collections;
using UnityEngine;

namespace Elke.StateMachine.Runtime.States
{
    /// <summary>
    /// Delays the state-machine for the set amount. Pass in an Action<float> to run
    /// every frame while waiting (e.g. for a loading bar on the SplashScreen).
    /// </summary>
    public class DelayState : BaseState
    {
        private readonly float m_DelayInSeconds;

        // Optional Action to run every frame while waiting
        private readonly Action<float> m_ProgressUpdated;

        // Optional Action to run when execution completes
        private readonly Action m_OnExit;

        public DelayState(float delayInSeconds, Action<float> onUpdate = null, Action onExit = null, string stateName = nameof(DelayState))
        {
            m_DelayInSeconds = delayInSeconds;
            m_ProgressUpdated = onUpdate;
            m_OnExit = onExit;
            Name = stateName;
        }

        public override IEnumerator Execute()
        {
            var startTime = Time.time;

            base.LogCurrentState();

            while (Time.time - startTime < m_DelayInSeconds)
            {
                yield return null;
                float progressValue = (Time.time - startTime) / m_DelayInSeconds;
                m_ProgressUpdated?.Invoke(progressValue * 100);
            }
        }

        public override void Exit()
        {
            m_OnExit?.Invoke();
        }
    }
}
