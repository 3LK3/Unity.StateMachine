using Elke.Utilities.Runtime;
using System;
using System.Collections;
using UnityEngine;

namespace Elke.StateMachine.Runtime
{
    /// <summary>
    /// A Generic state machine, adapted from the Runner template
    /// https://unity.com/features/build-a-runner-game
    /// </summary>
    public class BaseStateMachine
    {
        private bool m_PlayLock;

        private Coroutine m_CurrentPlayCoroutine;

        private Coroutine m_LoopCoroutine;

        #region Properties

        // The current state the statemachine is in
        public IState CurrentState { get; private set; }

        public bool IsRunning => m_LoopCoroutine != null;

        #endregion

        public virtual void Run(IState state)
        {
            SetCurrentState(state);
            CurrentState.EnableLinks();

            Run();
        }

        /// <summary>
        /// Finalizes the previous state and then runs the new state
        /// </summary>
        /// <param name="state"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void SetCurrentState(IState state)
        {
            if (CurrentState != null && m_CurrentPlayCoroutine != null)
            {
                //interrupt currently executing state
                Skip();
            }

            CurrentState = state ?? throw new ArgumentNullException(nameof(state));

            Coroutines.StartCoroutine(Play());
        }

        /// <summary>
        /// Turns on the main loop of the StateMachine.
        /// This method does not resume previous state if called after Stop()
        /// and the client needs to set the state manually.
        /// </summary>
        public virtual void Run()
        {
            if (m_LoopCoroutine != null)
            {
                return; //already running
            }

            m_LoopCoroutine = Coroutines.StartCoroutine(Loop());
        }

        /// <summary>
        /// The main update loop of the StateMachine.
        /// It checks the status of the current state and its link to provide state sequencing
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator Loop()
        {
            while (true)
            {
                if (CurrentState != null && m_CurrentPlayCoroutine == null) //current state is done playing
                {
                    if (CurrentState.ValidateLinks(out var nextState))
                    {
                        if (m_PlayLock)
                        {
                            //finalize current state
                            CurrentState.Exit();
                            m_PlayLock = false;
                        }

                        CurrentState.DisableLinks();
                        SetCurrentState(nextState);
                        CurrentState.EnableLinks();
                    }
                }

                yield return null;
            }
        }

        /// <summary>
        /// Runs the life cycle methods of the current state.
        /// </summary>
        private IEnumerator Play()
        {
            if (!m_PlayLock)
            {
                m_PlayLock = true;

                CurrentState.Enter();

                //keep a ref to execute coroutine of the current state
                //to support stopping it later.
                m_CurrentPlayCoroutine = Coroutines.StartCoroutine(CurrentState.Execute());

                yield return m_CurrentPlayCoroutine;

                m_CurrentPlayCoroutine = null;
            }
        }

        /// <summary>
        /// Interrupts the execution of the current state and finalizes it.
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void Skip()
        {
            if (CurrentState == null)
            {
                throw new Exception($"{nameof(CurrentState)} is null!");
            }

            if (m_CurrentPlayCoroutine != null)
            {
                Coroutines.StopCoroutine(ref m_CurrentPlayCoroutine);
                //finalize current state
                CurrentState.Exit();
                m_CurrentPlayCoroutine = null;
                m_PlayLock = false;
            }
        }
    }
}
