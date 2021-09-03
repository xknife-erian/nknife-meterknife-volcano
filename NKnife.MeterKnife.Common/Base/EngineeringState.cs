﻿using System;
using NKnife.Events;

// ReSharper disable once CheckNamespace
namespace NKnife.MeterKnife.Base
{
    public class EngineeringState
    {
        private State _engState  = State.Start;

        public EngineeringState(string engineeringId)
        {
            EngineeringId = engineeringId;
        }

        public string EngineeringId { get; set; }

        public State EngState
        {
            get => _engState;
            set
            {
                if (!Equals(value, _engState))
                {
                    _engState = value;
                    OnEngineeringStateChanged(new EventArgs<State>(value));
                }
            }
        }

        public event EventHandler<EventArgs<State>> EngineeringStateChanged;

        protected virtual void OnEngineeringStateChanged(EventArgs<State> e)
        {
            EngineeringStateChanged?.Invoke(this, e);
        }

        /// <summary>
        ///     工程的采集状态
        /// </summary>
        public enum State
        {
            Start,
            Pause,
            Resume,
            Stop
        }
    }
}