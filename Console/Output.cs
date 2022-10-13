namespace auto
{
    public interface IOutput
    {
        void Write(string msg);
        void WriteLine(string msg);
    }
    public class ConsoleOutput : IOutput
    {
        public void Write(string msg)
        {
            System.Console.Write(msg);
        }

        public void WriteLine(string msg)
        {
            System.Console.WriteLine(msg);
        }
    }
}
