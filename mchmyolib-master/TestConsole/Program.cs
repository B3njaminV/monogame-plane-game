using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyoLib;
using MyoSharp;
using static System.Console;
using MyoSharp.Poses;

namespace TestConsole
{
    class Program
    {
        static MyoManager mgr;
        static void Main(string[] args)
        {
            mgr = new MyoManager();
            mgr.Init();
            //mgr.MyoConnected += Mgr_MyoConnected;
            //mgr.MyoLocked += Mgr_MyoLocked;
            //mgr.MyoUnlocked += Mgr_MyoUnlocked;
            //mgr.PoseChanged += Mgr_PoseChanged;
            //mgr.HeldPoseTriggered += Mgr_HeldPoseTriggered;
            //mgr.PoseSequenceCompleted += Mgr_PoseSequenceCompleted;
            mgr.MyoConnected += Mgr_MyoConnected1;
            mgr.StartListening();
            ReadKey();
        }

        private static void Mgr_MyoConnected1(object sender, MyoSharp.Device.MyoEventArgs e)
        {
            //mgr.SubscribeToOrientationData(0, (source, args) => WriteLine($"{args.Yaw:0.00} ; {args.Pitch:0.00} ; {args.Roll:0.00}"));
            //mgr.SubscribeToGyroscopeData(0, (source, args) => WriteLine($"{args.Gyroscope.X:00.00} ; {args.Gyroscope.Y:00.00} ; {args.Gyroscope.Z:00.00}"));
            mgr.SubscribeToAccelerometerData(0, (source, args) => WriteLine($"{args.Accelerometer.X:00.00} ; {args.Accelerometer.Y:00.00} ; {args.Accelerometer.Z:00.00}"));
        }

        private static void Mgr_PoseSequenceCompleted(object sender, PoseSequenceEventArgs e)
        {
            WriteLine($"Sequence completed : {e.Poses.Select(p => p.ToString()).Aggregate("", (chaine, s) => $"{chaine} {s}")}");
        }

        private static Dictionary<Pose, string> traductions = new Dictionary<Pose, string>()
        {
            [Pose.Fist] = "POING FERME",
            [Pose.FingersSpread] = "MAIN OUVERTE",
            [Pose.WaveOut] = "MAIN A DROITE"
        };

        private static void Mgr_HeldPoseTriggered(object sender, MyoSharp.Device.PoseEventArgs e)
        {
            WriteLine($"HeldPose : {traductions[e.Pose]}");
        }

        private static void Mgr_PoseChanged(object sender, MyoSharp.Device.PoseEventArgs e)
        {
            WriteLine($"{e.Pose}");
        }

        private static void Mgr_MyoUnlocked(object sender, MyoSharp.Device.MyoEventArgs e)
        {
            WriteLine($"{e.Myo} has been unlocked");
        }

        private static void Mgr_MyoLocked(object sender, MyoSharp.Device.MyoEventArgs e)
        {
            WriteLine($"{e.Myo} has been locked");
        }

        

        private async static void Mgr_MyoConnected(object sender, MyoSharp.Device.MyoEventArgs e)
        {
            WriteLine($"{e.Myo} connected ({e.Myo.Arm}, {e.Myo.Handle})");
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
            WriteLine("Pose Sequence");
            mgr.AddPoseSequence(mgr.Myos.First(), Pose.Fist, Pose.FingersSpread);
        }
    }
}
