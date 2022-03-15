using System;
using System.Linq;
using System.Threading.Tasks;
using MyoLib;
using MyoSharp.Poses;

namespace ConsoleApp1
{
    class Program
    {
        static MyoManager mgr;
        MyoSharp.Device.MyoEventArgs e;
        static void Main(string[] args)
        {
            mgr = new MyoManager();
            mgr.Init();
            mgr.UnlockAll(MyoSharp.Device.UnlockType.Hold);
            mgr.PoseChanged += Mgr_PoseChanged;
            mgr.MyoConnected += Mgr_MyoConnected;
            //mgr.MyoConnected += Mgr_MyoConnected1;
            mgr.StartListening();
            mgr.Vibrate();
            Console.WriteLine("ahah");
        }

        public static void Mgr_PoseChanged(object sender, MyoSharp.Device.PoseEventArgs e)
        {
            mgr.Vibrate();
            Console.WriteLine($"Pose : {e.Pose}");
        }

        private async static void Mgr_MyoConnected(object sender, MyoSharp.Device.MyoEventArgs e)
        {
            Console.WriteLine($"{e.Myo} connected ({e.Myo.Arm}, {e.Myo.Handle})");
            mgr.Vibrate(MyoSharp.Device.VibrationType.Long);
            await Task.Delay(2000);
            mgr.Vibrate(MyoSharp.Device.VibrationType.Medium);
            await Task.Delay(2000);
            mgr.Vibrate(MyoSharp.Device.VibrationType.Short);
            await Task.Delay(2000);
            mgr.VibrateAll();
            await Task.Delay(5000);
            mgr.Lock();
            await Task.Delay(5000);
            mgr.Unlock(MyoSharp.Device.UnlockType.Hold);
            mgr.AddHeldPose(mgr.Myos.First(), Pose.Fist, Pose.FingersSpread);
            await Task.Delay(10000);
            mgr.AddHeldPose(mgr.Myos.First(), Pose.Fist, Pose.WaveOut);
            await Task.Delay(10000);
            Console.WriteLine("Pose Sequence");
            mgr.AddPoseSequence(mgr.Myos.First(), Pose.Fist, Pose.FingersSpread);
        }
    }
}
