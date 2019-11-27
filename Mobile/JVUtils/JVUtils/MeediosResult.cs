using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace JVUtils
{
    public enum MeediosResult
    {
        Ok,
        UndefinedError,
        OtherException,
        WrongUser, // 2
        WrongPass, // 3, 5
        InvalidUser, // 4
        FailedGeneratedAccound, // 6
        FailedStoreIMEI, // 7
        InvalidAPIKey // 8
    }
}
