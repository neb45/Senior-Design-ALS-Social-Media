﻿using Connectome.Core.Interface;
using Connectome.Emotiv.Enum;

namespace Connectome.Emotiv.Interface
{
    //TODO require IEquality for comparing. 
    /// <summary>
    /// State to be read from device. 
    /// </summary>
    public interface IEmotivState : ITime
    {
        /// <summary>
        /// Command.
        /// </summary>
        EmotivCommandType Command { get; }
        /// <summary>
        /// Power 0.0 to 1.0.
        /// </summary>
        float Power { get; }
    }
}
