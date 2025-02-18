﻿
using Connectome.Core.Interface;
using Connectome.Core.Template;
using Connectome.Emotiv.Event;
using Connectome.Emotiv.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connectome.Core.Implementation
{
    /// <summary>
    /// The processor that handles anything related to the timeline. All children processors/processees set as children
    /// to this processor must be of the Timeline type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TimelineProcessor<T> : Processor<T, ITimeline<IEmotivState>>, IEmotivReaderTracker
    {
        #region public Attributes
        /// <summary>
        /// The children processor/processees
        /// </summary>
        public override IProcessable<ITimeline<IEmotivState>>[] Children { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="timeline"></param>
        /// <param name="children"></param>
        public TimelineProcessor(ITimeline<IEmotivState> timeline, 
            params IProcessable<ITimeline<IEmotivState>>[] children) : base(timeline)
        {
            Children = children;
        }

        #endregion
        #region IEmotivReaderTracker
        public void Track(EmotivReadArgs args)
        {
            Argument.Register(args.State); 
        }
        #endregion
    }
}
