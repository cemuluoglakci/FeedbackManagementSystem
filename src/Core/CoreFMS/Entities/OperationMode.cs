using System;
using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class OperationMode
    {
        public int Id { get; set; }
        public string ModeName { get; set; } = null!;
    }
}
