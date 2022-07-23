using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Framework
{
    public abstract record Value<T> where T : Value<T>
    {

    }
}
