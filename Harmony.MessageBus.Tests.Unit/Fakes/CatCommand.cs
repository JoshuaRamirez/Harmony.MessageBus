using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tally.Bus.Tests.Unit.Fakes
{
    public class CatCommand : MammalCommand
    {
        public int MeowVolume { get; set; }
    }
}
