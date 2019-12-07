using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCraftChatExtraction
{
    class Log
    {
        public TimeSpan date;// 時分秒の順で記録(14:29:02など)
        public string state;//client・server/info・warnなど利用先未定
        public string message;//ログの本体?

        public Log(string log)
        {
            const int messageStartIndex = 32;
            const int dateStartIndex = 1;
            const int dateLength = 8;
            const int stateStartIndex = 12;
            const int stateLength = 18;
            //:
            this.message = log.Substring(messageStartIndex) + "\r\n";

            this.date = String2TimeSpan(log.Substring(dateStartIndex,dateLength));

            this.state = log.Substring(stateStartIndex, stateLength);
        }

        public TimeSpan String2TimeSpan(string str)
        {
            string[] arr = str.Split(':');

            return new TimeSpan(int.Parse(arr[0]), int.Parse(arr[1]), int.Parse(arr[2]));
        }
    }
}
