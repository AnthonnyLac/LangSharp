namespace LangSharp.Utils
{
    public static class PythonThread
    {
        private static IntPtr _threadState;

        public static void SetThreadState(IntPtr threadState) => _threadState = threadState;
        public static IntPtr GetThreadState() => _threadState;
    }
}
