﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace commodities.Models
{
    public class Node
    {
        public string Name { get; set; }
        public IList<Node> ChildNodes { get; set; }
    }
}