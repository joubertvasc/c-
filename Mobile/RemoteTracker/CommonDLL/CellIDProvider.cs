using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CommonDLL
{
    public enum CellIDProvider : int
    {
        OpenCellID = 0,
        Google = 1,
        CellDB = 2,
        Unknown = -1
    }
}
