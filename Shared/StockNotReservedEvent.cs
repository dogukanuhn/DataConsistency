﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{

    public class StockNotReservedEvent
    {
        public string OrderId { get; set; }

        public string Message { get; set; }
    }
}
