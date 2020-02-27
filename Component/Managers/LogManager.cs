using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Component.Interface;

namespace Component.Managers
{
    public class LogManager
    {
        private readonly ILogManager _logManager;

        public LogManager(ILogManager logManager)
        {
            _logManager = logManager;
        }

        public void Log(string message)
        {
            _logManager.Log(message);
        }
    }
}
