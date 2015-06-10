using System.Collections.Generic;
using System;

namespace SharpExpressions
{
    class Registry
    {
        public readonly Dictionary<string, object> identifiers = new Dictionary<string, object>();
        public readonly Dictionary<string, Type> types = new Dictionary<string, Type>();
    }
}
