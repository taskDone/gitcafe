using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitCafeCommon.PresentationEvent
{
    public class LoadRepositoryDBEvent : CompositePresentationEvent<List<GitCafeCommon.Models.GitCafeRepository>>
    {
    }

    public class AddRepositoryDBEvent : CompositePresentationEvent<GitCafeCommon.Models.GitCafeRepository>
    {
    }
}
