using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace CNVP.Framework.Utils
{
    public class ThreadUtils
    {
        #region "属性"
        public delegate void DelegateComplete();
        public delegate void DelegateWork(int TaskIndex, int ThreadIndex);

        public DelegateComplete CompleteEvent;
        public DelegateWork WorkMethod;

        private Thread[] _Thread;
        private bool[] _ThreadState;
        private int _TaskCount = 0;
        private int _TaskIndex = 0;
        private int _ThreadCount = 5;

        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TaskCount">任务总数</param>
        public ThreadUtils(int TaskCount)
        {
            this._TaskCount = TaskCount;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TaskCount">任务总数</param>
        /// <param name="ThreadCount">线程总数</param>
        public ThreadUtils(int TaskCount, int ThreadCount)
        {
            _TaskCount = TaskCount;
            _ThreadCount = ThreadCount;
        }
        /// <summary>
        /// 获取任务
        /// </summary>
        /// <returns></returns>
        private int GetTask()
        {
            //多线程安全锁
            //防止并发操作
            lock (this)
            {
                if (_TaskIndex < _TaskCount)
                {
                    _TaskIndex++;
                    return _TaskIndex;
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 启动线程
        /// </summary>
        public void Start()
        {
            _TaskIndex = 0;

            int Num = _TaskCount < _ThreadCount ? _TaskCount : _ThreadCount;
            _ThreadState = new bool[Num];
            _Thread = new Thread[Num];

            for (int i = 0; i < Num; i++)
            {
                _ThreadState[i] = false;
                _Thread[i] = new Thread(new ParameterizedThreadStart(Work));
                _Thread[i].Start(i);
            }
        }
        /// <summary>
        /// 结束线程
        /// </summary>
        public void Stop()
        {
            for (int i = 0; i < _Thread.Length; i++)
            {
                //结束线程
                _Thread[i].Abort();
            }
        }
        //执行任务
        public void Work(object arg)
        {
            //提取任务并执行
            int ThreadIndex = int.Parse(arg.ToString());
            int TaskIndex = GetTask();

            while (TaskIndex != 0 && WorkMethod != null)
            {
                WorkMethod(TaskIndex, ThreadIndex + 1);
                TaskIndex = GetTask();
            }

            //所有任务执行完毕
            _ThreadState[ThreadIndex] = true;

            //处理线程并发问题
            //如果有两个线程同时完成了只允许一个出发Complete事件
            lock (this)
            {
                for (int i = 0; i < _ThreadState.Length; i++)
                {
                    if (_ThreadState[i] == false)
                    {
                        return;
                    }
                } 
                //如果全部完成
                if (CompleteEvent != null)
                {
                    CompleteEvent();
                }
                //重置线程状态
                for (int j = 0; j < _ThreadState.Length; j++)
                {
                    _ThreadState[j] = false;
                }
            }
        }
    }
}