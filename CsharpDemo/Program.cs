using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace CsharpDemo
{
    class Program
    {
        static bool done;
        static void Main(string[] args)
        {
            #region Thread Demo 1
            Thread t1 = new Thread(p =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(1);
                    Thread.Sleep(100);
                }
            });
            t1.Start();
            #endregion

            #region Thread Demo 2
            new Thread(Go).Start(); //在新线程调用Go()
            Go(); //在主线程调用Go()
            #endregion

            #region issue Demo
            new Thread(Run).Start(); //在新线程调用Run()
            Run(); //在主线程调用Run()
            #endregion

            #region right Demo
            new Thread(mLocker).Start(); //在新线程调用mLocker()
            mLocker(); //在主线程调用mLocker()
            #endregion

            #region Async Demo
            AsyncPrint();
            for (int i = 0; i < 11; i++)
            {
                Console.WriteLine(1);
                Thread.Sleep(100);
            }
            #endregion


        }
        static void Go()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(1);
                Thread.Sleep(100);
            }
        }
        static void Run()
        {
            if (!done) { Console.WriteLine("Done"); done = true; } //done为假，则输出
        }

        static object locker = new object();
        static void mLocker()
        {
            lock (locker)
            {
                if (!done) { Console.WriteLine("Done"); done = true; }
            }
        }
        public async static void AsyncPrint()
        {
            Console.WriteLine("异步方法调用开始");
            await Task.Delay(1000);
            Console.WriteLine("退出异步方法");
        }
    }
}
