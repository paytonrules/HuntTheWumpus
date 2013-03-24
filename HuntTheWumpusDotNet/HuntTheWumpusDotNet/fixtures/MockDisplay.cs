using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuntTheWumpusDotNet.fixtures
{
    public class MockDisplay : Display
    {
        protected List<String> messages;
        public List<String> Messages { get { return messages; } }

        public MockDisplay()
        {
            Restart();
        }

        public void WriteMessage(string message)
        {
            messages.Add(message);
        }

        public void Restart()
        {
            messages = new List<String>();
        }

        public String LastMessage()
        {
            if (messages.Count > 0)
                return messages.Last();
            return "";
        }
    }

}
