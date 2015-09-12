using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.Radio.Entity
{
    public class Radios : BaseEntity
    {
        public String EnName { get; set; }

        public String CnName { get; set; }

        public String LinkAddress { get; set; }

        public Int32 RegionID { get; set; }

        public String Description { get; set; }

        public String Remarks { get; set; }

    }
}
