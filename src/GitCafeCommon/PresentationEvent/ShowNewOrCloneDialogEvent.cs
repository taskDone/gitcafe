using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitCafeCommon.PresentationEvent
{
    public class ToolBarClickEvent : CompositePresentationEvent<ToolBarClickType>
    {

    }

    public enum ToolBarClickType
    {
        NewOrClone,
        Commit,
        Add,
        Push
    }
}
