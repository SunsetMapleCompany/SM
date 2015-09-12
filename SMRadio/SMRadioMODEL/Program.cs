using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.Radio.Entity
{
    public class Program
    {
        public String ID { get; set; }

        public String RadioID { get; set; }

        public String EnName { get; set; }

        public String CnName { get; set; }

        public String PlayWeakDay { get; set; }

        public DateTime PlayTime { get; set; }

        public bool IsReplay { get; set; }

        public String Anchors { get; set; }

        public String AnchorIDS { get; set; }

        public String Remarks { get; set; }

    }
}
