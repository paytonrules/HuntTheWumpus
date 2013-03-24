using System;
using System.Collections.Generic;
namespace HuntTheWumpusDotNet.fixtures
{
    public class MessagesPrinted
    {
        public List<Object> Query()
        {
            var table = new List<Object>();

            foreach(var message in GameDriver.Display.Messages)
            {
                var row = new List<Object>();

                var cell = new List<Object> {"message", message};
                row.Add(cell);
                table.Add(row);
            }

            return table;
        }
    }
} 
