// Copyright (c) 2015 - 2021 Doozy Entertainment. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

//.........................
//.....Generated Class.....
//.........................
//.......Do not edit.......
//.........................

using UnityEngine;
// ReSharper disable All

namespace Doozy.Runtime.Signals
{
    public partial class Signal
    {
        public static bool Send(StreamId.Track id, string message = "") => SignalsService.SendSignal(nameof(StreamId.Track), id.ToString(), message);
        public static bool Send(StreamId.Track id, GameObject signalSource, string message = "") => SignalsService.SendSignal(nameof(StreamId.Track), id.ToString(), signalSource, message);
        public static bool Send(StreamId.Track id, SignalProvider signalProvider, string message = "") => SignalsService.SendSignal(nameof(StreamId.Track), id.ToString(), signalProvider, message);
        public static bool Send(StreamId.Track id, Object signalSender, string message = "") => SignalsService.SendSignal(nameof(StreamId.Track), id.ToString(), signalSender, message);
        public static bool Send<T>(StreamId.Track id, T signalValue, string message = "") => SignalsService.SendSignal(nameof(StreamId.Track), id.ToString(), signalValue, message);
        public static bool Send<T>(StreamId.Track id, T signalValue, GameObject signalSource, string message = "") => SignalsService.SendSignal(nameof(StreamId.Track), id.ToString(), signalValue, signalSource, message);
        public static bool Send<T>(StreamId.Track id, T signalValue, SignalProvider signalProvider, string message = "") => SignalsService.SendSignal(nameof(StreamId.Track), id.ToString(), signalValue, signalProvider, message);
        public static bool Send<T>(StreamId.Track id, T signalValue, Object signalSender, string message = "") => SignalsService.SendSignal(nameof(StreamId.Track), id.ToString(), signalValue, signalSender, message);

        public static bool Send(StreamId.View id, string message = "") => SignalsService.SendSignal(nameof(StreamId.View), id.ToString(), message);
        public static bool Send(StreamId.View id, GameObject signalSource, string message = "") => SignalsService.SendSignal(nameof(StreamId.View), id.ToString(), signalSource, message);
        public static bool Send(StreamId.View id, SignalProvider signalProvider, string message = "") => SignalsService.SendSignal(nameof(StreamId.View), id.ToString(), signalProvider, message);
        public static bool Send(StreamId.View id, Object signalSender, string message = "") => SignalsService.SendSignal(nameof(StreamId.View), id.ToString(), signalSender, message);
        public static bool Send<T>(StreamId.View id, T signalValue, string message = "") => SignalsService.SendSignal(nameof(StreamId.View), id.ToString(), signalValue, message);
        public static bool Send<T>(StreamId.View id, T signalValue, GameObject signalSource, string message = "") => SignalsService.SendSignal(nameof(StreamId.View), id.ToString(), signalValue, signalSource, message);
        public static bool Send<T>(StreamId.View id, T signalValue, SignalProvider signalProvider, string message = "") => SignalsService.SendSignal(nameof(StreamId.View), id.ToString(), signalValue, signalProvider, message);
        public static bool Send<T>(StreamId.View id, T signalValue, Object signalSender, string message = "") => SignalsService.SendSignal(nameof(StreamId.View), id.ToString(), signalValue, signalSender, message);   
    }

    public partial class StreamId
    {
        public enum Track
        {
            HUDEnabled
        }
        public enum View
        {
            InputEnabled,
            StartGameEnabled
        }         
    }
}
