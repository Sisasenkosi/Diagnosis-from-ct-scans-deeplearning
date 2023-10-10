using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace CadeCompetion.modelclass
{
    public class Data : IComparable<Data>
    {
        public string Label { get; set; }
        public string Percent { get; set; }
        public float Prediction { get; set; }

        public int CompareTo(Data other)
        {
            if (this.Prediction * 100 < other.Prediction * 100)
                return 1;
            else if (this.Prediction * 100 > other.Prediction * 100)
                return -1;
            else
                return 0;
        }
    }
}

