using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
//using NAudio;
//using NAudio.Wave;
using Xamarin.Essentials;

namespace P_Layer.ViewModels
{
    class NAudioViewModel: ViewModelBase
    {
        //private WaveIn recorder;
        //private BufferedWaveProvider bufferedWaveProvider;
        //private SavingWaveProvider savingWaveProvider;
        //private WaveOut player;

        public NAudioViewModel()
        {
            StartRecordCommand = new Command(StartRecord);
            StopRecordCommand = new Command(StopRecord);
        }
        private string status = "Du har nu åbnet NAudioPage";
        private string recordingStatus = "Du er ikke igang med en optagelse";
        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        public string RecordingStatus
        {
            get => recordingStatus;
            set => SetProperty(ref recordingStatus, value);
        }

        public ICommand StartRecordCommand { get; }
        public ICommand StopRecordCommand { get; }

        private void StartRecord()
        {
            //// set up the recorder
            //recorder = new WaveIn();
            //recorder.DataAvailable += RecorderOnDataAvailable;
            //recordingStatus = "Du optager og afspiller!!!";
            //// set up our signal chain
            //bufferedWaveProvider = new BufferedWaveProvider(recorder.WaveFormat);
            //savingWaveProvider = new SavingWaveProvider(bufferedWaveProvider, "temp.wav");

            //// set up playback
            //player = new WaveOut();
            //player.Init(savingWaveProvider);

            //// begin playback & record
            //player.Play();
            //recorder.StartRecording();
        }
        private void StopRecord()
        {
            //// stop recording
            //recorder.StopRecording();
            //// stop playback
            //player.Stop();
            //// finalise the WAV file
            //savingWaveProvider.Dispose();
            //recordingStatus = "Du optager ikke";
        }
        //private void RecorderOnDataAvailable(object sender, WaveInEventArgs waveInEventArgs)
        //{
        //    //bufferedWaveProvider.AddSamples(waveInEventArgs.Buffer, 0, waveInEventArgs.BytesRecorded);
        //}
    }

    //internal class SavingWaveProvider : IWaveProvider, IDisposable
    //{
    //    private readonly IWaveProvider sourceWaveProvider;
    //    private readonly WaveFileWriter writer;
    //    private bool isWriterDisposed;

    //    public SavingWaveProvider(IWaveProvider sourceWaveProvider, string wavFilePath)
    //    {
    //        this.sourceWaveProvider = sourceWaveProvider;
    //        writer = new WaveFileWriter(wavFilePath, sourceWaveProvider.WaveFormat);
    //    }

    //    public int Read(byte[] buffer, int offset, int count)
    //    {
    //        var read = sourceWaveProvider.Read(buffer, offset, count);
    //        if (count > 0 && !isWriterDisposed)
    //        {
    //            writer.Write(buffer, offset, read);
    //        }
    //        if (count == 0)
    //        {
    //            Dispose(); // auto-dispose in case users forget
    //        }
    //        return read;
    //    }

    //    public WaveFormat WaveFormat { get { return sourceWaveProvider.WaveFormat; } }

    //    public void Dispose()
    //    {
    //        if (!isWriterDisposed)
    //        {
    //            isWriterDisposed = true;
    //            writer.Dispose();
    //        }
    //    }
    //}

}
