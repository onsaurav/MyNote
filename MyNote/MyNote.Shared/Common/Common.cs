using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.Common
{
    public static class Common
    {
        public const string DB_NAME = "MyNote_APP_1.db";
                
        public enum ConformState
        {
            Yes = 1,
            No,
            OK,
            Cancel
        };
    }
}
