using System;
using System.Collections.Generic;
namespace HuntTheWumpusDotNet.fixtures
{
    public class MessagesPrinted
    {
        public List<Object> Query()
        {
            var list = new List<Object>
                           {
                               new List<Object>
                                   {
                                       new List<String> {"message", "string"}
                                   }
                           };

            return list;
        }
    }
} 
