using System;
using System.Collections.Generic;
using System.Linq;

namespace Test.Architecture
{
    public static class TypesFilter
    {
        public static IEnumerable<Type> GetFilterTypeList<T>()
        {
            var q = typeof(T).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)                                         
                .Where(x => !x.IsGenericTypeDefinition)                            
                .Where(x => typeof(T).IsAssignableFrom(x));

            return q;
        }
    }
}