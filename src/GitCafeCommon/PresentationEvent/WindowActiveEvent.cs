using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitCafeCommon.PresentationEvent
{
    public class WindowActiveEvent : CompositePresentationEvent<WindowActiveEx>
    {
    }
    public class WindowActiveEx
    { }
}
