namespace LangSharp.Out.Consts
{
    public static class PythonThread
    {
        private static IntPtr ThreadState;
        public static void SetThreadState(IntPtr threadState) => ThreadState = threadState;
        public static IntPtr GetThreadState() => ThreadState;
    }
}
