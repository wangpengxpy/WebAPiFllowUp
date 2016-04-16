using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class logger
    {
        private static ILog Info;
        private static ILog Error;
        private static ILog Warn;
        private static ILog Debug;

        private static object objectLock = new object();
        private static logger _instance;
        private logger()
        {
            Error = LogManager.GetLogger("logerror");
            Info = LogManager.GetLogger("loginfo");
            Warn = LogManager.GetLogger("logwarn");
            Debug = LogManager.GetLogger("logdebug");
        }

        private static logger Instance()
        {
            if (_instance == null)
            {
                lock (objectLock)
                {
                    if (_instance == null)
                    {
                        _instance = new logger();
                    }
                }
            }
            return _instance;
        }

        public static void LogInfo(string info)
        {
            logger.Instance();
            //需要添加操作人id
            var method = Method(2);
            var infoLog = string.Format("{0}{1}", method, info);
            Info.Info(infoLog);
        }

        public static void LogInfo(string info,Exception ex)
        {
            logger.Instance();
            //需要添加操作人id
            var method = Method(2);
            var infoLog = string.Format("{0}{1}{2}", method,info, ex);
            Info.Info(infoLog);
        }

        public static void LogWarn(string warn)
        {
            logger.Instance();
            //需要添加操作人id
            var method = Method(2);
            var warnLog = string.Format("{0}{1}", method, warn);
            Warn.Warn(warnLog);
        }
        public static void LogWarn(string warn,Exception ex)
        {
            logger.Instance();
            //需要添加操作人id
            var method = Method(2);
            var warnLog = string.Format("{0}{1}{2}", method,warn, ex);
            Warn.Warn(warnLog);
        }
        public static void LogError(string error)
        {
            logger.Instance();
            //需要添加操作人id
            var method = Method(2);
            var errorLog = string.Format("{0}{1}", method, error);
            Error.Error(errorLog);
        }

        public static void LogError(string error,Exception ex)
        {
            logger.Instance();
            //需求添加操作人id
            var method = Method(2);
            var errorLog = string.Format("{0}{1}{2}", method, error,ex);
            Error.Error(errorLog);
        }

        private static string Method(int i)
        {
            var trace = new StackTrace();
            var methodInfo = trace.GetFrame(i).GetMethod();
            var methodName = methodInfo.Name;
            var className = methodInfo.DeclaringType.FullName;
            return string.Format("{0}.{1}", className, methodName).Trim('.');
        }

    }
}
