using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyoSharp;
using MyoSharp.Communication;
using MyoSharp.Device;
using MyoSharp.Exceptions;
using MyoSharp.Poses;

namespace MyoLib
{
    public class MyoManager : IDisposable
    {
        IHub _Hub { get; set; }

        IChannel _Channel { get; set; }

        public IReadOnlyMyoCollection Myos { get { return _Hub.Myos; } }

        public void Dispose()
        {
            _Channel?.Dispose();
            _Hub?.Dispose();
        }

        public void Init()
        {
            _Channel = Channel.Create(
                ChannelDriver.Create(ChannelBridge.Create(),
                MyoErrorHandlerDriver.Create(MyoErrorHandlerBridge.Create())));
            _Hub = Hub.Create(_Channel);
        }

        public void StartListening()
        {
            _Channel?.StartListening();
        }
        
        public void StopListening()
        {
            _Channel?.StopListening();
        }

        public event EventHandler<MyoEventArgs> MyoConnected
        {
            add { if(_Hub != null) _Hub.MyoConnected += value; }
            remove { if (_Hub != null) _Hub.MyoConnected -= value; }
        }

        public event EventHandler<MyoEventArgs> MyoDisconnected
        {
            add { if (_Hub != null) _Hub.MyoDisconnected += value; }
            remove { if (_Hub != null) _Hub.MyoDisconnected -= value; }
        }

        public event EventHandler<MyoEventArgs> MyoLocked
        {
            add { if (_Hub != null) _Hub.MyoLocked += value; }
            remove { if (_Hub != null) _Hub.MyoLocked -= value; }
        }
        public event EventHandler<MyoEventArgs> MyoUnlocked
        {
            add { if (_Hub != null) _Hub.MyoUnlocked += value; }
            remove { if (_Hub != null) _Hub.MyoUnlocked -= value; }
        }

        public event EventHandler<PoseEventArgs> PoseChanged
        {
            add { if (_Hub != null) _Hub.PoseChanged += value; }
            remove { if (_Hub != null) _Hub.PoseChanged -= value; }
        }

        public event EventHandler<PoseEventArgs> HeldPoseTriggered;

        public event EventHandler<PoseSequenceEventArgs> PoseSequenceCompleted;

        //public event EventHandler<OrientationDataEventArgs> OrientationDataAcquired;

        //public event EventHandler<AccelerometerDataEventArgs> AccelerometerDataAcquired;

        //public event EventHandler<GyroscopeDataEventArgs> GyroscopeDataAcquired;

        public void Vibrate(VibrationType vibrationType = VibrationType.Short, int myoId = 0)
        {
            if (myoId < 0 || myoId >= Myos.Count) return;
            Myos.ElementAt(myoId).Vibrate(vibrationType);
        }

        public void VibrateAll(VibrationType vibrationType = VibrationType.Short)
        {
            foreach(var myo in Myos)
            {
                myo.Vibrate(vibrationType);
            }
        }

        public void Lock(int myoId = 0)
        {
            if (myoId < 0 || myoId >= Myos.Count) return;
            Myos.ElementAt(myoId).Lock();
        }

        public void LockAll()
        {
            foreach(var myo in Myos)
            {
                myo.Lock();
            }
        }

        public void Unlock(UnlockType type, int myoId = 0)
        {
            if (myoId < 0 || myoId >= Myos.Count) return;
            Myos.ElementAt(myoId).Unlock(type);
        }

        public void UnlockAll(UnlockType type)
        {
            foreach(var myo in Myos)
            {
                myo.Unlock(type);
            }
        }

        private Dictionary<IMyo, IHeldPose> heldPoses = new Dictionary<IMyo, IHeldPose>();

        public void AddHeldPose(IMyo myo, TimeSpan interval, params Pose[] poses)
        {
            if(!heldPoses.ContainsKey(myo))
            {
                var pose = HeldPose.Create(myo, interval, poses);
                pose.Triggered += Pose_Triggered;
                heldPoses.Add(myo, pose);
                pose.Start();
                return;
            }
            heldPoses[myo].Stop();
            heldPoses[myo].AddPoses(poses);
            heldPoses[myo].Start();
        }

        public TimeSpan HeldPoseInterval { get; set; } = TimeSpan.FromMilliseconds(500);

        public void AddHeldPose(IMyo myo, params Pose[] poses)
        {
            AddHeldPose(myo, HeldPoseInterval, poses);
        }

        private void Pose_Triggered(object sender, PoseEventArgs e)
        {
             HeldPoseTriggered?.Invoke(sender, e);
        }

        public void RemoveHeldPose(IMyo myo, params Pose[] poses)
        {
            if (!heldPoses.ContainsKey(myo))
            {
                return;
            }
            heldPoses[myo].Stop();
            heldPoses[myo].RemovePoses(poses);
            heldPoses[myo].Start();
        }

        private List<IPoseSequence> poseSequences = new List<IPoseSequence>();

        public void AddPoseSequence(IMyo myo, params Pose[] poses)
        {
            var sequence = PoseSequence.Create(myo, poses);
            if (poseSequences.Contains(sequence)) return;
            poseSequences.Add(sequence);
            sequence.PoseSequenceCompleted += Sequence_PoseSequenceCompleted;
        }

        public void RemovePoseSequence(IMyo myo, params Pose[] poses)
        {
            var sequence = PoseSequence.Create(myo, poses);
            if (!poseSequences.Contains(sequence)) return;
            sequence.PoseSequenceCompleted -= Sequence_PoseSequenceCompleted;
            poseSequences.Remove(sequence);
        }

        private void Sequence_PoseSequenceCompleted(object sender, PoseSequenceEventArgs e)
        {
            PoseSequenceCompleted?.Invoke(sender, e);
        }

        public void SubscribeToOrientationData(int myoId, EventHandler<OrientationDataEventArgs> handler)
        {
            if (myoId < 0 || myoId >= Myos.Count) return;
            Myos.ElementAt(myoId).OrientationDataAcquired += handler;
        }

        public void UnsubscribeToOrientationData(int myoId, EventHandler<OrientationDataEventArgs> handler)
        {
            if (myoId < 0 || myoId >= Myos.Count) return;
            Myos.ElementAt(myoId).OrientationDataAcquired -= handler;
        }

        public void SubscribeToAccelerometerData(int myoId, EventHandler<AccelerometerDataEventArgs> handler)
        {
            if (myoId < 0 || myoId >= Myos.Count) return;
            Myos.ElementAt(myoId).AccelerometerDataAcquired += handler;
        }

        public void UnsubscribeToAccelerometerData(int myoId, EventHandler<AccelerometerDataEventArgs> handler)
        {
            if (myoId < 0 || myoId >= Myos.Count) return;
            Myos.ElementAt(myoId).AccelerometerDataAcquired -= handler;
        }

        public void SubscribeToGyroscopeData(int myoId, EventHandler<GyroscopeDataEventArgs> handler)
        {
            if (myoId < 0 || myoId >= Myos.Count) return;
            Myos.ElementAt(myoId).GyroscopeDataAcquired += handler;
        }

        public void UnsubscribeToGyroscopeData(int myoId, EventHandler<GyroscopeDataEventArgs> handler)
        {
            if (myoId < 0 || myoId >= Myos.Count) return;
            Myos.ElementAt(myoId).GyroscopeDataAcquired -= handler;
        }
    }
}
