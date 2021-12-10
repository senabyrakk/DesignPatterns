using System;

namespace ChainofResponsibility
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                throw new Exception();
            }
            catch (Exception ex)
            {
                DbLogger dbLogger = new DbLogger();
                dbLogger.Logging(ex.ToString());
            }

            Console.Read();
        }

    }

    public abstract class BaseLogger
    {
        protected BaseLogger NextLogger { get; set; }

        abstract public void WriteLog(string Messsage);

        internal void Logging(string message)
        {
            try
            {
                WriteLog(message);
            }
            catch
            {
                if (NextLogger != null)
                    NextLogger.Logging(message);
            }
        }

    }

    public class DbLogger : BaseLogger
    {
        public DbLogger()
        {
            NextLogger = new FileLogger();
        }
        public override void WriteLog(string Messsage)
        {
            try
            {
                throw new Exception();
            }
            catch (Exception)
            {
                NextLogger?.WriteLog(Messsage);
            }
        }
    }

    public class FileLogger : BaseLogger
    {
        public override void WriteLog(string Messsage)
        {
            Console.WriteLine(string.Concat(Messsage, " mesaj dosyaya yazıldı."));
        }
    }
}
