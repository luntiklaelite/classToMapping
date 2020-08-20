using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classToMapping
{
    enum enum4ik
    {
        defaults,
        nope
    }

    class Example
    {
        public float HeightToNaturalSoil { get; set; }
        public float LayingDepth { get; set; }
        public Example TypicalProject { get; set; }
        public enum4ik enum1 { get; set; }

        public float MassivePartSizeAlong { get; set; }
        public float MassivePartSizeAcross { get; set; }
        public int PileCount { get; set; }
        public float MaxDistanceBetweenAxis { get; set; }
        public string Scheme { get; set; }
        public float CrossbarWidth { get; set; }
        public float CrossbarHeight { get; set; }
        public float CrossbarLength { get; set; }
        public float PileCrossSectionWidth { get; set; }
        public float PileCrossSectionHeight { get; set; }
        public string Notes { get; set; }

    }
}
